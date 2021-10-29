using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;

namespace BitirmeProV3.Controllers
{
    
    public class MainController : Controller
    {
        // GET: Main
        BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
        public ActionResult Index()
        {
            var Kategori = db.TBL_Kategori.ToList();
            var IndexCard = db.TBL_IndexKart.ToList();
            
            KategoriCardModel model = new KategoriCardModel();
            model.Kategoris = Kategori;
            model.IndexKarts = IndexCard;
            return View(model);
        }
        public ActionResult Kategori(int id)
        {
            KategoriCardModel model = new KategoriCardModel();
            var Kategori = db.TBL_Kategori.ToList();
            var IndexCard = db.TBL_IndexKart.ToList();
            if (id==-1)
            {
                foreach (var item in IndexCard)
                {
                    if (true == item.AliciSatici)
                    {
                        model.filtre.Add(item);

                    }
                }
            }
            else
            {
                foreach (var item in IndexCard)
                {
                    if (id == item.TBL_Kategori.KategoriID)
                    {
                        model.filtre.Add(item);

                    }
                }
            }
            
            model.Kategoris = Kategori;
            model.IndexKarts = model.filtre;

            return View("~/Views/Main/Index.cshtml",model);
        }
        public ActionResult Getimage()
        {
            
            var sorgu = db.TBL_Kullanici.ToList();
            foreach (var item in sorgu)
            {
                if (User.Identity.Name == item.TBL_KullaniciOzellik.KullaniciAdi)
                {
                    
                    return Content(item.TBL_KullaniciOzellik.ProfilResmi);
                }
            }
            return Content(null);
        }

    }
}