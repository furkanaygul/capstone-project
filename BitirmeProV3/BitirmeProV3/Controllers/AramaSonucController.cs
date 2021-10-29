using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitirmeProV3.Controllers
{
    public class AramaSonucController : Controller
    {
       
        // GET: AramaSonuc
        [HttpPost]
        public ActionResult AramaSonuc(string arama)
        {
            var model = new AramaSonucModel();
            List<TBL_Kullanici> kisiler = new List<TBL_Kullanici>();
            List<TBL_IndexKart> kartlar = new List<TBL_IndexKart>();
            BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
            var kullanicilar = db.TBL_Kullanici.ToList();
            var ilanlar = db.TBL_IndexKart.ToList();

            if (arama != "")
            {
                arama = arama.ToLower();
            foreach (var item in kullanicilar)
            {

                    if ((-1) != item.TBL_KullaniciOzellik.KullaniciAdi.ToLower().IndexOf(arama)||(-1)!=item.Adi.ToLower().IndexOf(arama))
                {
                          
                        kisiler.Add(item);

                }
            }
            foreach (var item in ilanlar)
            {
                if ((-1) != item.TBL_Kullanici.TBL_KullaniciOzellik.KullaniciAdi.ToLower().IndexOf(arama) || (-1)!=item.KartBaslik.ToLower().IndexOf(arama)|| (-1) != item.KartAciklamasi.ToLower().IndexOf(arama))
                {
                    
                    kartlar.Add(item);


                }
            }
            }
            model.Kullanici = kisiler;
            model.Ilanlar = kartlar;
            return View(model);
        }
    }
}