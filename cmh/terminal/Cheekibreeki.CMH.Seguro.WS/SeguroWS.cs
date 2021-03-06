﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cheekibreeki.CMH.Seguro.BL;
namespace Cheekibreeki.CMH.Seguro.WS
{
    public class SeguroWS : ISeguroWS
    {
        public SeguroResponse obtenerDescuento(SeguroRequest seguroRequest)
        {

            AccionesSeguro accionesSeguro = new AccionesSeguro();
            int descuento = accionesSeguro.obtenerDescuentoPrestacion(seguroRequest.AfiliadoRut, seguroRequest.CodigoPrestacion, seguroRequest.PrecioPrestacion);
            SeguroResponse response = new SeguroResponse();
            response.DescuentoPesos = descuento;
            response.NombreAseguradora = accionesSeguro.obtenerNombreEmpresa(seguroRequest.AfiliadoRut);
            if (descuento > 0)
            {
                response.AfiliadoTieneSeguro = true;
            }
            return response;
        }
    }
}
