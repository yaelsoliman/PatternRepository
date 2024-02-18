using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.IdentityModels
{
    public class AddRoleModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
