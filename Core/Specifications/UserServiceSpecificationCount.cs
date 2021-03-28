
using GetGroup.Core.Entities;
using GetGroup.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
   public class UserServiceSpecificationCount : BaseSpecifcation<UserService>
    {
        public UserServiceSpecificationCount(ServiceSpecParams serviceSpecParams) : base(x =>
          (string.IsNullOrEmpty(serviceSpecParams.Search) || x.AppUser.FirstName.ToLower().Contains(serviceSpecParams.Search))
      )
        {

        }

      
    }
}
