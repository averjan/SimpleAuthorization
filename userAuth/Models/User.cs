using System;
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
