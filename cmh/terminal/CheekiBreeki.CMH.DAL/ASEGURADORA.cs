//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheekiBreeki.CMH.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ASEGURADORA
    {
        public ASEGURADORA()
        {
            this.BONO = new HashSet<BONO>();
        }
    
        public int ID_ASEGURADORA { get; set; }
        public string NOM_ASEGURADORA { get; set; }
    
        public virtual ICollection<BONO> BONO { get; set; }
    }
}
