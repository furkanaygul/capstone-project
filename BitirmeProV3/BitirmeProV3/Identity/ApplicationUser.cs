using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public bool checkbox1 { get; set; }
        public bool checkbox2 { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PasswordControl { get; set; }
    }
}