using Microsoft.AspNetCore.Identity;

namespace U_OnlineBazer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
