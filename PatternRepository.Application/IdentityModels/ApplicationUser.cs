using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(50)]
        public string? FisrtName { get; set; }
        [MaxLength(50)]

        public string? LAstName { get; set; }
    }
}
