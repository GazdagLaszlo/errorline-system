namespace ErrorlineSystem.DataContext.Entities;

public enum RoleType
{ 
    /// <summary>
    /// Jogosultságok
    /// - Hibabejelentés
    /// - Hibabejelentés módosítása
    /// - Visszajelzés javításról
    /// </summary>
    Kollégista,

    /// <summary>
    /// Jogosultságok
    /// - Hozzá rendelt hibák megtekintése
    /// - Hibák kijavításának visszaigazolása
    /// - Hiba kapcsán eszköz rendelése
    /// </summary>
    Karbantartó,

    /// <summary>
    /// Jogosultságok
    /// - Összes hibabejelentés megtekintése
    /// - Hibabejelentések karbantartóhoz rendelése
    /// - Hibabejelentés státuszának módosítása
    /// - Eszközrendelési igények leadása
    /// - Rendelési folyamat nyomon követése
    /// </summary>
    KarbantartásiVezető,

    /// <summary>
    /// Jogosultságok
    /// - Új eszközök és hibatípusok hozzáadása
    /// - Felszereltségek hozzáadása és eltávolítása helyiségekre vonatkozóan
    /// </summary>
    Adminisztrátor
}
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
}t