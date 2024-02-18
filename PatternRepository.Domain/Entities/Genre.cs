using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatternRepository.Domain.Common;

namespace PatternRepository.Domain.Entities
{
    public class Genre:BaseEntity
    {
       

        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
