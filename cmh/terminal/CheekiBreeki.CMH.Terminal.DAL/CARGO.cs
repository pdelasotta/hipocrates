//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheekiBreeki.CMH.Terminal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CARGO
    {
        public CARGO()
        {
            this.FUNCIONARIO = new HashSet<FUNCIONARIO>();
        }
    
        public int ID_CARGO_FUNCI { get; set; }
        public string NOMBRE_CARGO { get; set; }
    
        public virtual ICollection<FUNCIONARIO> FUNCIONARIO { get; set; }
    }
}
