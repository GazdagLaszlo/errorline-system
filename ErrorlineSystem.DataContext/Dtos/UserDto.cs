using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.DataContext.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RoleType RoleType { get; set; }

    public static UserDto FromEntity(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            RoleType = user.Role.Type
        };
    }
}