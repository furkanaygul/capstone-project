//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BitirmeProV3.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ProfilIcerik
    {
        public int ProfilIcerikID { get; set; }
        public string ProfilCarouselPic1 { get; set; }
        public string ProfilCarouselPic2 { get; set; }
        public string ProfilCarouselPic3 { get; set; }
        public string ProfilAciklama { get; set; }
    
        public virtual TBL_Profil TBL_Profil { get; set; }
    }
}
