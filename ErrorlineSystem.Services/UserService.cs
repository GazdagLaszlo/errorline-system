using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.Services;

public interface IUserService
{
    List<User> List();
}

public class UserService(AppDbContext context) : IUserService
{
    public List<User> List()
    {
        return context.Users.ToList();
    }
}