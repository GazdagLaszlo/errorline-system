using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

public enum RoleType
{ 
    /// <summary>
    /// Jogosultságok
    /// - Hibabejelentés
    /// - Hibabejelentés módosítása
    /// - Visszajelzés javításról
    /// </summary>
    Resident,

    /// <summary>
    /// Jogosultságok
    /// - Hozzá rendelt hibák megtekintése
    /// - Hibák kijavításának visszaigazolása
    /// - Hiba kapcsán eszköz rendelése
    /// </summary>
    MaintenanceWorker,

    /// <summary>
    /// Jogosultságok
    /// - Összes hibabejelentés megtekintése
    /// - Hibabejelentések karbantartóhoz rendelése
    /// - Hibabejelentés státuszának módosítása
    /// - Eszközrendelési igények leadása
    /// - Rendelési folyamat nyomon követése
    /// </summary>
    MaintenanceManager,

    /// <summary>
    /// Jogosultságok
    /// - Új eszközök és hibatípusok hozzáadása
    /// - Felszereltségek hozzáadása és eltávolítása helyiségekre vonatkozóan
    /// </summary>
    Administrator
}
public class Role
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public RoleType Type { get; set; }
    
    public List<User> Users { get; set; }
}