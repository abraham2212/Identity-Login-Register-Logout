using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class AppUser:IdentityUser
    {
        public string  FullName { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
