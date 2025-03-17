using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;

namespace ErrorlineSystem.Services;

public interface IUserService
{
    List<UserDto> List();
}

public class UserService(AppDbContext context) : IUserService
{
    public List<UserDto> List()
    {
        return context.Users.ToList().ConvertAll(UserDto.FromEntity);
    }
}