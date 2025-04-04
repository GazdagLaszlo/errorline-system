using ErrorlineSystem.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorlineSystem.DataContext.Dtos
{
    /// <summary>
    /// A Hibajegyhez tartozó kommentek DTO-ja
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// A komment írója
        /// </summary>
        public string Authorname { get; set; }

        /// <summary>
        /// A komment tartalma
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// A létrehozás dátuma
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
