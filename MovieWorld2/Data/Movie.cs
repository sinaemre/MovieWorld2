using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWorld2.Data
{
    [Table("Movies")]
    public class Movie
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; }

        public int Year { get; set; }

        public decimal Rating { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

    }
}
