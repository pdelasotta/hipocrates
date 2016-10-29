package cl.cheekibreeki.cmh.webapp.bl;

import cl.cheekibreeki.cmh.lib.dal.dbcontrol.Controller;
import cl.cheekibreeki.cmh.lib.dal.entities.*;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author palan
 */
public class AccionesPacienteTest {
    
     Paciente paciente = null;
    
    public AccionesPacienteTest() {
        paciente = new Paciente();
        paciente.setNombresPaciente("Tomas");
        paciente.setApellidosPaciente("Muniz Quiroz");
        paciente.setRut(18766322);
        paciente.setDigitoVerificador('1');
        paciente.setHashedPass("holamundo");
        paciente.setEmailPaciente("tomasmuniz@peladordenaranjas.com");
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }

    /**
     * Test of registrarPaciente method, of class AccionesPaciente.
     */
    @Test
    public void testRegistrarPaciente() {
        System.out.println("registrarPaciente");
        AccionesPaciente instance = new AccionesPaciente();
        
        //remueve los pacientes extras de test
        Controller ctr = new Controller();
        Map<String, Object> params = new HashMap<>();
        params.put("rut", this.paciente.getRut());
        List<? extends Object>  pacientes = ctr.findByQuery("Paciente.findByRut", params);
        if(!pacientes.isEmpty()){
            Paciente  pacienteAux = (Paciente)pacientes.get(0);
            Controller.remove(Paciente.class, pacienteAux.getIdPaciente());
        }
        
        //Prueba 1: Ingresa paciente
        boolean expResult = true;
        boolean result = instance.registrarPaciente(this.paciente);
        assertEquals(expResult, result);
        
        //Prueba 2: Ingresa paciente vacio
        Paciente paciente2 = new Paciente();
        expResult = false;
        result = instance.registrarPaciente(paciente2);
        assertEquals(expResult, result);
        
        //Prueba 3: ingresa el mismo paciente que en el test 1 pero no se puede inresar un paciente con el mismo rut
        expResult = false;
        result = instance.registrarPaciente(this.paciente);
        assertEquals(expResult, result);
        
        // FIN
         System.out.println("registro exitoso");
    }

    /**
     * Test of obtenerExamenes method, of class AccionesPaciente.
     */
    @Test
    public void testObtenerExamenes() {
         System.out.println("obtenerExamenes");
         AccionesPaciente instance = new AccionesPaciente();
         Controller ctr = new Controller();
         Map<String, Object> params1 = new HashMap<>();
         params1.put("rut", this.paciente.getRut());
         Paciente pacienteAux = (Paciente)ctr.findByQuery("Paciente.findByRut", params1).get(0);
 
         AtencionAgen atencion = new AtencionAgen();
         ResAtencion resAtencion = new ResAtencion();
         
         //Prueba 1: Si no hay examenes asociados
         InsertAtencionesYExamenes(atencion, resAtencion, true, ctr);
         ArrayList<ResAtencion> result = instance.obtenerExamenes(pacienteAux);
         if(result.isEmpty()){
             System.out.println("El paciente no tiene examenes asociados");
         }else{
             fail("Examenes obtenidos");
         }
         
         //Prueba 2: Si hay examenes asociados
         InsertAtencionesYExamenes(atencion, resAtencion, false, ctr);
         result = instance.obtenerExamenes(pacienteAux);
         if(result.isEmpty()){
             fail("El paciente no tiene examenes asociados");
         }else{
             System.out.println("Examenes obtenidos");
         }
    }
    
