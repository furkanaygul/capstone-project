using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Models
{
    public class CardPageModel
    {
        public TBL_IndexKart Card { get; set; }
        public TBL_IndexCardComment yorum { get; set; }
        public List<Yorumlar> yorumlar { get; set; }

       
    }
    public class Yorumlar
    {
        public TBL_IndexCardComment yorum { get; set; }
        public TBL_Kullanici kullanici { get; set; }

    }
}