using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsBrowser.Models
{
    public class UserProperty
    {
        public Guid UserID { get; set; }
        public Guid UserTypeID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfileURL { get; set; }
        public string ProfilePhoto { get; set; }
    }
}