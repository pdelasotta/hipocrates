//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheekiBreeki.CMH.Terminal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PERS_MEDICO
    {
        public PERS_MEDICO()
        {
            this.ATENCION_AGEN = new HashSet<ATENCION_AGEN>();
            this.RES_ATENCION = new HashSet<RES_ATENCION>();
        }
    
        public int ID_PERSONAL_MEDICO { get; set; }
        public Nullable<int> ID_ESPECIALIDAD { get; set; }
        public Nullable<int> ID_PERSONAL { get; set; }
        public Nullable<int> ID_TURNO { get; set; }
    
        public virtual ICollection<ATENCION_AGEN> ATENCION_AGEN { get; set; }
        public virtual ESPECIALIDAD ESPECIALIDAD { get; set; }
        public virtual ICollection<RES_ATENCION> RES_ATENCION { get; set; }
        public virtual PERSONAL PERSONAL { get; set; }
        public virtual TURNO TURNO { get; set; }
    }
}
