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
    
    public partial class BLOQUE
    {
        public BLOQUE()
        {
            this.ATENCION_AGEN = new HashSet<ATENCION_AGEN>();
            this.HORARIO = new HashSet<HORARIO>();
        }
    
        public int ID_BLOQUE { get; set; }
        public int NUM_BLOQUE { get; set; }
        public byte NUM_HORA_INI { get; set; }
        public byte NUM_MINU_INI { get; set; }
        public byte NUM_HORA_FIN { get; set; }
        public byte NUM_MINU_FIN { get; set; }
        public int ID_DIA_SEM { get; set; }
    
        public virtual ICollection<ATENCION_AGEN> ATENCION_AGEN { get; set; }
        public virtual DIA_SEM DIA_SEM { get; set; }
        public virtual ICollection<HORARIO> HORARIO { get; set; }
    }
}