using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitirmeProV3.Models.Entity;
namespace BitirmeProV3.Models
{
    public class ProfilModel
    {
        public TBL_Kullanici Kullanici { get; set; }
        public string yorum { get; set; }
        public List<int> puanlar { get; set; }
        public int puan { get; set; }
        public int ToplamYorum { get; set; }
        public float OrtalamaPuan { get; set; }

    }
    
}