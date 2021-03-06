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
    
    public partial class TBL_IndexKart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_IndexKart()
        {
            this.TBL_IndexCardComment = new HashSet<TBL_IndexCardComment>();
        }
    
        public int IndexKartID { get; set; }
        public string KartAciklamasi { get; set; }
        public Nullable<int> Fiyat { get; set; }
        public Nullable<bool> AliciSatici { get; set; }
        public Nullable<bool> Durum { get; set; }
        public string KartResim1 { get; set; }
        public string KartResim2 { get; set; }
        public string KartResim3 { get; set; }
        public Nullable<int> KullaniciID { get; set; }
        public Nullable<int> KategoriID { get; set; }
        public string KartBaslik { get; set; }
    
        public virtual TBL_Kategori TBL_Kategori { get; set; }
        public virtual TBL_Kullanici TBL_Kullanici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_IndexCardComment> TBL_IndexCardComment { get; set; }
    }
}
