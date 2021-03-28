using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetGroup.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserService> UserServices { get; set; }
    }
}
