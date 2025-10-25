using Microsoft.AspNetCore.Identity;

namespace WebServiceProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Можно добавить дополнительные поля
        public string? FullName { get; set; }
    }
}