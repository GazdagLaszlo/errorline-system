using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ErrorlineSystem.Services;

public interface IUserService
{
    Task<UserDto> RegisterAsync(UserCreateDto userDto);
    Task<string> LoginAsync(UserLoginDto userDto);
    Task<UserDto> UpdateUserAsync(int userId, UserCreateDto userDto);
    Task<IList<UserDto>> GetAllUsers();
    Task DeleteUserAsync(int userId);
}

public class UserService(AppDbContext context, IMapper mapper) : IUserService
{
    public async Task<IList<UserDto>> GetAllUsers()
    {
        var users = await context.Users
                .Include(x => x.Role)
                .ToListAsync();
        return mapper.Map<IList<UserDto>>(users);
    }
    
    public async Task<string> LoginAsync(UserLoginDto userLoginDto)
    {
       var user = await context.Users
           .Include(u => u.Role)
           .FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);
       if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
       {
           throw new UnauthorizedAccessException("Invalid email or password!");
       }

       return await GenerateToken(user);
    }
    
    private async Task<string> GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("randomSztring12345_x2____randomSztring12345_x2"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(5));

        var id = await GetClaimsIdentity(user);
        var token = new JwtSecurityToken("https://localhost:8080", "https://localhost:8080", id.Claims, expires: expires, signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<ClaimsIdentity> GetClaimsIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new("roleid", user.Role.Id.ToString()),
            new(ClaimTypes.Role, user.Role.Type.ToString()),
            new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
        };

        return new ClaimsIdentity(claims, "Token");
    }


    public async Task<UserDto> RegisterAsync(UserCreateDto userCreateDto)
    {
        var emailAlreadyInUse = await context.Users.AnyAsync(x => x.Email == userCreateDto.Email);
        if (emailAlreadyInUse) 
        {
            throw new Exception($"The given email address is already in use.");
        }
        
        var user = mapper.Map<User>(userCreateDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

        if (user.Role == null)
        {
            var residentRole = await context.Roles
                .FirstOrDefaultAsync(x => x.Type == RoleType.Resident);
            user.Role = residentRole;
        }

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int userId, UserCreateDto userCreateDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"User not found with id: {userId}");
        }
        mapper.Map(userCreateDto, user);

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = context.Users
            .FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User not found with id: {userId}");
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}