    public void InsertAtencionesYExamenes(AtencionAgen atencion,ResAtencion resAtencion, boolean delete, Controller ctr){
        if(!delete){
            Map<String, Object> params1 = new HashMap<>();
            params1.put("rut", this.paciente.getRut());
            Paciente pacienteAux = (Paciente)ctr.findByQuery("Paciente.findByRut", params1).get(0);
            atencion.setFechor(Date.from(Instant.now()));
            atencion.setObservaciones("Test");
            //Crear Bloque
            Bloque bloque = new Bloque();
            //Se crea un dia si no existe
            DiaSem dia = new DiaSem();
            Map<String, Object> para = new HashMap<>();
            List<? extends Object> dias = ctr.findByQuery("DiaSem.findAll", para);
            if(!dias.isEmpty()){
                dia = (DiaSem)dias.get(dias.size()-1);
            }else{
                dia.setNombreIda("DiaTest");
                Object objDia = dia;
                Controller.upsert(objDia);
                List<? extends Object> diasAux = ctr.findByQuery("DiaSem.findAll", para);
                dia = (DiaSem)diasAux.get(diasAux.size()-1);
            }
            //Fin dia
            para = new HashMap<>();
            List<? extends Object> bloques = ctr.findByQuery("Bloque.findAll", para);
            if(!bloques.isEmpty()){
                bloque = (Bloque)bloques.get(bloques.size()-1);
            }else{
                bloque.setIdDiaSem(dia);
                bloque.setNumBloque(0);
                bloque.setNumHoraFin(Short.parseShort("0"));
                bloque.setNumHoraIni(Short.parseShort("0"));
                bloque.setNumMinuFin(Short.parseShort("0"));
                bloque.setNumMinuIni(Short.parseShort("0"));
                Object objBloq = bloque;
                Controller.upsert(objBloq);
                List<? extends Object> bloquesAux = ctr.findByQuery("Bloque.findAll", para);
                bloque = (Bloque)bloquesAux.get(bloquesAux.size()-1);
            }
            //Fin bloque
            atencion.setIdBloque(bloque);
            atencion.setIdEstadoAten(new EstadoAten());
            atencion.setIdPaciente(pacienteAux);
            //Se crea PersMedico
            PersMedico pers = new PersMedico();
            para = new HashMap<>();
            List<? extends Object> persMedicos = ctr.findByQuery("PersMedico.findAll", para);
            if(!persMedicos.isEmpty()){
                pers = (PersMedico)persMedicos.get(persMedicos.size()-1);
            }else{
                //pers.setIdEspecialidad(idEspecialidad);
                Object objPers = pers;
                Controller.upsert(objPers);
                List<? extends Object> diasAux = ctr.findByQuery("PersMedico.findAll", para);
                pers = (PersMedico)persMedicos.get(persMedicos.size()-1);
            }
            //Fin PersMedico
            atencion.setIdPersAtiende(pers);
            atencion.setIdPrestacion(new Prestacion());
            atencion.setObservaciones("test");
            Object obj1 = atencion;
            boolean x = Controller.upsert(obj1);
            assertEquals(true, x);//error
            
            Map<String, Object> params2 = new HashMap<>();
            params2.put("idPaciente", pacienteAux);
            List<? extends Object> atenciones = ctr.findByQuery("AtencionAgen.findByIdPaciente", params2);
            AtencionAgen atencionAux = (AtencionAgen)atenciones.get(atenciones.size()-1);
            resAtencion.setAtencionAbierta(Short.parseShort("0"));
            resAtencion.setComentario("test");
            resAtencion.setIdAtencionAgen(atencionAux);
            resAtencion.setIdOrdenAnalisis(new OrdenAnalisis());
           // resAtencion.setIdPersonalMedico(new PersMedico());
            resAtencion.setIdResultadoAtencion(0);
            Object obj2 = resAtencion;
            boolean y = Controller.upsert(obj2);   
            assertEquals(true, y);
            
        }else{
            Map<String, Object> params = new HashMap<>();
            params.put("rut", paciente.getRut());
            Paciente pacienteAux = (Paciente)ctr.findByQuery("Paciente.findByRut", params).get(0);
            if(pacienteAux == null)
                return;
            
             params = new HashMap<>();
            params.put("idPaciente", pacienteAux);
            try{
            AtencionAgen atencionAux = (AtencionAgen)ctr.findByQuery("AtencionAgen.findByIdPaciente", params).get(0);
            if(atencionAux == null)
                return;
            else
                Controller.remove(AtencionAgen.class, atencionAux.getIdAtencionAgen());
            
            params = new HashMap<>();
            params.put("idAtencionAgen", atencionAux.getIdAtencionAgen());
            ResAtencion resAtencionAux = (ResAtencion)ctr.findByQuery("ResAtencion.findByIdAtencionAgen", params).get(0);
            if(resAtencionAux != null)
                Controller.remove(ResAtencion.class, resAtencionAux.getIdResultadoAtencion());   
            }catch(Exception e){
                System.out.println(e.getMessage());
            }
        }  
    }

    /**
     * Test of obtenerAtenciones method, of class AccionesPaciente.
     */
    @Test
    public void testHorasDisponibles() {
        System.out.println("testHorasDisponibles");
        AccionesPaciente accionesPaciente = new AccionesPaciente();
        //Preparación
        //Crear dias
        //Crear bloques
        //Crear personal
        //Crear especialidad
        //Crear personal médico
        //Crear horario
        //Caso 1: obtener 3 horas disponibles
        //Caso 2: obtener 0 horas disponibles
        
    }

