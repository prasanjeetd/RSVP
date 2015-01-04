using CalendarRSVP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CalendarRSVP.Controllers
{
    public class RSVPController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello World");
        }

        public HttpResponseMessage AllocateVolunteer(RSVPDto rsvpDto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, rsvpDto);
        }

        public HttpResponseMessage UnAllocateVolunteer(RSVPDto rsvpDto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, rsvpDto);
        }

       
    }
}