using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Models
{
    public class AramaSonucModel
    {
       
            public List<TBL_Kullanici> Kullanici { get; set; }
            public List<TBL_IndexKart> Ilanlar { get; set; }

        
    }
}