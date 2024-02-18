using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Dto.FilterDto
{
    public class BaseFilterDto
    {
        public string? Keyword { get; set; }
        public bool IsAdvance { get; set; } = false;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
