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
    
    public partial class ATENCION_AGEN
    {
        public ATENCION_AGEN()
        {
            this.RES_ATENCION = new HashSet<RES_ATENCION>();
            this.PAGO = new HashSet<PAGO>();
        }
    
        public int ID_ATENCION_AGEN { get; set; }
        public Nullable<System.DateTime> FECHOR { get; set; }
        public string OBSERVACIONES { get; set; }
        public Nullable<int> ID_PACIENTE { get; set; }
        public Nullable<int> ID_PRESTACION { get; set; }
        public Nullable<int> ID_ESTADO_ATEN { get; set; }
        public int ID_PERS_ATIENDE { get; set; }
        public Nullable<int> ID_PERS_SOLICITA { get; set; }
        public int ID_BLOQUE { get; set; }
    
        public virtual BLOQUE BLOQUE { get; set; }
        public virtual PERS_MEDICO PERS_MEDICO { get; set; }
        public virtual PERS_MEDICO PERS_MEDICO1 { get; set; }
        public virtual PRESTACION PRESTACION { get; set; }
        public virtual ICollection<RES_ATENCION> RES_ATENCION { get; set; }
        public virtual ESTADO_ATEN ESTADO_ATEN { get; set; }
        public virtual PACIENTE PACIENTE { get; set; }
        public virtual ICollection<PAGO> PAGO { get; set; }
    }
}
