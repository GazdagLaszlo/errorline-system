using AutoMapper;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ErrorlineSystem.Services;

public interface IUserService
{
    Task<UserDto> RegisterAsync(UserCreateDto userDto);
    //Task<string> LoginAsync(UserLoginDto userDto);
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

    /*
public async Task<string> LoginAsync(UserLoginDto userLoginDto)
{
   var user = await context.Users
       .FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);
   if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
   {
       throw new UnauthorizedAccessException("Invalid email or password!");
   }

   return await GenerateToken(user);
}
*/

    public async Task<UserDto> RegisterAsync(UserCreateDto userCreateDto)
    {
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