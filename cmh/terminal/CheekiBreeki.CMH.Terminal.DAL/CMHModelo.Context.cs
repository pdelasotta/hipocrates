﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMHEntities : DbContext
    {
        public CMHEntities()
            : base("name=CMHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ARCHIVO> ARCHIVO { get; set; }
        public virtual DbSet<ASEGURADORA> ASEGURADORA { get; set; }
        public virtual DbSet<ATENCION_AGEN> ATENCION_AGEN { get; set; }
        public virtual DbSet<BANCO> BANCO { get; set; }
        public virtual DbSet<BONO> BONO { get; set; }
        public virtual DbSet<CAJA> CAJA { get; set; }
        public virtual DbSet<CARGO> CARGO { get; set; }
        public virtual DbSet<CUEN_BANCARIA> CUEN_BANCARIA { get; set; }
        public virtual DbSet<DEVOLUCION> DEVOLUCION { get; set; }
        public virtual DbSet<ENTRADA_FICHA> ENTRADA_FICHA { get; set; }
        public virtual DbSet<EQUIPO_REQ> EQUIPO_REQ { get; set; }
        public virtual DbSet<ESPECIALIDAD> ESPECIALIDAD { get; set; }
        public virtual DbSet<ESTADO_ATEN> ESTADO_ATEN { get; set; }
        public virtual DbSet<FICHA> FICHA { get; set; }
        public virtual DbSet<FUNCIONARIO> FUNCIONARIO { get; set; }
        public virtual DbSet<INVENTARIO> INVENTARIO { get; set; }
        public virtual DbSet<ORDEN_ANALISIS> ORDEN_ANALISIS { get; set; }
        public virtual DbSet<PACIENTE> PACIENTE { get; set; }
        public virtual DbSet<PAGO> PAGO { get; set; }
        public virtual DbSet<PERS_MEDICO> PERS_MEDICO { get; set; }
        public virtual DbSet<PERSONAL> PERSONAL { get; set; }
        public virtual DbSet<PRESTACION> PRESTACION { get; set; }
        public virtual DbSet<PRUEBA> PRUEBA { get; set; }
        public virtual DbSet<RES_ATENCION> RES_ATENCION { get; set; }
        public virtual DbSet<TIPO_C_BANCARIA> TIPO_C_BANCARIA { get; set; }
        public virtual DbSet<TIPO_EQUIPO> TIPO_EQUIPO { get; set; }
        public virtual DbSet<TIPO_PRES> TIPO_PRES { get; set; }
        public virtual DbSet<TURNO> TURNO { get; set; }
    }
}
