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
    
    public partial class PAGO
    {
        public PAGO()
        {
            this.ATENCION_AGEN = new HashSet<ATENCION_AGEN>();
        }
    
        public int ID_PAGO { get; set; }
        public Nullable<System.DateTime> HORA_PAGO { get; set; }
        public Nullable<int> ID_BONO { get; set; }
        public Nullable<int> ID_CAJA { get; set; }
        public Nullable<int> ID_DEVOLUCION { get; set; }
    
        public virtual ICollection<ATENCION_AGEN> ATENCION_AGEN { get; set; }
        public virtual BONO BONO { get; set; }
        public virtual CAJA CAJA { get; set; }
        public virtual DEVOLUCION DEVOLUCION { get; set; }
    }
}
