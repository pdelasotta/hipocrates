//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cheekibreeki.CMH.Seguro.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AFILIADO
    {
        public int ID_AFILIADO { get; set; }
        public Nullable<int> RUT { get; set; }
        public string VERIFICADOR { get; set; }
        public Nullable<int> ID_PLAN { get; set; }
    
        public virtual PLAN PLAN { get; set; }
    }
}