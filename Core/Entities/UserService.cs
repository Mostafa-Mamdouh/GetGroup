using GetGroup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static GetGroup.Core.Helpers.Enum;

namespace GetGroup.Core.Entities
{
   public class UserService : BaseEntity
    {
  
        public bool IsDeleted { get; set; }
        public RequestStatus RequestSatusId { get; set; }

        public int UserId { get; set; }
        public  AppUser AppUser { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
