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
    
    public partial class PACIENTE
    {
        public PACIENTE()
        {
            this.ATENCION_AGEN = new HashSet<ATENCION_AGEN>();
            this.FICHA = new HashSet<FICHA>();
        }
    
        public int ID_PACIENTE { get; set; }
        public string NOMBRES_PACIENTE { get; set; }
        public string APELLIDOS_PACIENTE { get; set; }
        public int RUT { get; set; }
        public string HASHED_PASS { get; set; }
        public string EMAIL_PACIENTE { get; set; }
        public string DIGITO_VERIFICADOR { get; set; }
    
        public virtual ICollection<ATENCION_AGEN> ATENCION_AGEN { get; set; }
        public virtual ICollection<FICHA> FICHA { get; set; }
    }
}
