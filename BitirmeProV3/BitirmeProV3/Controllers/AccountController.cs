using BitirmeProV3.Identity;
using BitirmeProV3.Models;
using BitirmeProV3.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitirmeProV3.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private RoleManager<IdentityRole> roleManager;//sonradan
        private UserManager<ApplicationUser> userManager;
        // GET: Account

        public AccountController()
        {


            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireLowercase = true
            };

        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }





        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();


                var kullanici = new KullaniciModel();



                var user = new ApplicationUser(); //identity icin yeni nesne olusturuyoruz.Applicationuser ust cs dan miras aliyor
                user.Name = model.name;//htmlden post edilen degerleri hem veritabani hemde identity icin gecici nesneye atiyoruz.
                user.Surname = model.surname;
                kullanici.Kullanici.Adi = (user.Name) + (" ") + (user.Surname);
                kullanici.Kullanici.E_posta = user.Email = model.Email;
                user.UserName = model.Username;
                kullanici.Kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilIcerikID = kullanici.Kullanici.TBL_Profil.ProfilID = kullanici.Kullanici.TBL_KullaniciOzellik.KullaniciOzellikID = kullanici.Kullanici.KullaniciID;//kullaniciozellikId otomatik artmiyor digerinin idsini atiyoruz.

                kullanici.Kullanici.TBL_KullaniciOzellik.KullaniciAdi = user.UserName;


                user.PasswordControl = model.PasswordControl;
                user.checkbox1 = model.checkbox1;
                user.checkbox2 = model.checkbox2;


                if ((model.Password == model.PasswordControl) && (model.checkbox1 == true) && (EmailCheck(model)))
                {
                    var result = userManager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        db.TBL_Kullanici.Add(kullanici.Kullanici);

                        //db.TBL_KullaniciOzellik.Add(kullaniciOzellik);
                        db.SaveChanges();
                        userManager.AddToRole(user.Id, "User");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {

                            ModelState.AddModelError("", error);
                        }
                    }
                }
                else
                {
                    if (model.Password != model.PasswordControl)
                    {
                        ModelState.AddModelError("", "şifreler uyuşmuyor");
                    }
                    if (model.checkbox1 == false)
                    {
                        ModelState.AddModelError("", "Kullanıcı Sözleşmesi Kabul Edilmedi");
                    }
                }
            }
            return View(model);
        }





        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Erisim Hakkiniz Yok" });
            }


            ViewBag.returnUrl = returnUrl;
            return View();
        }





        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var user = userManager.Find(model.Username, model.Password);//gelen kullanici adi ve sifreyi find metodu ile sorguluyoruz

                if (user == null)//gelen kullanici yoksa hata versin
                {
                    ModelState.AddModelError("", "Yanlış Kullanıcı Adı veya Parola");
                }
                else//Kullanicinin tarayicisina cookie yerlestirdik
                {
                    if (user.Roles.Count == 0)
                    {
                        ModelState.AddModelError("", "Hesabiniz Engellendi");
                        ViewBag.returnUrl = returnUrl;
                        return View();
                    }

                    BitirmeProjesiDBEntities db = new BitirmeProjesiDBEntities();
                    var sorgu = db.TBL_Kullanici.ToList();
                    foreach (var item in sorgu)
                    {
                        if (item.TBL_KullaniciOzellik.KullaniciAdi == model.Username)
                        {
                            item.TBL_Profil.SonGiris = DateTime.Now;
                            db.Entry(item).State = EntityState.Modified;//veritabani guncelleme 
                            db.SaveChanges();
                            break;
                        }
                    }
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");//cookie olusturduk
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true

                    };
                    authManager.SignOut();//kullanici daha onceden giris yapmissa once bir silelim
                    authManager.SignIn(authProperties, identity);

                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }



        public ActionResult Logout()
        {
            var authManeger = HttpContext.GetOwinContext().Authentication;
            authManeger.SignOut();
            return RedirectToAction("Login");
        }





        //E-Posta sorgulama fonksyonu
        private bool EmailCheck(Register model)
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            foreach (var item in userManager.Users)
            {
                if (item.Email == model.Email)
                {
                    ModelState.AddModelError("", "E-posta adresi zaten alınmış");
                    return false;

                }
            }
            return true;
        }
    }
}