using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErrorlineSystem.DataContext.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required, ForeignKey("RoleId")]
    public Role Role { get; set; }
    
    [Required]
    public string Password { get; set; }
}