    /**
     * Test of agendarAtencion method, of class AccionesPaciente.
     */
    @Test
    public void testAgendarAtencion() {
    }
    
      /**
     * Test of obtenerAtenciones method, of class AccionesPaciente.
     */
    @Test
    public void testObtenerAtencionesPendientes() {
    }

    /**
     * Test of obtenerMedicosPorPrestacion method, of class AccionesPaciente.
     */
    @Test
    public void testObtenerMedicosPorPrestacion() {
        System.out.println("obtenerMedicosPorPrestacion");
        AccionesPaciente instance = new AccionesPaciente();
        Controller ctr = new Controller();
        
        //Prueba 1: Se encuentran medicos por prestacion
        Prestacion prestacionAux = InitPrestacion(ctr);
        ArrayList<PersMedico> persMedicos = instance.obtenerMedicosPorPrestacion(prestacionAux);
        if(!persMedicos.isEmpty()){
            System.out.println("Se encontraror medicos por prestacion");
        }else{
             fail("No se encontraron medicos por prestacion");
        }
        
        //Prueba 2: No se encuentran medicos por prestacion
        Prestacion prestacionVacia = new Prestacion();
        persMedicos = instance.obtenerMedicosPorPrestacion(prestacionVacia);
        if(!persMedicos.isEmpty()){
             fail("Se encontraror medicos por prestacion");
        }else{
             System.out.println("No se encontraron medicos por prestacion");
        }
        
        Map<String, Object> params1 = new HashMap<>();
        params1.put("rut", this.paciente.getRut());
        Paciente pacienteAux = (Paciente)ctr.findByQuery("Paciente.findByRut", params1).get(0);
    }
    
    private Prestacion InitPrestacion(Controller ctr){
        //Eliminar ultimo PersMedico
        Map<String, Object> params = new HashMap<>();
        List<? extends Object> persMedicos = ctr.findByQuery("PersMedico.findAll", params);
        if(!persMedicos.isEmpty()){
            PersMedico persMedicoAux = (PersMedico)persMedicos.get(persMedicos.size()-1);
            Controller.remove(PersMedico.class, persMedicoAux.getIdPersonalMedico());
        }
        //Eliminar ultima Prestacion
        params = new HashMap<>();
        List<? extends Object> prestaciones = ctr.findByQuery("Prestacion.findAll", params);
        if(!prestaciones.isEmpty()){
            Prestacion prestacionAux = (Prestacion)prestaciones.get(prestaciones.size()-1);
            Controller.remove(Prestacion.class, prestacionAux.getIdPrestacion());
        }
        //Eliminar ultima Especialidad
        params = new HashMap<>();
        List<? extends Object> especialidades = ctr.findByQuery("Especialidad.findAll", params);
        if(!especialidades.isEmpty()){
            Especialidad especialidadAux = (Especialidad)especialidades.get(especialidades.size()-1);
            Controller.remove(Especialidad.class, especialidadAux.getIdEspecialidad());
        }
        //Crear Especialidad
        Especialidad esp = new Especialidad();
        esp.setNomEspecialidad("Test");
        Object obj = esp;
        Controller.upsert(obj);
        params = new HashMap<>();
        especialidades = ctr.findByQuery("Especialidad.findAll", params);
        if(!especialidades.isEmpty()){
            esp = (Especialidad)especialidades.get(especialidades.size()-1);
        }
        //Crear PersMedico
        PersMedico perMe = new PersMedico();
        perMe.setIdEspecialidad(esp);
        //perMe.setIdPersonal(new Personal());
        //perMe.setIdTurno(new Turno());
        obj = perMe;
        Controller.upsert(obj);
        //Crear Prestacion
        Prestacion pre = new Prestacion();
        pre.setCodigoPrestacion("Test");
        pre.setIdEspecialidad(esp);
        //pre.setIdTipoPrestacion(new TipoPres());
        pre.setNomPrestacion("PrestacionTest");
        pre.setPrecioPrestacion(1000);
        obj = pre;
        Controller.upsert(obj);
        params = new HashMap<>();
        prestaciones = ctr.findByQuery("Prestacion.findAll", params);
        if(!prestaciones.isEmpty()){
            pre = (Prestacion)prestaciones.get(prestaciones.size()-1);
        }
        return pre;
    }
    
