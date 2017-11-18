using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ucsdscheduleme.Models
{
    public class ApplicationUser : IdentityUser
    { 
        public ICollection<UserSection> UserSections { get; set; }
    }
}
