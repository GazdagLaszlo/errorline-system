using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorlineSystem.DataContext.Entities
{
    /// <summary>
    /// Komment osztály, ezeknek a listája tartozik egy ügyhöz
    /// </summary>
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// A Komment írója
        /// </summary>
        [Required]
        public User Author { get; set; }

        /// <summary>
        /// A komment tartalma
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// A létrehozás dátuma
        /// </summary>
        public DateTime CreatedAt { get; set; }

        [Required]
        /// <summary>
        /// Melyik ügyhöz tartozik a komment
        /// </summary>
        public Issue Issue { get; set; }
    }
}
