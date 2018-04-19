using System;
using System.Collections.Generic;
namespace RealEstateProject.Models
{
    public class UserWithRolesViewModel
    {
        public ApplicationUser User { set; get; }
        public List<string> Roles { set; get; } 
    }
}
