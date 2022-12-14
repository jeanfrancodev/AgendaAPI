using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Dtos.ContactDto
{
    public class ContactDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
