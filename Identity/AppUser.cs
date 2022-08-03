using HumanResources.Models;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Identity
{
    public class AppUser : IdentityUser
    {
        public List<Report>? Reports { get; set; }
    }
}
