
using GetGroup.Core.Entities;
using GetGroup.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
   public class UserServiceSpecification : BaseSpecifcation<UserService>
    {
        public UserServiceSpecification(ServiceSpecParams serviceSpecParams) : base(x =>
          (string.IsNullOrEmpty(serviceSpecParams.Search) || x.AppUser.FirstName.ToLower().Contains(serviceSpecParams.Search))
      )
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.Service);
            AddOrderBy(x => x.CreateDate);
            ApplyPaging(serviceSpecParams.PageSize * (serviceSpecParams.PageIndex - 1), serviceSpecParams.PageSize);

        }

        public UserServiceSpecification(int id) : base(x => x.UserId == id)
        {
            AddInclude(x => x.AppUser);
            AddInclude(x => x.Service);
        }
    }
}
