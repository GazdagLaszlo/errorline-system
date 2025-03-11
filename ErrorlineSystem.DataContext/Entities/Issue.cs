using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

public class Issue
{
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
    public int IssueTypeId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Item { get; set; } // TODO: Ennek még kell, hogy micsoda 

    /// <summary>
    /// Az ügy aktuális státusza
    /// </summary>
    public IssueState State { get; set; }

    /// <summary>
    /// Az ügyhöz tartozó komment
    /// </summary>    
    public string InternalComment { get; set; } // TODO: Ez lehetne mondjuk egy List<string> és akkor több komment lehetne 

    /// <summary>
    /// Korábbi feladatra való hivatkozás
    /// </summary>
    public int? ParentIssueId { get; set; }

    /// <summary>
    /// A feladat felelősének az azonosítója egy User-re mutat
    /// </summary>
    public int? AssignedId { get; set; } // TODO: Ez nem lehetne inkább egy User típus? és akkor idegen kulcs lenne

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
    public string ModifiedBy { get; set; } // TODO: Ez is lehetne idegen kulcs a User táblára

    /// <summary>
    /// A feladat bejelentőjének az azonosítója
    /// </summary>
    public string CreatedBy { get; set; } // TODO: Ez is lehetne idegen kulcs a User táblára

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