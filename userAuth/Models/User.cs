using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace userAuth.Models
{
    public class User : IdentityUser
    {
        public DateTime LastLog { get; set; }
        public DateTime RegTime { get; set; }
        public bool Blocked { get; set; }
    }
}
