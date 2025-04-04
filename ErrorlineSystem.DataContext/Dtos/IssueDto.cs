using ErrorlineSystem.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorlineSystem.DataContext.Dtos
{
    /// <summary>
    /// A hibajegy rögzítéséhez tartozó DTO 
    /// </summary>
    public class IssueRequestDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// Hosszú részletes leírás
        /// </summary>
        [Required(ErrorMessage = "A probléma leírását kötelező megadni!")]
        public string Description { get; set; }

        /// <summary>
        /// Az ügy típusának azonosítója
        /// </summary>
        [Required(ErrorMessage = "Az ügy típusát kötelező megadni!")]
        public int IssueTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Item { get; set; } // TODO: Ennek még kell, hogy micsoda 

        /// <summary>
        /// Az ügy aktuális státusza
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Korábbi feladatra való hivatkozás
        /// </summary>
        public int? ParentIssueId { get; set; }

        /// <summary>
        /// A feladat felelősének az azonosítója egy User-re mutat
        /// </summary>
        [Required(ErrorMessage = "A felhasználó megadása kötelező!")]
        public string Username { get; set; } // TODO: azért erre gondoltam, mert mi csak egy usernevet kapunk és az ID-t mi szedjük ki


        /// <summary>
        /// Az utolsó módosító User
        /// </summary>
        public string? ModifierUserName { get; set; } // TODO: Itt is csak usenevet kapunk és mi keressük is az ID-t


        /// <summary>
        /// A feladathoz szükséges eszközök listája
        /// </summary>
        public List<int>? Equipments { get; set; } = new List<int>(); // Kapcsolat az Equipment oldalon

        /// <summary>
        /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
        /// de meg kell rendelni
        /// </summary>
        public List<int>? EquipmentOrders { get; set; } = new List<int>(); // Kapcsolat az Order oldalon

        /// <summary>
        /// Az ügyhöz tartozó intézmény azonosítója
        /// </summary>
        public int FacilityId { get; set; }
    }


    /// <summary>
    /// A hibajegy adatainak visszaadásához tartozó DTO
    /// </summary>
    public class IssueResponseDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// Hosszú részletes leírás
        /// </summary>
        [Required(ErrorMessage = "A probléma leírását kötelező megadni!")]
        public string Description { get; set; }

        /// <summary>
        /// Az ügy típusának azonosítója
        /// </summary>
        [Required(ErrorMessage = "Az ügy típusát kötelező megadni!")]
        public IssueTypeDto IssueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Item { get; set; } // TODO: Ennek még kell, hogy micsoda 

        /// <summary>
        /// Az ügy aktuális státusza
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Korábbi feladatra való hivatkozás
        /// </summary>
        public int? ParentIssueId { get; set; }

        /// <summary>
        /// A feladat felelősének az azonosítója egy User-re mutat
        /// </summary>
        [Required(ErrorMessage = "A felhasználó megadása kötelező!")]
        public string Username { get; set; } // TODO: azért erre gondoltam, mert mi csak egy usernevet kapunk és az ID-t mi szedjük ki


        /// <summary>
        /// Az utolsó módosító User
        /// </summary>
        public string? ModifierUserName { get; set; } // TODO: Itt is csak usenevet kapunk és mi keressük is az ID-t


        /// <summary>
        /// A feladathoz szükséges eszközök listája
        /// </summary>
        public List<EquipmentSearchListItemDto>? Equipments { get; set; } = new List<EquipmentSearchListItemDto>(); // Kapcsolat az Equipment oldalon

        /// <summary>
        /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
        /// de meg kell rendelni
        /// </summary>
        public List<EquipmentOrderResponseDto>? EquipmentOrders { get; set; } = new List<EquipmentOrderResponseDto>(); // Kapcsolat az Order oldalon

        /// <summary>
        /// Az ügyhöz tartozó intézmény azonosítója
        /// </summary>
        public string FacilityName { get; set; }

        /// <summary>
        /// Az ügyhöz tartozó kommentek
        /// </summary>
        public List<CommentDto>? InternalComment { get; set; } = new List<CommentDto>();
    }

    /// <summary>
    /// A hibajegy módosításához tartozó DTO
    /// </summary>
    public class IssueUpdateDto
    {
        /// <summary>
        /// Hosszú részletes leírás
        /// </summary>
        [Required(ErrorMessage = "A probléma leírását kötelező megadni!")]
        public string Description { get; set; }

        /// <summary>
        /// Az ügy típusának azonosítója
        /// </summary>
        [Required(ErrorMessage = "Az ügy típusát kötelező megadni!")]
        public int IssueTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Item { get; set; } // TODO: Ennek még kell, hogy micsoda 

        /// <summary>
        /// Az ügy aktuális státusza
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Korábbi feladatra való hivatkozás
        /// </summary>
        public int? ParentIssueId { get; set; }

        /// <summary>
        /// A feladat felelősének az azonosítója egy User-re mutat
        /// </summary>
        [Required(ErrorMessage = "A felhasználó megadása kötelező!")]
        public string Username { get; set; } // TODO: azért erre gondoltam, mert mi csak egy usernevet kapunk és az ID-t mi szedjük ki


        /// <summary>
        /// Az utolsó módosító User
        /// </summary>
        public string? ModifierUserName { get; set; } // TODO: Itt is csak usenevet kapunk és mi keressük is az ID-t


        /// <summary>
        /// A feladathoz szükséges eszközök listája
        /// </summary>
        public List<int>? Equipments { get; set; } = new List<int>(); // Kapcsolat az Equipment oldalon

        /// <summary>
        /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
        /// de meg kell rendelni
        /// </summary>
        public List<int>? EquipmentOrders { get; set; } = new List<int>(); // Kapcsolat az Order oldalon 

        /// <summary>
        /// Az ügyhöz tartozó intézmény azonosítója
        /// </summary>
        public int FacilityId { get; set; }
    }

    public class IssueDto
    {
        public int? Id { get; set; }

        /// <summary>
        /// Hosszú részletes leírás
        /// </summary>
        public string Description { get; set; }

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
        public int State { get; set; }

        /// <summary>
        /// Korábbi feladatra való hivatkozás
        /// </summary>
        public int? ParentIssueId { get; set; }

        /// <summary>
        /// A feladat felelősének az azonosítója egy User-re mutat
        /// </summary>
        public string Username { get; set; } // TODO: azért erre gondoltam, mert mi csak egy usernevet kapunk és az ID-t mi szedjük ki


        /// <summary>
        /// Az utolsó módosító User
        /// </summary>
        public string? ModifierUserName { get; set; } // TODO: Itt is csak usenevet kapunk és mi keressük is az ID-t


        /// <summary>
        /// A feladathoz szükséges eszközök listája
        /// </summary>
        public List<int>? Equipments { get; set; } = new List<int>(); // Kapcsolat az Equipment oldalon

        /// <summary>
        /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
        /// de meg kell rendelni
        /// </summary>
        public List<int>? EquipmentOrders { get; set; } = new List<int>(); // Kapcsolat az Order oldalon

        /// <summary>
        /// Az ügyhöz tartozó intézmény azonosítója
        /// </summary>
        public int FacilityId { get; set; }
    }
}

