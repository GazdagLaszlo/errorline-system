using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

/// <summary>
/// A feladat felveehető státuszai
/// </summary>
public enum IssueState
{
    /// <summary>
    /// A feladat nyitott, még nem kezdődött el a hiba feltárása, javítása
    /// </summary>
    Open,

    /// <summary>
    /// A végrehajtást valami blokkolja, jellemzően eszköz rendelés
    /// </summary>
    Blocked,

    /// <summary>
    /// Beszerelés folyamatban
    /// </summary>
    InProgress,

    /// <summary>
    /// A feladat végrehajtója jelzi, hogy a javíás megtörtént
    /// </summary>
    Fixed,

    /// <summary>
    /// A bejelentő visszajelezte, hogy a feladat elvégezve, 
    /// a Closed státusz követi
    /// </summary>
    Verified,

    /// <summary>
    /// Zárt státusz
    /// </summary>
    Closed
}

/// <summary>
/// A hibajegy osztálya
/// </summary>
public class Issue
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Hooszú részletes leírás
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Az ügyhöz tartozó intézmény azonosítója
    /// </summary>
    public int FacilityId { get; set; }

    /// <summary>
    /// Az ügy típusának azonosítója
    /// </summary>
    public IssueType IssueType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Item { get; set; } // TODO: Ennek még kell, hogy micsoda 

    /// <summary>
    /// Az ügy aktuális státusza
    /// </summary>
    public IssueState State { get; set; }

    /// <summary>
    /// Az ügyhöz tartozó kommentek
    /// </summary>    
    public List<Comment> InternalComment { get; set; }
    // Legyen List, adhat visszajelzést a karbantartó és kollégista is.
    // Roli: Én nem bonyolítanám, így is lesz elég tennivaló. Ha marad rá ido megcsináltjuk.

    /// <summary>
    /// Korábbi feladatra való hivatkozás
    /// </summary>
    public Issue? ParentIssue { get; set; }

    /// <summary>
    /// A feladat felelősének az azonosítója egy User-re mutat
    /// </summary>
    public User? AssignedUser { get; set; }

    /// <summary>
    /// A feladat létrehozásának időpontja
    /// </summary>
    public DateTime CreateDateTime { get; set; }

    /// <summary>
    /// A feladat utolsó módosításának időpontja
    /// </summary>
    public DateTime ModifiedDateTime { get; set; }

    /// <summary>
    /// Az utolsó módosító User
    /// </summary>
    [Required]
    public User? ModifiedBy { get; set; }

    /// <summary>
    /// A feladat bejelentőjének az azonosítója
    /// </summary>
    [Required]
    public User CreatedBy { get; set; }

    /// <summary>
    /// A feladathoz szükséges eszközök listája
    /// </summary>
    public List<Equipment> Equipments { get; set; } = new List<Equipment>(); // Kapcsolat az Equipment oldalon

    /// <summary>
    /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
    /// de meg kell rendelni
    /// </summary>
    public List<EquipmentOrder> EquipmentOrders { get; set; } = new List<EquipmentOrder>(); // Kapcsolat az Order oldalon
}