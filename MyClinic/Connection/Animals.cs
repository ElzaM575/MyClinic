//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClinic.Connection
{
    using System;
    using System.Collections.Generic;
    
    public partial class Animals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animals()
        {
            this.Reception = new HashSet<Reception>();
        }
    
        public int Id { get; set; }
        public string clinic { get; set; }
        public Nullable<int> Id_Gender { get; set; }
        public Nullable<int> Id_TypeG { get; set; }
        public string Whole { get; set; }
        public string Height { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual Type_Animals Type_Animals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reception> Reception { get; set; }
    }
}
