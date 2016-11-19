﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheekiBreeki.CMH.Terminal.DAL;
using CheekiBreeki.CMH.Terminal.BL.SeguroServiceReference;
using System.IO;
using System.Security.Cryptography;
using System.Data.Entity;

namespace CheekiBreeki.CMH.Terminal.BL
{
    public class AccionesTerminal
    {
        CMHEntities conexionDB = new CMHEntities();

        //ECU-001
        public Boolean agendarAtencion(ATENCION_AGEN atencion)
        {

            try
            {
                if (Util.isObjetoNulo(atencion))
                {
                    throw new Exception("Atencion invalida");
                }
                else if (atencion.FECHOR == DateTime.MinValue ||
                         atencion.FECHOR == null)
                {
                    throw new Exception("Fecha vacía");
                }
                else
                {
                    conexionDB.ATENCION_AGEN.Add(atencion);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-005
        public List<ATENCION_AGEN> revisarAgendaDiaria(int rut, DateTime dia)
        {

            try
            {
                if (Util.isObjetoNulo(dia))
                {
                    throw new Exception("Día vacío");
                }
                else if (Util.isObjetoNulo(buscarPersonal(rut)))
                {
                    throw new Exception("Personal no existe");
                }
                else
                {
                    List<ATENCION_AGEN> atenciones = null;
                    atenciones = conexionDB.ATENCION_AGEN.
                        Where(d => d.PERS_MEDICO.PERSONAL.RUT == rut &&
                              d.FECHOR == dia).ToList();
                    return atenciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //ECU-006
        public Boolean ingresarPaciente(ATENCION_AGEN atencion)
        {
            try
            {
                if (Util.isObjetoNulo(atencion))
                {
                    throw new Exception("Atención nula");
                }
                if (atencion.ESTADO_ATEN.NOM_ESTADO_ATEN.ToUpper() != "VIGENTE")
                {
                    throw new Exception("Estado no válido de la atención");
                }
                else if (Util.isObjetoNulo(conexionDB.ATENCION_AGEN.Find(atencion.ID_ATENCION_AGEN)))
                {
                    throw new Exception("Atención agendada no existe");
                }
                else
                {
                    ATENCION_AGEN atencionFinal = conexionDB.ATENCION_AGEN.Find(atencion.ID_ATENCION_AGEN);
                    atencionFinal.ID_ESTADO_ATEN = conexionDB.ESTADO_ATEN.Where(d => d.NOM_ESTADO_ATEN.ToUpper() == "PAGADO").FirstOrDefault().ID_ESTADO_ATEN;
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-007
        public ResultadoVerificacionSeguro verificarSeguro(PRESTACION prestacion, PACIENTE paciente)
        {

            int precioPrestacion = prestacion.PRECIO_PRESTACION.Value;
            int rutPaciente = paciente.RUT;
            SeguroWSClient client = new SeguroWSClient();
            SeguroRequest request = new SeguroRequest();
            request.AfiliadoRut = paciente.RUT;
            request.CodigoPrestacion = prestacion.CODIGO_PRESTACION;
            Task<SeguroResponse> tResponse = client.obtenerDescuentoAsync(request);
            tResponse.Wait();
            SeguroResponse response = tResponse.Result;
            ResultadoVerificacionSeguro resultado = new ResultadoVerificacionSeguro();
            resultado.TieneSeguro = response.AfiliadoTieneSeguro;
            resultado.Descuento = response.DescuentoPesos;
            return resultado;
        }

        //ECU-008
        public Boolean registrarPago(PAGO pago)
        {
            try
            {
                if (Util.isObjetoNulo(pago))
                {
                    throw new Exception("Pago nulo");
                }
                else if (Util.isObjetoNulo(conexionDB.BONO.Find(pago.ID_BONO)))
                {
                    throw new Exception("Bono no existe");
                }
                else if (Util.isObjetoNulo(conexionDB.CAJA.Find(pago.ID_CAJA)))
                {
                    throw new Exception("Caja no existe");
                }
                else if (Util.isObjetoNulo(conexionDB.ATENCION_AGEN.Find(pago.ID_ATENCION_AGEN)))
                {
                    throw new Exception("Atención agendada no existe");
                }
                else
                {
                    conexionDB.PAGO.Add(pago);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-010
        public Boolean agregarEntradaFicha(ENTRADA_FICHA entradaFicha)
        {
            try
            {
                if (entradaFicha.CONTENIDO_ENTRADA == "" || entradaFicha.CONTENIDO_ENTRADA == null)
                {
                    throw new Exception("Contenido de entrada nula o vacía");
                }
                else if (entradaFicha.FECHA_ENTRADA == null)
                {
                    throw new Exception("Fecha de entrada nula o vacía");
                }
                else
                {
                    using (var con = new CMHEntities()) {
                        con.ENTRADA_FICHA.Add(entradaFicha);
                        con.SaveChangesAsync();
                    }
                    return true;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-011
        #region Cerrar consulta médica
        /// <summary>
        /// Se cierra una consulta médica con estado de atención "Vigente"
        /// </summary>
        /// <param name="resultadoAtencion">Resultado de dicha atención</param>
        /// <param name="atencionAgendada">Atención agendada para cambiarle la llave foranea de estado atención</param>
        /// <returns></returns>
        public Boolean cerrarConsultaMedica(RES_ATENCION resultadoAtencion, ATENCION_AGEN atencionAgendada)
        {
            try
            {
                if (Util.isObjetoNulo(atencionAgendada))
                {
                    throw new Exception("Atención nula");
                }
                else if (atencionAgendada.ESTADO_ATEN.NOM_ESTADO_ATEN.ToUpper() != "VIGENTE")
                {
                    throw new Exception("Estado no válido de la atención");
                }
                else
                {
                    ATENCION_AGEN atencionFinal = conexionDB.ATENCION_AGEN.Find(atencionAgendada.ID_ATENCION_AGEN);
                    atencionFinal.ID_ESTADO_ATEN = conexionDB.ESTADO_ATEN.Where(d => d.NOM_ESTADO_ATEN.ToUpper() == "PAGADO").FirstOrDefault().ID_ESTADO_ATEN;
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //ECU-012
        public Boolean generarOrdenDeAnalisis(ATENCION_AGEN atencion, RES_ATENCION resultadoAtencion)
        {
            try
            {
                if (Util.isObjetoNulo(atencion))
                {
                    throw new Exception("Atencion agendada nula");
                }

                else if (atencion.FECHOR == null)
                {
                    throw new Exception("Fecha nula");
                }

                else if (resultadoAtencion.ID_ATENCION_AGEN == null)
                {
                    throw new Exception("ID de atencion agendada es nulo");
                }

                else
                {
                    ORDEN_ANALISIS ordenAnalisis = new ORDEN_ANALISIS();
                    ordenAnalisis.FECHOR_EMISION = DateTime.Today;
                    conexionDB.ORDEN_ANALISIS.Add(ordenAnalisis);
                    conexionDB.SaveChangesAsync();

                    using (var con = new CMHEntities())
                    {
                        resultadoAtencion = con.RES_ATENCION.Find(resultadoAtencion.ID_RESULTADO_ATENCION);
                        resultadoAtencion.ID_ORDEN_ANALISIS = ordenAnalisis.ID_ORDEN_ANALISIS;
                        con.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-013
        public Boolean cerrarOrdenDeAnalisis(ORDEN_ANALISIS ordenAnalisis, RES_ATENCION resultadoAtencion)
        {
            try
            {
                if (Util.isObjetoNulo(ordenAnalisis))
                {
                    throw new Exception("Orden nula");
                }
                else
                {
                    using (var con = new CMHEntities())
                    {
                        ordenAnalisis = con.ORDEN_ANALISIS.Find(ordenAnalisis.ID_ORDEN_ANALISIS);
                        ordenAnalisis.FECHOR_RECEP = DateTime.Today;
                        con.SaveChangesAsync();
                    }
                    using (var con = new CMHEntities())
                    {
                        resultadoAtencion = con.RES_ATENCION.Find(resultadoAtencion.ID_RESULTADO_ATENCION);
                        resultadoAtencion.ATENCION_ABIERTA = false;
                        con.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean cerrarOrdenDeAnalisis(ORDEN_ANALISIS ordenAnalisis, string archivo)
        {
            try
            {
                if (Util.isObjetoNulo(ordenAnalisis))
                {
                    throw new Exception("Orden nula");
                }

                else if (ordenAnalisis.FECHOR_RECEP <= DateTime.Today)
                {
                    throw new Exception("Fecha invalida");
                }
                else
                {
                    ordenAnalisis = conexionDB.ORDEN_ANALISIS.Find(ordenAnalisis.ID_ORDEN_ANALISIS);
                    ordenAnalisis.FECHOR_RECEP = DateTime.Today;
                    conexionDB.SaveChangesAsync();

                    if (ordenAnalisis.RES_ATENCION.FirstOrDefault().ATENCION_AGEN.ID_PERS_SOLICITA != null)
                    {
                        string receptor, titulo, cuerpo;
                        receptor = ordenAnalisis.RES_ATENCION.FirstOrDefault().ATENCION_AGEN.PERS_MEDICO1.PERSONAL.EMAIL;
                        titulo = "Cerrada orden de análisis";
                        cuerpo = "La orden de análisis con número " + ordenAnalisis.ID_ORDEN_ANALISIS + " se ha cerrado";
                        if (File.Exists(archivo))
                        {
                            Emailer.enviarCorreo(receptor, titulo, cuerpo, archivo);
                            Console.WriteLine("Correo enviado");
                        }
                        else
                        {
                            Console.WriteLine("Archivo no existente");
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-016
        public Boolean anularAtencion(ATENCION_AGEN atencion)
        {
            try
            {
                if (Util.isObjetoNulo(atencion))
                {
                    throw new Exception("Atencion invalida");
                }
                else if (atencion.FECHOR == DateTime.MinValue ||
                         atencion.FECHOR == null)
                {
                    throw new Exception("Fecha vacía");
                }
                else if (atencion.OBSERVACIONES == String.Empty ||
                         atencion.OBSERVACIONES == null)
                {
                    throw new Exception("Observacion vacia");
                }
                else
                {
                    ESTADO_ATEN estadoatencion = new ESTADO_ATEN();
                    estadoatencion.NOM_ESTADO_ATEN = "ANULADO";
                    estadoatencion = conexionDB.ESTADO_ATEN.Where(d => d.NOM_ESTADO_ATEN == estadoatencion.NOM_ESTADO_ATEN).FirstOrDefault();

                    atencion = conexionDB.ATENCION_AGEN.Find(atencion.ID_ATENCION_AGEN);
                    atencion.ID_ESTADO_ATEN = estadoatencion.ID_ESTADO_ATEN;
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ECU-017
        /// <summary>
        /// Se registra una caja con el respectivo funcionario que la abrio.
        /// </summary>
        /// <param name="caja">La caja</param>
        /// <param name="funcionario">Funcionario que abre la caja</param>
        /// <returns>Si es true la caja fue abierta con exito</returns>
        #region  Abrir Caja
        public Boolean abrirCaja(CAJA caja, FUNCIONARIO funcionario)
        {
            try
            {
                if (Util.isObjetoNulo(caja))
                {
                    throw new Exception("Caja nula.");
                }
                else if (Util.isObjetoNulo(buscarFuncionario(funcionario.ID_CARGO_FUNCI, funcionario.ID_PERSONAL)))
                {
                    throw new Exception("Funcionario no encontrado.");
                }
                else
                {
                    conexionDB.CAJA.Add(caja);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// Busca una caja entre las cajas existentes.
        /// </summary>
        /// <param name="idCaja">Id de la caja que se quiere buscar</param>
        /// <returns>La caja encontrada, si es null la caja no se encontro</returns>
        #region Buscar caja
        public CAJA buscarCaja(int idCaja)
        {
            try
            {
                CAJA caja = null;
                caja = conexionDB.CAJA.Where(d => d.ID_CAJA == idCaja).FirstOrDefault();
                if (Util.isObjetoNulo(caja))
                {
                    throw new Exception("Caja no existe");
                }
                return caja;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion

        //ECU-018
        /// <summary>
        /// Metodo que cierra una caja. 
        /// Este metodo utiliza el metodo de buscar caja para verificar que esta exista previamente.
        /// </summary>
        /// <param name="caja">Caja que se quiere cerrar</param>
        /// <param name="fechor_cierre">Fecha en la que se cierra la caja</param>
        /// <returns>Si es true la caja fue cerrada con exito</returns>
        #region Cerrar Caja
        public Boolean cerrarCaja(CAJA caja, DateTime fechor_cierre, CARGO cargoAuditor)
        {
            try
            {
                //Verificar si caja existe
                bool cajaNula = Util.isObjetoNulo(buscarCaja(caja.ID_CAJA));
                if (cajaNula)
                {
                    throw new Exception("Caja nulo");
                }
                //VERIFICAR HORA DE CIERRE PARA VER SI ESTA CERRADA O NO
                else if (caja.FECHOR_APERTURA == null)
                {
                    throw new Exception("Fecha y hora apertura nula");
                }
                else
                {
                    if (caja.FECHOR_CIERRE == null)
                    {
                        caja = buscarCaja(caja.ID_CAJA);
                        caja.FECHOR_CIERRE = fechor_cierre;
                        //si caja descuadrada entonces enviar correo                      
                        conexionDB.SaveChangesAsync();
                        this.auditarCaja(caja, cargoAuditor);
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Si una caja presenta diferencias, envia un correo notificando a todos los funcionarios con el cargo especificado
        /// </summary>
        /// <param name="caja">Caja a evaluar</param>
        /// <param name="nombreCargo">Nombre del cargo a notificar</param>
        /// <returns>Cantidad de mails enviados</returns>
        public int auditarCaja(CAJA caja, CARGO cargo)
        {
            using (var cmhEntities = new CMHEntities())
            {
                ReporteCaja reporteCaja = new ReporteCaja(caja);
                int result = 0;
                if (reporteCaja.Diferencia != 0)
                {
                    //si caja está descuadrada, entonces mandar mail a jefes de operadores
                    ICollection<FUNCIONARIO> jefesOperadores = cargo.FUNCIONARIO;
                    String tituloMail = "Caja descuadrada";
                    foreach (FUNCIONARIO jefe in jefesOperadores)
                    {
                        String cuerpoMail = "Estimado " + jefe.PERSONAL.NOMBRES +
                            " se le comunica que " + caja.FUNCIONARIO.PERSONAL.NOMBRES +
                            " " + caja.FUNCIONARIO.PERSONAL.APELLIDOS +
                            " ha registrado una inconsistencia en su caja con diferencia de " +
                            reporteCaja.Diferencia + " en la fecha " + reporteCaja.FechorCierre;
                        Emailer.enviarCorreo(jefe.PERSONAL.EMAIL, tituloMail, cuerpoMail);
                        result++;
                    }
                }
                return result;
            }
        }

        #endregion

        //ECU-019
        public List<ReporteCaja> generarReporteCaja(FUNCIONARIO funcionario, DateTime dia)
        {
            using (var context = new CMHEntities())
            {
                //Todas las cajas del funcionario
                ICollection<CAJA> cajasFuncionario = funcionario.CAJA;
                //Si no hay cajas, levantar excepcion
                if (cajasFuncionario == null || cajasFuncionario.Count() == 0)
                {
                    throw new Exception("No hay cajas para este funcionario");
                }
                //Filtrar por día
                List<CAJA> cajasDelDia = new List<CAJA>();
                foreach (CAJA caja in cajasFuncionario)
                {
                    if (caja.FECHOR_CIERRE.Value.Date == dia.Date)
                    {
                        cajasDelDia.Add(caja);
                    }
                }
                List<ReporteCaja> reportesCaja = new List<ReporteCaja>();
                //Instanciar un reporte de caja por caja
                foreach (CAJA caja in cajasDelDia)
                {
                    ReporteCaja reporteCaja = new ReporteCaja(caja);
                    reportesCaja.Add(reporteCaja);
                }
                return reportesCaja;
            }
        }

        //ECU-022
        #region Equipos
        public Boolean nuevoEquipo(TIPO_EQUIPO equipo)
        {
            try
            {
                if (Util.isObjetoNulo(equipo))
                {
                    throw new Exception("Personal nulo");
                }
                else if (equipo.NOMBRE_TIPO_EQUIPO == null ||
                         equipo.NOMBRE_TIPO_EQUIPO == String.Empty)
                {
                    throw new Exception("Nombre vacío");
                }
                else if (!Util.isObjetoNulo(buscarEquipo(equipo.NOMBRE_TIPO_EQUIPO)))
                {
                    throw new Exception("Equipo ya ingresado");
                }
                else
                {
                    conexionDB.TIPO_EQUIPO.Add(equipo);
                    Task<int> insercion = conexionDB.SaveChangesAsync();
                    insercion.Wait();

                    INVENTARIO inventario = new INVENTARIO();
                    inventario.CANT_BODEGA = 0;
                    inventario.ID_TIPO_EQUIPO = buscarEquipo(equipo.NOMBRE_TIPO_EQUIPO).ID_TIPO_EQUIPO;
                    conexionDB.INVENTARIO.Add(inventario);
                    conexionDB.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public TIPO_EQUIPO buscarEquipo(string nombre)
        {
            try
            {
                if (Util.isObjetoNulo(nombre))
                {
                    throw new Exception("Nombre nulo");
                }
                else
                {
                    TIPO_EQUIPO equipo = null;
                    equipo = conexionDB.TIPO_EQUIPO.Where(d => d.NOMBRE_TIPO_EQUIPO == nombre)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(equipo))
                    {
                        throw new Exception("Equipo no existe");
                    }
                    return equipo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean actualizarEquipoCantidad(string nombre, int cantidad)
        {
            try
            {
                if (Util.isObjetoNulo(nombre))
                {
                    throw new Exception("Nombre nulo");
                }
                else
                {
                    TIPO_EQUIPO equipo = null;
                    equipo = conexionDB.TIPO_EQUIPO.Where(d => d.NOMBRE_TIPO_EQUIPO == nombre)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(equipo))
                    {
                        throw new Exception("Equipo no existe");
                    }

                    INVENTARIO inventario = conexionDB.INVENTARIO.Where(d => d.ID_TIPO_EQUIPO == equipo.ID_TIPO_EQUIPO)
                                                                        .FirstOrDefault();
                    if (Util.isObjetoNulo(inventario))
                    {
                        throw new Exception("Inventario no existe");
                    }
                    inventario.CANT_BODEGA = cantidad;
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean borrarEquipo(TIPO_EQUIPO equipo)
        {
            try
            {
                if (Util.isObjetoNulo(equipo))
                {
                    throw new Exception("Paciente no existe");
                }
                else
                {
                    conexionDB.TIPO_EQUIPO.Remove(equipo);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //ECU-023 y ECU-026
        #region Personal
        public Boolean nuevoPersonal(PERSONAL personal)
        {
            try
            {
                if (Util.isObjetoNulo(personal))
                {
                    throw new Exception("Personal nulo");
                }
                else if (personal.NOMBRES == null ||
                         personal.APELLIDOS == null ||
                         personal.NOMBRES == String.Empty ||
                         personal.APELLIDOS == String.Empty)
                {
                    throw new Exception("Nombre o apellido vacío");
                }
                else if (personal.RUT == null || personal.RUT == 0)
                {
                    throw new Exception("RUT vacío");
                }
                else if (!Util.isObjetoNulo(buscarPersonal(personal.RUT, personal.VERIFICADOR)))
                {
                    throw new Exception("Personal ya ingresado");
                }
                else
                {
                    conexionDB.PERSONAL.Add(personal);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public PERS_MEDICO buscarPersonalMedico(PERSONAL personal)
        {
            try
            {
                if (Util.isObjetoNulo(personal))
                {
                    throw new Exception("Personal vacío");
                }
                else
                {
                    PERS_MEDICO personalMed = null;
                    personalMed = conexionDB.PERS_MEDICO.Where(d => d.ID_PERSONAL == personal.ID_PERSONAL).FirstOrDefault();
                    //personalMed.HORARIO = conexionDB.HORARIO.Where(d => d.ID_PERS_MEDICO == personalMed.ID_PERSONAL_MEDICO).ToList();
                    if (Util.isObjetoNulo(personal))
                    {
                        throw new Exception("Personal no existe");
                    }
                    return personalMed;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public PERSONAL buscarPersonal(PERSONAL personal)
        {
            try
            {
                if (Util.isObjetoNulo(personal))
                {
                    throw new Exception("RUT o vacío");
                }
                else
                {
                    PERSONAL encontrado = conexionDB.PERSONAL.Find(personal.ID_PERSONAL);
                    if (Util.isObjetoNulo(personal))
                    {
                        throw new Exception("Personal no existe");
                    }
                    return encontrado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public PERSONAL buscarPersonal(int rut)
        {
            try
            {
                if (Util.isObjetoNulo(rut))
                {
                    throw new Exception("RUT o vacío");
                }
                else
                {
                    PERSONAL personal = null;
                    personal = conexionDB.PERSONAL.Where(d => d.RUT == rut).FirstOrDefault();
                    if (Util.isObjetoNulo(personal))
                    {
                        throw new Exception("Personal no existe");
                    }
                    return personal;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public PERSONAL buscarPersonal(int rut, string dv)
        {
            try
            {
                if (Util.isObjetoNulo(rut) || Util.isObjetoNulo(dv))
                {
                    throw new Exception("RUT o dígito verificador nulo");
                }
                else
                {
                    PERSONAL personal = null;
                    personal = conexionDB.PERSONAL.Where(d => d.RUT == rut
                                                         && d.VERIFICADOR == dv)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(personal))
                    {
                        throw new Exception("Personal no existe");
                    }
                    return personal;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean actualizarPersonal(PERSONAL personal)
        {
            try
            {
                if (Util.isObjetoNulo(personal))
                {
                    throw new Exception("Personal nulo");
                }
                else if (personal.NOMBRES == null ||
                         personal.APELLIDOS == null ||
                         personal.NOMBRES == String.Empty ||
                         personal.APELLIDOS == String.Empty)
                {
                    throw new Exception("Nombre o apellido vacío");
                }
                else
                {
                    using (var context = new CMHEntities())
                    {
                        PERSONAL buscado = context.PERSONAL.Where(d => d.ID_PERSONAL == personal.ID_PERSONAL).FirstOrDefault();
                        buscado.EMAIL = personal.EMAIL;
                        buscado.NOMBRES = personal.NOMBRES;
                        buscado.APELLIDOS = personal.APELLIDOS;
                        buscado.HASHED_PASS = personal.HASHED_PASS;
                        buscado.PORCENT_DESCUENTO = personal.PORCENT_DESCUENTO;
                        buscado.REMUNERACION = personal.REMUNERACION;
                        buscado.RUT = personal.RUT;
                        buscado.VERIFICADOR = personal.VERIFICADOR;
                        buscado.ACTIVO = personal.ACTIVO;
                        context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean borrarPersonal(PERSONAL personal)
        {
            try
            {
                if (Util.isObjetoNulo(personal))
                {
                    throw new Exception("Paciente no existe");
                }
                else
                {
                    if (personal.FUNCIONARIO.Count > 0 && personal.FUNCIONARIO.FirstOrDefault().CARGO.NOMBRE_CARGO == "Jefe de operadores")
                    {
                        if (conexionDB.FUNCIONARIO.Where(d => d.CARGO.NOMBRE_CARGO == "Jefe de operadores").Count() <= 1)
                            throw new Exception("Tiene que haber por lo menos un jefe de operadores");
                    }
                    conexionDB.PERSONAL.Remove(personal);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Funcionarios
        public Boolean nuevoFuncionario(FUNCIONARIO funcionario)
        {
            try
            {
                if (Util.isObjetoNulo(funcionario))
                {
                    throw new Exception("Funcionario nulo");
                }
                else if (funcionario.ID_CARGO_FUNCI == null ||
                         funcionario.ID_CARGO_FUNCI == 0 ||
                         funcionario.ID_PERSONAL == null ||
                         funcionario.ID_PERSONAL == 0)
                {
                    throw new Exception("Cargo o personal no vacío");
                }
                else if (Util.isObjetoNulo(conexionDB.CARGO.Find(funcionario.ID_CARGO_FUNCI)))
                {
                    throw new Exception("Cargo no existe");
                }
                else if (!Util.isObjetoNulo(buscarFuncionario(funcionario.ID_CARGO_FUNCI, funcionario.ID_PERSONAL)))
                {
                    throw new Exception("Funcionario ya ingresado");
                }
                else
                {
                    conexionDB.FUNCIONARIO.Add(funcionario);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public FUNCIONARIO buscarFuncionario(int cargo, int personal)
        {
            try
            {
                if (Util.isObjetoNulo(cargo) || Util.isObjetoNulo(personal))
                {
                    throw new Exception("Cargo o personal nulo");
                }
                else
                {
                    FUNCIONARIO funcionario = null;
                    funcionario = conexionDB.FUNCIONARIO.Where(d => d.ID_CARGO_FUNCI == cargo
                                                         && d.ID_PERSONAL == personal)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(funcionario))
                    {
                        throw new Exception("Funcionario no existe");
                    }
                    return funcionario;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean actualizarFuncionario(FUNCIONARIO funcionario)
        {
            try
            {
                if (Util.isObjetoNulo(funcionario))
                {
                    throw new Exception("Funcionario nulo");
                }
                else if (funcionario.ID_CARGO_FUNCI == null ||
                         funcionario.ID_CARGO_FUNCI == 0 ||
                         funcionario.ID_PERSONAL == null ||
                         funcionario.ID_PERSONAL == 0)
                {
                    throw new Exception("Cargo o personal no vacío");
                }
                else if (Util.isObjetoNulo(conexionDB.CARGO.Find(funcionario.ID_CARGO_FUNCI)))
                {
                    throw new Exception("Cargo no existe");
                }
                else
                {
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean borrarFuncionario(FUNCIONARIO funcionario)
        {
            try
            {
                if (Util.isObjetoNulo(funcionario))
                {
                    throw new Exception("Funcionario no existe");
                }
                else
                {
                    conexionDB.FUNCIONARIO.Remove(funcionario);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //ECU-024
        #region Prestación médica
        public Boolean nuevaPrestacionMedica(PRESTACION prestacion)
        {
            try
            {
                if (Util.isObjetoNulo(prestacion))
                {
                    throw new Exception("Prestación nula");
                }
                else if (prestacion.NOM_PRESTACION == null ||
                         prestacion.NOM_PRESTACION == String.Empty ||
                         prestacion.PRECIO_PRESTACION == null)
                {
                    throw new Exception("Nombre, código o precio vacío");
                }
                else if (prestacion.CODIGO_PRESTACION == null ||
                         prestacion.CODIGO_PRESTACION == string.Empty ||
                         Util.isObjetoNulo(prestacion.CODIGO_PRESTACION))
                {
                    throw new Exception("Código vacío");
                }
                else if (prestacion.ID_TIPO_PRESTACION == null)
                {
                    throw new Exception("Tipo de prestación vacío");
                }
                else if (prestacion.ID_ESPECIALIDAD == null)
                {
                    throw new Exception("Especialidad vacío");
                }
                else if (!Util.isObjetoNulo(buscarPrestacionMedica(prestacion.CODIGO_PRESTACION)))
                {
                    throw new Exception("Prestación ya ingresada");
                }
                else
                {
                    conexionDB.PRESTACION.Add(prestacion);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public PRESTACION buscarPrestacionMedica(string codigo)
        {
            try
            {
                if (Util.isObjetoNulo(codigo))
                {
                    throw new Exception("ID verificador nulo");
                }
                else
                {
                    PRESTACION prestacion = null;
                    prestacion = conexionDB.PRESTACION.Where(d => d.CODIGO_PRESTACION == codigo)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(prestacion))
                    {
                        throw new Exception("Personal no existe");
                    }
                    return prestacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean actualizarPrestacionesMedicas(PRESTACION prestacion)
        {
            try
            {
                if (Util.isObjetoNulo(prestacion))
                {
                    throw new Exception("Prestación nula");
                }
                else if (prestacion.NOM_PRESTACION == null ||
                         prestacion.NOM_PRESTACION == String.Empty ||
                         prestacion.PRECIO_PRESTACION == null)
                {
                    throw new Exception("Nombre, código o precio vacío");
                }
                else if (prestacion.CODIGO_PRESTACION == null || prestacion.CODIGO_PRESTACION == String.Empty)
                {
                    throw new Exception("Código vacío");
                }
                else if (prestacion.ID_TIPO_PRESTACION == null)
                {
                    throw new Exception("Tipo de prestación vacío");
                }
                else
                {
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean borrarPrestacionMedica(PRESTACION prestacion)
        {
            try
            {
                if (Util.isObjetoNulo(prestacion))
                {
                    throw new Exception("Prestacion no existe");
                }
                else
                {
                    conexionDB.PRESTACION.Remove(prestacion);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //ECU-025
        #region Paciente
        public Boolean nuevoPaciente(PACIENTE paciente)
        {
            try
            {
                if (Util.isObjetoNulo(paciente))
                {
                    throw new Exception("Paciente nulo");
                }
                else if (paciente.NOMBRES_PACIENTE == null ||
                         paciente.APELLIDOS_PACIENTE == null ||
                         paciente.NOMBRES_PACIENTE == String.Empty ||
                         paciente.APELLIDOS_PACIENTE == String.Empty)
                {
                    throw new Exception("Nombre o apellido vacío");
                }
                else if (paciente.EMAIL_PACIENTE == null || paciente.EMAIL_PACIENTE == String.Empty)
                {
                    throw new Exception("Email vacío");
                }
                else if (paciente.RUT == null || paciente.RUT == 0)
                {
                    throw new Exception("RUT vacío");
                }
                else if (!Util.isObjetoNulo(buscarPaciente(paciente.RUT, paciente.DIGITO_VERIFICADOR)))
                {
                    throw new Exception("Paciente ya ingresado");
                }
                else
                {
                    string titulo, cuerpo, pass, md5;
                    // Crear contraseña
                    pass = Util.generarPass();
                    md5 = Util.hashMD5(pass);
                    paciente.HASHED_PASS = md5;
                    // Enviar correo
                    titulo = "Bienvenido a Centro Médico Hipócrates";
                    cuerpo = "Estimado/a " + paciente.NOMBRES_PACIENTE + " " + paciente.APELLIDOS_PACIENTE + ",\n\n";
                    cuerpo += "Gracias por atenderse en Centro Médico Hipócrates\n";
                    cuerpo += "Su contraseña es " + pass;
                    Emailer.enviarCorreo(paciente.EMAIL_PACIENTE, titulo, cuerpo);

                    conexionDB.PACIENTE.Add(paciente);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public PACIENTE buscarPaciente(int rut, string dv)
        {
            try
            {
                if (Util.isObjetoNulo(rut) || Util.isObjetoNulo(dv))
                {
                    throw new Exception("RUT o dígito verificador nulo");
                }
                else
                {
                    PACIENTE paciente = null;
                    paciente = conexionDB.PACIENTE.Where(d => d.RUT == rut
                                                         && d.DIGITO_VERIFICADOR == dv)
                                                         .FirstOrDefault();
                    if (Util.isObjetoNulo(paciente))
                    {
                        throw new Exception("Paciente no existe");
                    }
                    return paciente;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean actualizarPaciente(PACIENTE paciente)
        {
            try
            {
                if (Util.isObjetoNulo(paciente))
                {
                    throw new Exception("Paciente nulo");
                }
                else if (paciente.NOMBRES_PACIENTE == null ||
                         paciente.APELLIDOS_PACIENTE == null ||
                         paciente.NOMBRES_PACIENTE == String.Empty ||
                         paciente.APELLIDOS_PACIENTE == String.Empty)
                {
                    throw new Exception("Nombre o apellido vacío");
                }
                else if (paciente.EMAIL_PACIENTE == null || paciente.EMAIL_PACIENTE == String.Empty)
                {
                    throw new Exception("Email vacío");
                }
                else if (paciente.RUT == null || paciente.RUT == 0)
                {
                    throw new Exception("RUT vacío");
                }
                else if (Util.isObjetoNulo(buscarPaciente(paciente.RUT, paciente.DIGITO_VERIFICADOR)))
                {
                    throw new Exception("Paciente no existe");
                }
                else
                {
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean borrarPaciente(PACIENTE paciente)
        {
            try
            {
                if (Util.isObjetoNulo(paciente))
                {
                    throw new Exception("Paciente no existe");
                }
                else
                {
                    conexionDB.PACIENTE.Remove(paciente);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //Devolucion
        #region Devolución
        public bool DevolucionPago(PAGO pago, string nombre_dev)
        {
            try
            {
                if (Util.isObjetoNulo(pago))
                {
                    throw new Exception("Pago nulo");
                }
                else if (pago.ID_PAGO == 0 || pago.ID_DEVOLUCION != null)
                {
                    throw new Exception("El pago es invalido o ya tiene una devolucion");
                }
                DEVOLUCION devo = new DEVOLUCION();
                devo.NOM_TIPO_DEV = nombre_dev;
                conexionDB.DEVOLUCION.Add(devo);
                conexionDB.SaveChangesAsync();
                pago = conexionDB.PAGO.Where(d => d.ID_PAGO == pago.ID_PAGO).FirstOrDefault();
                if (pago.ID_DEVOLUCION != null)
                {
                    //conexionDB.DEVOLUCION.Remove(devo);
                    //conexionDB.SaveChangesAsync();
                    return false;
                }
                pago.ID_DEVOLUCION = devo.ID_DEVOLUCION;
                conexionDB.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Horas disponibles
        private List<ATENCION_AGEN> buscarAtencionMedicoPorEstado(PERS_MEDICO medico, String nombreEstado)
        {
            List<ATENCION_AGEN> atencionList = medico.ATENCION_AGEN.ToList();
            List<ATENCION_AGEN> atencionesFiltradas = new List<ATENCION_AGEN>();
            foreach (ATENCION_AGEN atencion in atencionList)
            {
                ESTADO_ATEN estado = conexionDB.ESTADO_ATEN.Where(d => d.NOM_ESTADO_ATEN.ToUpper() == nombreEstado.ToUpper()).FirstOrDefault();
                if (atencion.ID_ESTADO_ATEN == estado.ID_ESTADO_ATEN)
                {
                    atencionesFiltradas.Add(atencion);
                }
            }
            return atencionesFiltradas;
        }

        private List<ATENCION_AGEN> filtrarAtencionPorDia(List<ATENCION_AGEN> atenciones, DateTime dia)
        {
            List<ATENCION_AGEN> atencionesFiltradas = new List<ATENCION_AGEN>();
            atencionesFiltradas = atenciones.Where(d => d.FECHOR.Value.Date == dia.Date).ToList();
            return atencionesFiltradas;
        }

        private List<BLOQUE> bloquesMedico(PERS_MEDICO medico, DateTime date)
        {
            List<BLOQUE> bloquesFiltrados = new List<BLOQUE>();
            List<HORARIO> horarios = medico.HORARIO.ToList();
            DIA_SEM dia = buscarDiaPorDate(date);
            foreach (HORARIO horario in horarios)
            {
                if (horario.BLOQUE.ID_DIA_SEM == dia.ID_DIA)
                {
                    bloquesFiltrados.Add(horario.BLOQUE);
                }
            }
            return bloquesFiltrados;
        }

        private List<BLOQUE> removerBloquesAgendados(List<BLOQUE> bloques, List<ATENCION_AGEN> atenciones)
        {
            List<BLOQUE> result = new List<BLOQUE>();
            foreach (BLOQUE bloque in bloques)
            {
                if (isBloqueInAtenciones(bloque, atenciones))
                {

                }
                else
                {
                    result.Add(bloque);
                }
            }
            return result;
        }

        public DIA_SEM buscarDiaPorDate(DateTime date)
        {
            int numDia = (int)date.DayOfWeek;
            String nomDiaBuscar = "";
            switch (numDia)
            {
                case 0:
                    nomDiaBuscar = "Domingo";
                    break;
                case 1:
                    nomDiaBuscar = "Lunes";
                    break;
                case 2:
                    nomDiaBuscar = "Martes";
                    break;
                case 3:
                    nomDiaBuscar = "Miercoles";
                    break;
                case 4:
                    nomDiaBuscar = "Jueves";
                    break;
                case 5:
                    nomDiaBuscar = "Viernes";
                    break;
                case 6:
                    nomDiaBuscar = "Sabado";
                    break;
                default:
                    throw new Exception("Dia invalido");
            }
            DIA_SEM dia = conexionDB.DIA_SEM.Where(d => d.NOMBRE_DIA == nomDiaBuscar).FirstOrDefault();
            if (Util.isObjetoNulo(dia))
            {
                throw new Exception("No hay dia con nombre " + nomDiaBuscar);
            }
            return dia;
        }

        private bool isBloqueInAtenciones(BLOQUE bloque, List<ATENCION_AGEN> atenciones)
        {
            bool result = false;
            if (atenciones.Where(d => d.BLOQUE.ID_BLOQUE == bloque.ID_BLOQUE).Count() > 0)
                result = true;
            return result;
        }

        public HorasDisponibles horasDisponiblesMedico(PERS_MEDICO medico, DateTime dia)
        {
            //Obtener todas las atenciones Vigentes del médico
            List<ATENCION_AGEN> atencionesVigentes = buscarAtencionMedicoPorEstado(medico, "Vigente");
            //Obtener todas las atenciones para el día
            List<ATENCION_AGEN> atencionesFiltradasPorDia = filtrarAtencionPorDia(atencionesVigentes, dia);
            //Obtener los bloques del medico para el día solicitado
            List<BLOQUE> bloquesDia = bloquesMedico(medico, dia);
            //Remover bloques que tengan una atencion agendada
            List<BLOQUE> bloquesLibres = removerBloquesAgendados(bloquesDia, atencionesFiltradasPorDia);
            //convertir bloque a hora disponible
            HorasDisponibles horas = new HorasDisponibles(dia, bloquesLibres);
            return horas;
        }
        #endregion

        #region Devolver listas
        public List<ESPECIALIDAD> listaEspecialidad()
        {
            return (conexionDB.ESPECIALIDAD.ToList());
        }

        public List<PERSONAL> listaPersonales(int idEspecialidad)
        {
            List<PERS_MEDICO> personalesMedicos = conexionDB.PERS_MEDICO.Where(d => d.ESPECIALIDAD.ID_ESPECIALIDAD == idEspecialidad).ToList();
            List<PERSONAL> personales = new List<PERSONAL>();
            foreach (PERS_MEDICO p in personalesMedicos)
            {
                personales.Add(p.PERSONAL);
            }
            return (personales);
        }

        public List<PRESTACION> listaPrestaciones(int idEspecialidad)
        {
            List<PRESTACION> prestaciones = conexionDB.PRESTACION.Where(d => d.ESPECIALIDAD.ID_ESPECIALIDAD == idEspecialidad).ToList();
            return (prestaciones);
        }
        #endregion

        //Tipo Ficha
        #region TipoFicha
        public List<TIPO_FICHA> ObtenerTiposFicha()
        {
            List<TIPO_FICHA> tipos = conexionDB.TIPO_FICHA.ToList();
            return tipos;
        }
        #endregion

        //LogPAgo
        #region LogPago
        public List<LOGPAGOHONORARIO> ObtenerLogPagoHonorario()
        {
            List<LOGPAGOHONORARIO> logs = conexionDB.LOGPAGOHONORARIO.ToList();
            return logs;
        }
        #endregion

        //Banco
        #region Banco
        public List<BANCO> ObtenerBancos()
        {
            List<BANCO> bancos = conexionDB.BANCO.ToList();
            return bancos;
        }
        #endregion

        //TipoCuentaBancaria
        #region TipoCuentaBancaria
        public List<TIPO_C_BANCARIA> ObtenerTiposCuentaBancaria()
        {
            List<TIPO_C_BANCARIA> tipcuentab = conexionDB.TIPO_C_BANCARIA.ToList();
            return tipcuentab;
        }
        #endregion

        //CuentaBancaria
        #region CuentaBancaria
        public bool crearCuentaBancaria(CUEN_BANCARIA cuenta)
        {
            try
            {
                if (Util.isObjetoNulo(cuenta))
                {
                    throw new Exception("Paciente nulo");
                }
                else if (cuenta.ID_BANCO == null ||
                         cuenta.ID_PERS_MEDICO == null ||
                         cuenta.ID_TIPO_C_BANCARIA == null)
                {
                    throw new Exception("Cuenta bancaria invalida");
                }
                else
                {
                    conexionDB.CUEN_BANCARIA.Add(cuenta);
                    conexionDB.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool actualizarCuentaBancaria(CUEN_BANCARIA cuenta, int personalID)
        {
            try
            {
                if (Util.isObjetoNulo(cuenta))
                {
                    throw new Exception("Paciente nulo");
                }
                else if (cuenta.ID_BANCO == null ||
                         cuenta.ID_TIPO_C_BANCARIA == null)
                {
                    throw new Exception("Cuenta bancaria invalida");
                }
                else
                {
                    using (var con = new CMHEntities())
                    {
                        CUEN_BANCARIA x = new CUEN_BANCARIA();
                        x = con.CUEN_BANCARIA.Where(d => d.PERS_MEDICO.ID_PERSONAL == personalID).FirstOrDefault();
                        x.ID_BANCO = cuenta.ID_BANCO;
                        x.ID_TIPO_C_BANCARIA = cuenta.ID_TIPO_C_BANCARIA;
                        x.NUM_C_BANCARIA = cuenta.NUM_C_BANCARIA;
                        con.SaveChangesAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        //Obtener atenciones aptas para orden de analisis
        #region AtencionesAptasAnalisis
        public List<RES_ATENCION> ResAtencionesAptasParaAnalisis()
        {
            List<RES_ATENCION> resultados = conexionDB.RES_ATENCION.Where(d => d.ATENCION_ABIERTA == true && d.ID_ORDEN_ANALISIS == null).ToList();
            if (resultados != null)
                return resultados;
            else return new List<RES_ATENCION>();
        }
        public List<RES_ATENCION> ResAtencionesAptasParaCerrarAnalisis()
        {
            List<RES_ATENCION> resultados = conexionDB.RES_ATENCION.Where(d => d.ATENCION_ABIERTA == true && d.ID_ORDEN_ANALISIS != null).ToList();
            if (resultados != null)
                return resultados;
            else return new List<RES_ATENCION>();
        }
        #endregion
    }
}
