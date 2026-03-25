using Microsoft.AspNetCore.Identity;

namespace ECommereceBookStore.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}