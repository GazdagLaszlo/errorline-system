using ErrorlineSystem.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorlineSystem.DataContext.Dtos
{
    public class IssueDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Hosszú részletes leírás
        /// </summary>
        [Required(ErrorMessage ="A probléma leírását kötelező megadni!")]
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
        /// Az ügyhöz tartozó komment
        /// </summary>    
        [StringLength(100, ErrorMessage = "A komment hossza max 100 karakter lehet!", MinimumLength = 0)]
        public string InternalComment { get; set; } // TODO: Ez lehetne mondjuk egy List<string> és akkor több komment lehetne 

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
        public string? ModifiedBy { get; set; } // TODO: Itt is csak usenevet kapunk és mi keressük is az ID-t


        /// <summary>
        /// A feladathoz szükséges eszközök listája
        /// </summary>
        public List<Equipment>? Equipments { get; set; } = new List<Equipment>(); // Kapcsolat az Equipment oldalon

        /// <summary>
        /// Azoknak az eszközöknek a listája, amik a feladathoz szükségesek, 
        /// de meg kell rendelni
        /// </summary>
        public List<EquipmentOrder>? EquipmentOrders { get; set; } = new List<EquipmentOrder>(); // Kapcsolat az Order oldalon
    }

    public class IssueAssignDto
    {
        public int IssueId { get; set; }
        public string AssignedUser { get; set; }
    }
}
