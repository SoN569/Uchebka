//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uchebka.Components
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.DocOrder = new HashSet<DocOrder>();
            this.HistoryStatus = new HashSet<HistoryStatus>();
            this.Quality = new HashSet<Quality>();
            this.Size = new HashSet<Size>();
        }
    
        public string Id { get; set; }
        public System.DateTime DateOrder { get; set; }
        public string Name { get; set; }
        public Nullable<int> IdProduct { get; set; }
        public string LoginCustomer { get; set; }
        public string LoginManager { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<int> IdStatus { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocOrder> DocOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoryStatus> HistoryStatus { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quality> Quality { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Size> Size { get; set; }
    }
}
