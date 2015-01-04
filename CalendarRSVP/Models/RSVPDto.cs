using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarRSVP.Models
{
    public class RSVPDto
    {
        public long EntityId { get; set; }

        public long EntityName { get; set; }

        public long UserId { get; set; }

        public long TeamId { get; set; }
    }
}