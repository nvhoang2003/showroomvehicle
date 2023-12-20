//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShowroomManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class service_order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public service_order()
        {
            this.customer_service = new HashSet<customer_service>();
            this.vehicle_service = new HashSet<vehicle_service>();
        }
    
        public int service_id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> time_create { get; set; }
        public string description { get; set; }
        public Nullable<int> manage_by { get; set; }
        public Nullable<int> customer_id { get; set; }
    
        public virtual customer customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customer_service> customer_service { get; set; }
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vehicle_service> vehicle_service { get; set; }
    }
}