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
    
    public partial class ARCHIVO
    {
        public int ID_ARCHIVO { get; set; }
        public string ARCHIVO_B64 { get; set; }
        public Nullable<int> ID_RESULTADO_ATENCION { get; set; }
        public string EXTENSION { get; set; }
    
        public virtual RES_ATENCION RES_ATENCION { get; set; }
    }
}
