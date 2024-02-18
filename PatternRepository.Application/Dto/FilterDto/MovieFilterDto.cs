using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Dto.FilterDto
{
    public class MovieFilterDto:BaseFilterDto
    {
        public string? Title { get; set; }
        public int? Year { get; set; }
        public Guid? GenreId { get; set; }
    }
}
