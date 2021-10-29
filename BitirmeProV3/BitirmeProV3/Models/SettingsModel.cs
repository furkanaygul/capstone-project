using BitirmeProV3.Identity;
using BitirmeProV3.Models.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Models
{
    public class SettingsModel
    {
        public TBL_Kullanici kullanici { get; set; }
        public UserManager<ApplicationUser> userManager { get; set; }
        public TBL_ProfilKart CardAdd { get; set; }
    }
}