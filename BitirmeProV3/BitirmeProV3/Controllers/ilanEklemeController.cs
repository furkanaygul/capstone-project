using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BitirmeProV3.Controllers
{
    
    [Authorize]
    [ValidateInput(false)]
    public class ilanEklemeController : Controller
    {
        BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
        
        
        public IlanModel ekle(IlanModel model)
        {
            var ekle = db.TBL_Kategori.ToList();
            foreach (var item in ekle)
            {
                model.kategori.Add(item.KategoriAdi);
            }
            return model;
        }
    // GET: ilanEkleme
    public ActionResult Ilan()
        {
            IlanModel model = new IlanModel();
            
            return View(ekle(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ilan(IlanModel model, HttpPostedFileBase ilanResmi1, HttpPostedFileBase ilanResmi2, HttpPostedFileBase ilanResmi3)
        {
            var kullanici = User.Identity.Name;
            var kategori = db.TBL_Kategori.ToList();
            var sorgu = db.TBL_Kullanici.ToList();
            if (model.Ilan.KartBaslik==null)
            {
                ModelState.AddModelError("", "İlan Başlığı boş bırakılamaz.");
            }
            else
            {
                if (model.Ilan.KartBaslik.Length > 150)
                {
                    ModelState.AddModelError("", "İlan Başlığı 150 karakterden uzun olamaz");
                }
            }
            if (model.Ilan.KartAciklamasi==null)
            {
                ModelState.AddModelError("", "İlan Açıklaması boş bırakılamaz");
            }
            else
            {
                //if (model.Ilan.KartAciklamasi.Length > 1000)
                //{
                //    ModelState.AddModelError("", "İlan Açıklaması çok uzun");
                //}
            }
            
            
            if (model.ktgr== "Kategori")
            {
                ModelState.AddModelError("", "Kategori Seçilmedi");
            }

             if (ilanResmi1 != null)
                {
                    if (ilanResmi1.FileName.Length < 200)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ilanResmi1.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/CardImages"), temp);
                        ilanResmi1.SaveAs(filePath);
                        model.Ilan.KartResim1 = "/Content/images/CardImages/" + temp;
                    }
                    else ModelState.AddModelError("", "Resim 1 Dosya Adi cok uzun");
                    {

                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "İlan Resmi 1 Boş Olmamalıdır.");
                }
                if (ilanResmi2 != null)
                {
                    if (ilanResmi2.FileName.Length < 200)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ilanResmi2.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/CardImages"), temp);
                        ilanResmi2.SaveAs(filePath);
                        model.Ilan.KartResim2 = "/Content/images/CardImages/" + temp;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Resim 2 Dosya Adi cok uzun");
                    }
                    
                }
                if (ilanResmi3 != null)
                {
                    if (ilanResmi3.FileName.Length < 200)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ilanResmi3.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/CardImages"), temp);
                        ilanResmi3.SaveAs(filePath);
                        model.Ilan.KartResim3 = "/Content/images/CardImages/" + temp;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Resim 3 Dosya Adi cok uzun");
                    }
                    
                }
            
            
            if (ModelState.IsValid==true)
            {
                foreach (var item in sorgu)
                {
                    if (kullanici == item.TBL_KullaniciOzellik.KullaniciAdi)
                    {
                        foreach (var ktgr in kategori)
                        {
                            if (model.ktgr == ktgr.KategoriAdi)
                            {
                                model.Ilan.KategoriID = ktgr.KategoriID;
                            }
                        }
                        model.Ilan.KullaniciID = item.KullaniciID;
                        if (model.yardim == true)
                        {
                            model.Ilan.AliciSatici = true;
                        }
                        else {
                            model.Ilan.AliciSatici = false;
                            if (model.Ilan.Fiyat == null)
                            {
                                ModelState.AddModelError("", "İlan Fiyatı Boş Olmamalıdır");
                            }
                        }
                        model.Ilan.Durum = true;
                        //item.TBL_IndexKart.Add(model.Ilan);
                        //item.TBL_IndexKart.Add(model.Ilan);
                        //db.TBL_IndexKart.Add(model.Ilan);
                        db.Entry(model.Ilan).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Main");
            }
            
                    return View(ekle(model));
        }
    }
}