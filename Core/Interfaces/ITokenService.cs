using GetGroup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetGroup.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user,string role);
    }
}
