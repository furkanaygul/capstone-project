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
    
    public partial class TBL_Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Kategori()
        {
            this.TBL_IndexKart = new HashSet<TBL_IndexKart>();
        }
    
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public Nullable<bool> KategoriDurumu { get; set; }
        public Nullable<int> KategoriRank { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_IndexKart> TBL_IndexKart { get; set; }
    }
}