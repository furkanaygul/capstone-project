using BitirmeProV3.Identity;
using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BitirmeProV3.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class SettingsController : Controller
    {
        public class Kullanici
        {
            public Kullanici()
            {
                Kullanicis = new TBL_Kullanici();
                Kullanicis.TBL_KullaniciOzellik = new TBL_KullaniciOzellik();
                Kullanicis.TBL_Profil = new TBL_Profil();
            }
            public TBL_Kullanici Kullanicis;
        }

        private UserManager<ApplicationUser> userManager;
        public SettingsController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
        }
        // GET: Settings
        BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();

        public ActionResult Settings(string kullaniciAdi)
        {
            if (kullaniciAdi==User.Identity.Name)
            {
                var model = new SettingsModel();
                BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
                var sorgu = db.TBL_Kullanici.ToList();

                foreach (var item in sorgu)
                {
                    if (kullaniciAdi == item.TBL_KullaniciOzellik.KullaniciAdi)
                    {
                        model.kullanici = item;
                        return View(model);
                    }
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        [HttpPost]
        public ActionResult Settings(SettingsModel model, HttpPostedFileBase uploadfile)
        {
            var sorgu = db.TBL_Kullanici.ToList();
            foreach (var item in sorgu)
            {
                if (model.kullanici.KullaniciID==item.KullaniciID)
                {
                    var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
                    userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

                    if (model.kullanici.Adi != null)
                    {
                        if (model.kullanici.Adi.Length > 50)
                        {
                            ModelState.AddModelError("", "İsim 50 Karekterden Fazla Olamaz.");

                        }
                        else item.Adi = model.kullanici.Adi;

                    }
                    else ModelState.AddModelError("", "Adı Boş Bırakılamaz");

                    if (model.kullanici.TBL_KullaniciOzellik.Unvani != null)
                    {
                        if (model.kullanici.TBL_KullaniciOzellik.Unvani.Length > 50)
                        {
                            ModelState.AddModelError("", "İsim 50 Karekterden Fazla Olamaz.");

                        }
                        else item.TBL_KullaniciOzellik.Unvani = model.kullanici.TBL_KullaniciOzellik.Unvani;

                    }
                    else ModelState.AddModelError("", "Adı Boş Bırakılamaz");

                    if (model.kullanici.TBL_KullaniciOzellik.KullaniciAdi.Length>50)
                    {
                        ModelState.AddModelError("", "Kullanıcı Adı 50 Karekterden Fazla Olamaz.");
                        
                    }
                    else
                      if (model.kullanici.TBL_KullaniciOzellik.KullaniciAdi != item.TBL_KullaniciOzellik.KullaniciAdi)
                    {
                        foreach (var i in userManager.Users)
                        {
                            if (i.UserName == model.kullanici.TBL_KullaniciOzellik.KullaniciAdi)
                            {
                                ModelState.AddModelError("", "Kullanici  Adi zaten alınmış");

                            }
                        }

                    }
                    else item.TBL_KullaniciOzellik.KullaniciAdi = model.kullanici.TBL_KullaniciOzellik.KullaniciAdi;


                    if (model.kullanici.E_posta!=item.E_posta)
                    {
                            
                            foreach (var i in userManager.Users)
                            {
                                if (i.Email == model.kullanici.E_posta)
                                {
                                    ModelState.AddModelError("", "E-posta adresi zaten alınmış");
                                }
                            }
                    }else item.E_posta = model.kullanici.E_posta;

                    if (model.kullanici.TBL_KullaniciOzellik.InternetSitesi!=null&& model.kullanici.TBL_KullaniciOzellik.InternetSitesi.Length > 200)
                    {
                        
                            ModelState.AddModelError("", "İnternet Sitesi 200 Karekterden Fazla Olamaz.");

                        

                    }
                    else item.TBL_KullaniciOzellik.InternetSitesi = model.kullanici.TBL_KullaniciOzellik.InternetSitesi;

                    if (model.kullanici.TBL_KullaniciOzellik.Biyografi != null)
                    {
                        if (model.kullanici.TBL_KullaniciOzellik.Biyografi.Length > 500)
                        {

                            ModelState.AddModelError("", "Biyografi 500 Karekterden Fazla Olamaz.");


                        }
                        else item.TBL_KullaniciOzellik.Biyografi = model.kullanici.TBL_KullaniciOzellik.Biyografi;
                    }

                    if (uploadfile!= null)
                    {
                        string temp= Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/ProfilPictures"), temp);
                        uploadfile.SaveAs(filePath);
                        item.TBL_KullaniciOzellik.ProfilResmi = "/Content/images/ProfilPictures/" + temp;
                    }


                    //if (model.TBL_KullaniciOzellik.Unvani.Length > 50)
                    //{
                    //    ModelState.AddModelError("", "Unvanı 50 Karekterden Fazla Olamaz.");

                    //}

                    item.Telefon = model.kullanici.Telefon;
                    item.Cinsiyet = model.kullanici.Cinsiyet;
                    db.Entry(item).State = EntityState.Modified;//veritabani guncelleme 
                    db.SaveChanges();
                    if (ModelState.IsValid==true)
                    {
                        ViewBag.success = "Guncelleme İşlemi Başarılı";
                    }

                    //ModelState.IsValid("","Basarılı")
                    model.kullanici = item;
                    return View(model);
                }
            
            
            }

            return View(model);
        }

        public ActionResult IlanSil(int id)
        {
            var sorgu = db.TBL_IndexKart.ToList();
            foreach (var item in sorgu)
            {
                if (id==item.IndexKartID)
                {
                     item.Durum=false;
                    db.Entry(item).State = EntityState.Modified;//veritabani guncelleme 
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Settings","Settings", new {kullaniciAdi= User.Identity.Name });
        }
   
        [HttpPost]
        public ActionResult ProfilSettings(SettingsModel send,int id, HttpPostedFileBase ProfilResim1, HttpPostedFileBase ProfilResim2, HttpPostedFileBase ProfilResim3) //Profil Carousel ve Aciklama bolumu guncelleme
        {
            var sorgu = db.TBL_Kullanici.ToList();
            foreach (var item in sorgu)
            {
                if (id==item.KullaniciID)
                {
                    if (send.kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilAciklama==null)
                    {
                        ModelState.AddModelError("", "Profil Açıklaması Boş olmamalıdır.");
                    }
                    else
                    {
                        item.TBL_Profil.TBL_ProfilIcerik.ProfilAciklama = send.kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilAciklama;
                    }
                    if (ProfilResim1 != null)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilResim1.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/ProfilPictures"), temp);
                        ProfilResim1.SaveAs(filePath);
                        item.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic1 = "/Content/images/ProfilPictures/" + temp;
                    }
                    if (ProfilResim2 != null)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilResim2.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/ProfilPictures"), temp);
                        ProfilResim2.SaveAs(filePath);
                        item.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic2 = "/Content/images/ProfilPictures/" + temp;
                    }
                    if (ProfilResim3 != null)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilResim3.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/ProfilPictures"), temp);
                        ProfilResim3.SaveAs(filePath);
                        item.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic3 = "/Content/images/ProfilPictures/" + temp;
                    }
                    
                    if (ModelState.IsValid == true)
                    {
                        ViewBag.success = "Guncelleme İşlemi Başarılı";
                        db.Entry(item).State = EntityState.Modified;//veritabani guncelleme 
                        db.SaveChanges();
                    }
                        send.kullanici = item;
                    return View("~/Views/Settings/Settings.cshtml", send);

                }

            }
            
            return View();
        }

        [HttpPost]
        public ActionResult ProfilCardAdd(SettingsModel model, string kullaniciAdi, HttpPostedFileBase CardImg)
        {
            var sorgu = db.TBL_Profil.ToList();
            foreach (var item in sorgu)
            {
                if (item.TBL_Kullanici.TBL_KullaniciOzellik.KullaniciAdi == kullaniciAdi)
                {
                    if (CardImg != null)
                    {
                        string temp = Guid.NewGuid().ToString() + "_" + Path.GetFileName(CardImg.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Content/images/CardImages"), temp);
                        CardImg.SaveAs(filePath);
                        model.CardAdd.PKart_Resim = "/Content/images/CardImages/" + temp;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lütfen Resim Yukleyiniz");
                    }
                    if (model.CardAdd.PKart_Baslik.Length>50)
                    {
                        ModelState.AddModelError("", "Kart Başlığı 50 karakterden uzun olamaz");
                    }
                    if (model.CardAdd.PKart_Aciklama.Length > 200)
                    {
                        ModelState.AddModelError("", "Kart Açıklaması 200 karakterden uzun olamaz");
                    }
                    if (ModelState.IsValid)
                    {
                        model.CardAdd.ProfilID = item.ProfilID;
                        db.TBL_ProfilKart.Add(model.CardAdd);
                        //db.Entry(model.CardAdd).State = EntityState.Added;
                        db.SaveChanges();
                        return RedirectToAction("Profil", "Profil", new { kullaniciAdi = kullaniciAdi });
                    }
                    else
                    {
                        model.kullanici = item.TBL_Kullanici;
                        break;
                    }
                    
                    
                }

            }
            
            return View("~/Views/Settings/Settings.cshtml", model);
        }
        public ActionResult resetPP()
        {
            var sorgu = db.TBL_Kullanici.ToList();
            var username = User.Identity.Name;

            foreach (var item in sorgu)
            {
                if (username == item.TBL_KullaniciOzellik.KullaniciAdi)
                {
                    item.TBL_KullaniciOzellik.ProfilResmi = "/Content/Images/default_pp.png";
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return View("Settings",item);
                }
            }
            return View();
        }

    }
}