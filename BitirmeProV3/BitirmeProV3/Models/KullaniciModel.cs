using BitirmeProV3.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitirmeProV3.Models
{
   
    public class KullaniciModel
    {
        public KullaniciModel()
        {
            Kullanici = new TBL_Kullanici();
            Kullanici.TBL_KullaniciOzellik = new TBL_KullaniciOzellik();
            Kullanici.TBL_Profil = new TBL_Profil();
            Kullanici.TBL_Profil.TBL_ProfilIcerik = new TBL_ProfilIcerik();
            Kullanici.TBL_KullaniciOzellik.ProfilResmi = "/Content/Images/default_pp.png";
            Kullanici.TBL_KullaniciOzellik.Unvani = "Üye";
            Kullanici.TBL_Profil.OrtalamaYanitSure = 0;
            Kullanici.TBL_Profil.SonGiris = DateTime.Now;
            Kullanici.TBL_Profil.ToplamIsSayisi = 0;
            Kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic1 = "/Content/Images/Carousel_800x400.png";
            Kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic2 = "/Content/Images/Carousel_800x400.png";
            Kullanici.TBL_Profil.TBL_ProfilIcerik.ProfilCarouselPic3 = "/Content/Images/Carousel_800x400.png";
            Kullanici.UyelikTarihi = DateTime.Now;

        }
        public TBL_Kullanici Kullanici { get; set; }
    }
}