//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalSkills2017.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tracking
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> OnSystemTime { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }
        public Nullable<bool> Crashe { get; set; }
        public string CrasheReason { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
