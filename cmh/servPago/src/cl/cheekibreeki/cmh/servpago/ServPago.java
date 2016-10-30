/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.cheekibreeki.cmh.servpago;

import cl.cheekibreeki.cmh.lib.dal.dbcontrol.Controller;
import cl.cheekibreeki.cmh.lib.dal.entities.AtencionAgen;
import cl.cheekibreeki.cmh.lib.dal.entities.CuenBancaria;
import cl.cheekibreeki.cmh.lib.dal.entities.Especialidad;
import cl.cheekibreeki.cmh.lib.dal.entities.Pago;
import cl.cheekibreeki.cmh.lib.dal.entities.PersMedico;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;


/**
 *
 * @author pdelasotta
 */
public class ServPago {
    
    private static final float descuento = 0.4f;
    
    //Tiene que sacar todas las atenciones realizadas con sus respectivos pagos de el mes (Solo de los medicos) sumarlo y el total restarle un 40%
    public boolean pagarMedicos(){
        Controller ctr = new Controller();
        boolean result = false;
        //Se obtiene una lista con la especialidad "Medico" de la cual solo se saca el primer registro [0]
        Map<String, Object> params = new HashMap<>();
        params.put("nomEspecialidad", "Medico");
        List<? extends Object> especialidadList = ctr.findByQuery("Especialidad.findByNomEspecialidad", params);
        if(!especialidadList.isEmpty()){
            Especialidad especialidad = (Especialidad)especialidadList.get(0);
            //Se obtiene una lista con el personal medico con la especialidad "Medico"
            params = new HashMap<>();
            params.put("idEspecialidad", especialidad);
            List<? extends Object> personalMedicos = ctr.findByQuery("PersMedico.findByIdEspecialidad", params);
            ArrayList<PersMedico> medicos = new ArrayList<>();
            for (Object x : medicos) {
                medicos.add((PersMedico)x);
            }
            //Se filtra uno por uno el personal medico para sacar y almacenar sus datos;
            ArrayList<PagoHonorario> pagosHonorario = new ArrayList<>();
            for (PersMedico x : medicos) {
                PagoHonorario ph = new PagoHonorario();
                ph.setNombre(x.getIdPersonal().getNombres()+" "+x.getIdPersonal().getApellidos());
                ArrayList<CuenBancaria> cuentaBancaria = new ArrayList<>(x.getCuenBancariaCollection());
                //Saca la primera cuenta bancaria
                ph.setBanco(cuentaBancaria.get(0).getIdBanco().getNombre());
                ph.setTipoCuenta(cuentaBancaria.get(0).getIdTipoCBancaria().getNomCBancaria());
                ph.setCuenta(cuentaBancaria.get(0).getIdCuenBancaria());
                //Calcular todos los pagos del mes
                int subtotal = 0;
                ArrayList<AtencionAgen> atenciones = new ArrayList<>(x.getAtencionAgenCollection());
                for (AtencionAgen at : atenciones) {
                    if(at.getIdEstadoAten().getNomEstadoAten().equals("Terminado") && AtencionDeEsteMes(at.getFechor())){
                        ArrayList<Pago> pagos = new ArrayList<>(at.getPagoCollection());
                        for (Pago pago : pagos) {
                            subtotal += pago.getMontoPago();
                        }
                    }
                }
                ph.setSubTotal(subtotal);
                ph.setTotal(Math.round(subtotal+subtotal*descuento));
                //Se guarda el pago de honorario en el ArrayList
                pagosHonorario.add(ph);
            }
            //Se tiene que guardar el resgitro en la base de datos
            
        }
        return result;
    }
    
    private boolean AtencionDeEsteMes(Date dat){
        Date now = Date.from(Instant.now());
        if(now.getMonth()-1 <=0){
            now.setMonth(12);
            now.setYear(now.getYear()-1);
        }
        else 
            now.setMonth(now.getMonth()-1 );
        return (dat.getMonth()==(now.getMonth()) && dat.getYear()==now.getYear());
    }
    
    public boolean pagarMedicos(String esp){
        Controller ctr = new Controller();
        boolean result = false;
        //Se obtiene una lista con la especialidad "Medico" de la cual solo se saca el primer registro [0]
        Map<String, Object> params = new HashMap<>();
        params.put("nomEspecialidad", esp);
        List<? extends Object> especialidadList = ctr.findByQuery("Especialidadd.findByNomEspecialidad", params);
        if(!especialidadList.isEmpty()){
            Especialidad especialidad = (Especialidad)especialidadList.get(0);
            //Se obtiene una lista con el personal medico con la especialidad "Medico"
            params = new HashMap<>();
            params.put("idEspecialidad", especialidad);
            List<? extends Object> personalMedicos = ctr.findByQuery("PersMedico.findByIdEspecialidad", params);
            ArrayList<PersMedico> medicos = new ArrayList<>();
            for (Object x : medicos) {
                medicos.add((PersMedico)x);
            }
            //Se filtra uno por uno el personal medico para sacar y almacenar sus datos;
            ArrayList<PagoHonorario> pagosHonorario = new ArrayList<>();
            for (PersMedico x : medicos) {
                PagoHonorario ph = new PagoHonorario();
                ph.setNombre(x.getIdPersonal().getNombres()+" "+x.getIdPersonal().getApellidos());
                ArrayList<CuenBancaria> cuentaBancaria = new ArrayList<>(x.getCuenBancariaCollection());
                //Saca la primera cuenta bancaria
                ph.setBanco(cuentaBancaria.get(0).getIdBanco().getNombre());
                ph.setTipoCuenta(cuentaBancaria.get(0).getIdTipoCBancaria().getNomCBancaria());
                ph.setCuenta(cuentaBancaria.get(0).getIdCuenBancaria());
                //Calcular todos los pagos del mes
                int subtotal = 0;
                ArrayList<AtencionAgen> atenciones = new ArrayList<>(x.getAtencionAgenCollection());
                for (AtencionAgen at : atenciones) {
                    if(at.getIdEstadoAten().getNomEstadoAten().equals("Terminado") && AtencionDeEsteMes(at.getFechor())){
                        ArrayList<Pago> pagos = new ArrayList<>(at.getPagoCollection());
                        for (Pago pago : pagos) {
                            subtotal += pago.getMontoPago();
                        }
                    }
                }
                ph.setSubTotal(subtotal);
                ph.setTotal(Math.round(subtotal+subtotal*descuento));
                //Se guarda el pago de honorario en el ArrayList
                pagosHonorario.add(ph);
            }
            //Se tiene que guardar el resgitro en la base de datos
            
        }
        return result;
    }
}
