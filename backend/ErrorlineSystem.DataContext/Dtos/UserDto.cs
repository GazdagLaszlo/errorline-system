using ErrorlineSystem.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public RoleType RoleType { get; set; }


    /*
    public static UserDto FromEntity(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            RoleType = user.Role.Type
        };
    }
    */
}

public class UserCreateDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public RoleType RoleType { get; set; }
}

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}