    /**
     * Test of anularAtencion method, of class AccionesPaciente.
     */
    @Test
    public void testAnularAtencion() throws Exception {
        //Preparación
        //Crear estado atención Vigente y Anulado
        EstadoAten estadoVigente = crearEstadoAten("Vigente");
        EstadoAten estadoAnulada = crearEstadoAten("Anulada");
        //Crear personal
        Personal personal = new Personal();
        personal.setActivo((short)1);
        personal.setNombres("El hombre test");
        personal.setApellidos("El hombre test");
        Controller.upsert(personal);
        //Crear especialidad
        Especialidad especialidad = new Especialidad();
        especialidad.setNomEspecialidad("Especialidad test");
        Controller.upsert(especialidad);
        //Crear personal médico y asignar personal y especialidad
        PersMedico personalMedico = new PersMedico();
        personalMedico.setIdPersonal(personal);
        personalMedico.setIdEspecialidad(especialidad);
        Controller.upsert(personalMedico);
        //Crear dia
        DiaSem dia = new DiaSem();
        dia.setNombreIda("Lunes");
        Controller.upsert(dia);
        //crear bloque
        Bloque bloque = new Bloque();
        bloque.setNumBloque(1);
        bloque.setIdDiaSem(dia);
        bloque.setNumHoraIni((short)8);
        bloque.setNumMinuIni((short)0);
        bloque.setNumHoraFin((short)8);
        bloque.setNumMinuFin((short)15);
        Controller.upsert(bloque);
        //crear horario
        Horario horario = new Horario();
        horario.setIdBloque(bloque);
        horario.setIdPersMedico(personalMedico);
        Controller.upsert(horario);
        //Crear Atención agendada
        AtencionAgen atencion = new AtencionAgen();
        atencion.setFechor(new Date());
        atencion.setObservaciones("TestAnular");
        atencion.setIdEstadoAten(estadoVigente);
        atencion.setIdPersAtiende(personalMedico);
        atencion.setIdBloque(bloque);
        Controller.upsert(atencion);
        //Caso 1: anular una atención pendiente correctamente
        AccionesPaciente accionesPaciente = new AccionesPaciente();
        EstadoAten result = accionesPaciente.anularAtencion(atencion).getIdEstadoAten();
        EstadoAten expectedResult = new EstadoAten();
        expectedResult.setNomEstadoAten("Anulada");
        try {
            if(result.getNomEstadoAten().equals(expectedResult.getNomEstadoAten())){
            
        }else{
            fail();
        }
        } catch (Exception e) {
            fail();
        }
        
        //Caso 2: fallar al intentar anular una atención no pendiente
        try{
            accionesPaciente.anularAtencion(atencion); //si esto no levanta una excepción, falla la prueba
            fail();
        }catch(Exception ex){
            System.out.println("Exito test anular atencion caso 2");
        }
    }
    
    private EstadoAten crearEstadoAten(String nomEstado){
        EstadoAten estadoVigente = new EstadoAten();
        estadoVigente.setNomEstadoAten(nomEstado);
        Controller.upsert(estadoVigente);
        Map<String, Object> params = new HashMap<>();
        params.put("nomEstadoAten", nomEstado);
        List<? extends Object>  estadoAtenList = Controller.findByQuery("EstadoAten.findByNomEstadoAten", params);
        return (EstadoAten)estadoAtenList.get(0);
    }
    
//    private Personal crearPersonal(short isActivo, String nombres, String apellidos){
//        Personal personal = new Personal();
//        personal.setActivo(isActivo);
//        personal.setNombres(nombres);
//        personal.setApellidos(apellidos);
//        Controller.upsert(personal);
//        Map<String, Object> params = new HashMap<>();
//        params.put("nomEstadoAten", nomEstado);
//        List<? extends Object>  estadoAtenList = Controller.findByQuery("EstadoAten.findByNomEstadoAten", params);
//    }
//    
//    private Especialidad crearEspecialidad(){
//        
//    }
//    
//    private PersMedico crearPersonalMedico(Personal personal, Especialidad especialidad){
//        
//    }
//    
//    private Dia crearDia(){
//        
//    }
//    
//    private Bloque crearBloque(){
//        
//    }
//    
//    private Horario crearHorario(){
//        
//    }
//    
//    private AtencionAgen crearAtencionAgendada(){
//        
//    }
}
