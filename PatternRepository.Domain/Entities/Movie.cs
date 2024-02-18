using PatternRepository.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Domain.Entities
{
    public class Movie:BaseEntity
    {
       
        [MaxLength(255)]
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string? StoreLine { get; set; }
        
        public Guid GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre? Genre { get; set; }

    }
}
