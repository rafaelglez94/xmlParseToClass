using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LectorCfdi
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlName = "XMLFile1.xml";
            XNamespace ns = "http://www.sat.gob.mx/cfd/3";
            XDocument xml = XDocument.Load(xmlName);
            var comprobante = ObtenerComprobante(xml);
            Console.WriteLine(comprobante.ToString());
        }

        public static List<Concepto> ObtenerConceptos(XDocument _Cfdi){
            XNamespace ns = "http://www.sat.gob.mx/cfd/3";
            return (from concepto in _Cfdi.Descendants(ns + "Conceptos").FirstOrDefault().Descendants(ns + "Concepto")
                select new Concepto {
                    ClaveProdServ = concepto.Attribute("ClaveProdServ") != null ? concepto.Attribute("ClaveProdServ").Value : null,
                    NoIdentificacion = concepto.Attribute("NoIdentificacion") != null ? concepto.Attribute("NoIdentificacion").Value : null,
                    Cantidad = concepto.Attribute("Cantidad") != null ? concepto.Attribute("Cantidad").Value : null,
                    ClaveUnidad = concepto.Attribute("ClaveUnidad") != null ? concepto.Attribute("ClaveUnidad").Value : null,
                    Unidad = concepto.Attribute("Unidad") != null ? concepto.Attribute("Unidad").Value : null,
                    Descripcion = concepto.Attribute("Descripcion") != null ? concepto.Attribute("Descripcion").Value : null,
                    ValorUnitario = concepto.Attribute("ValorUnitario") != null ? concepto.Attribute("ValorUnitario").Value : null,
                    Descuento = concepto.Attribute("Descuento") != null ? concepto.Attribute("Descuento").Value : null,
                    Importe = concepto.Attribute("Importe") != null ? concepto.Attribute("Importe").Value : null
                }
            ).ToList();
        }

        public static Emisor ObtenerEmisor(XDocument _Cfdi)
        {
            XNamespace ns = "http://www.sat.gob.mx/cfd/3";
            return (from emisor in _Cfdi.Descendants(ns + "Emisor")
                    select new Emisor {
                        Rfc = emisor.Attribute("Rfc") != null ? emisor.Attribute("Rfc").Value : null,
                        Nombre = emisor.Attribute("Nombre") != null ? emisor.Attribute("Nombre").Value : null,
                        RegimenFiscal = emisor.Attribute("RegimenFiscal") != null ? emisor.Attribute("RegimenFiscal").Value : null,
                    }).FirstOrDefault();
        }
        public static Receptor ObtenerReceptor(XDocument _Cfdi)
        {
            XNamespace ns = "http://www.sat.gob.mx/cfd/3";
            return (from receptor in _Cfdi.Descendants(ns + "Receptor")
                    select new Receptor
                    {
                        Rfc = receptor.Attribute("Rfc") != null ? receptor.Attribute("Rfc").Value : null,
                        Nombre = receptor.Attribute("Nombre") != null ? receptor.Attribute("Nombre").Value : null,
                        ResidenciaFiscal = receptor.Attribute("ResidenciaFiscal") != null ? receptor.Attribute("ResidenciaFiscal").Value : null,
                        NumRegIdTrib = receptor.Attribute("NumRegIdTrib") != null ? receptor.Attribute("NumRegIdTrib").Value : null,
                        UsoCFDI = receptor.Attribute("UsoCFDI") != null ? receptor.Attribute("UsoCFDI").Value : null,
                    }).FirstOrDefault();
        }

        public static Comprobante ObtenerComprobante(XDocument _Cfdi)
        {
            XNamespace ns = "http://www.sat.gob.mx/cfd/3";
            var comprobante = ( from receptor in _Cfdi.Descendants(ns + "Comprobante")
                    select new Comprobante {
                        Serie = receptor.Attribute("Serie") != null ? receptor.Attribute("Serie").Value : null,
                        Folio = receptor.Attribute("Folio") != null ? receptor.Attribute("Folio").Value : null,
                        Fecha = receptor.Attribute("Fecha") != null ? receptor.Attribute("Fecha").Value : null,
                        Sello = receptor.Attribute("Sello") != null ? receptor.Attribute("Sello").Value : null,
                        FormaPago = receptor.Attribute("FormaPago") != null ? receptor.Attribute("FormaPago").Value : null,
                        NoCertificado = receptor.Attribute("NoCertificado") != null ? receptor.Attribute("NoCertificado").Value : null,
                        Certificado = receptor.Attribute("Certificado") != null ? receptor.Attribute("Certificado").Value : null,
                        CondicionesDePago = receptor.Attribute("CondicionesDePago") != null ? receptor.Attribute("CondicionesDePago").Value : null,
                        SubTotal = receptor.Attribute("SubTotal") != null ? receptor.Attribute("SubTotal").Value : null,
                        Descuento = receptor.Attribute("Descuento") != null ? receptor.Attribute("Descuento").Value : null,
                        Moneda = receptor.Attribute("Moneda") != null ? receptor.Attribute("Moneda").Value : null,
                        Total = receptor.Attribute("Total") != null ? receptor.Attribute("Total").Value : null,
                        TipoDeComprobante = receptor.Attribute("TipoDeComprobante") != null ? receptor.Attribute("TipoDeComprobante").Value : null,
                        MetodoPago = receptor.Attribute("MetodoPago") != null ? receptor.Attribute("MetodoPago").Value : null,
                        LugarExpedicion = receptor.Attribute("LugarExpedicion") != null ? receptor.Attribute("LugarExpedicion").Value : null
                    }).FirstOrDefault();
            comprobante.Emisor = ObtenerEmisor(_Cfdi);
            comprobante.Receptor = ObtenerReceptor(_Cfdi);
            comprobante.Conceptos = ObtenerConceptos(_Cfdi);
            return comprobante;
        }
    }

    public class Comprobante
    {
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string Sello { get; set; }
        public string FormaPago { get; set; }
        public string NoCertificado { get; set; }
        public string Certificado { get; set; }
        public string CondicionesDePago { get; set; }
        public string SubTotal { get; set; }
        public string Descuento { get; set; }
        public string Moneda { get; set; }
        public string Total { get; set; }
        public string TipoDeComprobante { get; set; }
        public string MetodoPago { get; set; }
        public string LugarExpedicion { get; set; }
        public List<Concepto> Conceptos { get; set; }
        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public override string ToString() {
            return this.Serie + this.Folio + this.Fecha + this.Sello + this.FormaPago + this.NoCertificado + this.Certificado + this.CondicionesDePago + this.SubTotal + this.Descuento + this.Moneda + this.Total + this.TipoDeComprobante + this.MetodoPago + this.LugarExpedicion + this.Emisor.ToString() + this.Receptor.ToString() + String.Join(" ",(from c in this.Conceptos select c.ToString()));
        }
    }

    public class Emisor {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string RegimenFiscal { get; set; }
        public override string ToString() {
            return this.Rfc + this.Nombre + this.RegimenFiscal;
        }
    }

    public class Receptor
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ResidenciaFiscal { get; set; }
        public string NumRegIdTrib { get; set; }
        public string UsoCFDI { get; set; }
        public override string ToString() {
            return this.Rfc + this.Nombre + this.ResidenciaFiscal + this.NumRegIdTrib + this.UsoCFDI;
        }
    }

    public class Concepto {
        public string ClaveProdServ { get; set; }
        public string NoIdentificacion { get; set; }
        public string Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public string ValorUnitario { get; set; }
        public string Descuento { get; set; }
        public string Importe { get; set; }
        public override string ToString() {
            return this.ClaveProdServ + this.NoIdentificacion + this.Cantidad + this.ClaveUnidad + this.Unidad + this.Descripcion + this.ValorUnitario + this.Descuento + this.Importe;
        }
    }
}
