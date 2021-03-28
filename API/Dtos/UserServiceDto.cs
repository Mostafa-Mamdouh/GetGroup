using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GetGroup.Core.Helpers.Enum;

namespace GetGroup.API.Dtos
{
    public class UserServiceDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsDeleted { get; set; }
        public RequestStatus RequestSatusId { get; set; }
        public string RequestSatusName { get; set; }

    }
}
