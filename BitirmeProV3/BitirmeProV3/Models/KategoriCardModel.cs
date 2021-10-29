using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitirmeProV3.Models.Entity;
namespace BitirmeProV3.Models
{
    public class KategoriCardModel
    {
        public List<TBL_Kategori> Kategoris { get; set; }
        public List<TBL_IndexKart> IndexKarts { get; set; }
        public List<TBL_IndexKart> filtre { get; set; }

        public KategoriCardModel()
        {
            filtre = new List<TBL_IndexKart>();
        }
    }
}