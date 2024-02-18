using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Dto
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string? StoreLine { get; set; }

        public Guid GenreId { get; set; }
        
    }

    public class MovieMainDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Year { get; set; }
    }

    public class MovieListDto : MovieMainDto
    {
        public double Rate { get; set; }
        public Guid GenreId { get; set; }
        public GenreDto Genre { get; set; }
    }

}
