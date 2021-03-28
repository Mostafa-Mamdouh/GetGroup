using GetGroup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetGroup.Core.Entities
{
   public class Service : BaseEntity
    {
     
        public string Name { get; set; }
        public ICollection<UserService> UserServices { get; set; }

    }
}
