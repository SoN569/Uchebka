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
    
    public partial class UserOperation
    {
        public int Id { get; set; }
        public Nullable<int> IdOperation { get; set; }
        public string Login { get; set; }
    
        public virtual Operation Operation { get; set; }
        public virtual User User { get; set; }
    }
}
