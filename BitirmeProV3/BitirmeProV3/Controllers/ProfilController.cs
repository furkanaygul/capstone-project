using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitirmeProV3.Models.Entity;
using BitirmeProV3.Models;
using System.Net;

namespace BitirmeProV3.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
        ProfilModel model = new ProfilModel();
        
            
       
        
        public ActionResult Profil(string kullaniciAdi)
        {
            
            
            var sorgu = db.TBL_Kullanici.ToList();
            model.ToplamYorum = 0;
            float temp = 0;
            foreach (var item in sorgu)
            {
                if (kullaniciAdi == item.TBL_KullaniciOzellik.KullaniciAdi)
                {
                    var yorumlar = db.TBL_Yorumlar.ToList();
                    foreach (var i in yorumlar)
                    {
                        if (i.KullaniciID == item.KullaniciID)
                        {
                            model.ToplamYorum++;
                            temp += i.Puan.Value;

                        }

                    }
                    model.OrtalamaPuan = (float)Math.Round((temp / (model.ToplamYorum)), 1);
                    model.Kullanici = item;
                    model.puanlar = new List<int> { 1, 2, 3, 4, 5 };
                    return View(model);

                }
                

            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult YorumEkle(TBL_Yorumlar model,string profil, string YorumYapanAdi)
        {
            var sorgu = db.TBL_Kullanici.ToList();
            var yorum = new TBL_Yorumlar();
            foreach (var item in sorgu)
            {
                if (profil==item.TBL_KullaniciOzellik.KullaniciAdi)
                {
                    foreach (var i in sorgu)
                    {
                        if (i.TBL_KullaniciOzellik.KullaniciAdi==YorumYapanAdi)
                        {
                            yorum.YorumYapanKulPP = i.TBL_KullaniciOzellik.ProfilResmi;
                            break;
                        }
                    }
                    yorum.KullaniciID = item.KullaniciID;
                    yorum.Puan = model.Puan;
                    yorum.Tarih = DateTime.Now;
                    yorum.Yorum = model.Yorum;
                    yorum.YorumYapanKulAdi = YorumYapanAdi;
                    item.TBL_Yorumlar.Add(yorum);
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Profil", "Profil", new { kullaniciAdi = profil });
        }

        
        public ActionResult ProfilCardDelete(int id)
        {
            var sorgu = db.TBL_ProfilKart.ToList();
            foreach (var item in sorgu)
            {
                if (item.ProfilKartID==id)
                {
                    db.TBL_ProfilKart.Remove(item);
                    db.SaveChanges();
                    return RedirectToAction("Profil", new { kullaniciAdi = User.Identity.Name });
                }
            }
            return RedirectToAction("Profil", new { kullaniciAdi = User.Identity.Name });
        }
        public ActionResult YorumSil(int id,string profil)
        {
            var sorgu = db.TBL_Yorumlar.ToList();
            
            foreach (var item in sorgu)
            {
                if (item.YorumID==id)
                {
                    
                    db.TBL_Yorumlar.Remove(item);
                    db.SaveChanges();
                    
                    break;
                }
            }
            return RedirectToAction("Profil", "Profil",new { kullaniciAdi=profil });
        }
    }
}