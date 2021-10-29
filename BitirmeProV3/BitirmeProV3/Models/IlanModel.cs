using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Models
{
    public class IlanModel
    {
        public List<string> kategori { get; set; }
        public TBL_IndexKart Ilan { get; set; }
        public bool yardim { get; set; }
        public string ktgr { get; set; }

        public IlanModel()
        {
            kategori = new List<string>();
        }
    }
}