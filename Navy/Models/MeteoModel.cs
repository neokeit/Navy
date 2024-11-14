using Newtonsoft.Json;
using System.Collections.Generic;

namespace Navy.Models
{
    public class Attributes
    {
        public string fecha { get; set; }
        public string orto { get; set; }
        public string ocaso { get; set; }
        public string periodo { get; set; }
    }

    public class Breadcrumb
    {
        public string name { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }

    public class Hoy
    {
        [JsonProperty("@attributes")]
        public Attributes attributes { get; set; }
        public List<string> estado_cielo { get; set; }
        public List<string> precipitacion { get; set; }
        public List<string> prob_precipitacion { get; set; }
        public List<string> prob_tormenta { get; set; }
        public List<string> nieve { get; set; }
        public List<string> prob_nieve { get; set; }
        public List<string> temperatura { get; set; }
        public List<string> sens_termica { get; set; }
        public List<string> humedad_relativa { get; set; }
        public List<Viento> viento { get; set; }
        public List<string> racha_max { get; set; }
        public List<string> estado_cielo_descripcion { get; set; }
    }

    public class HumedadRelativa
    {
        public string maxima { get; set; }
        public string minima { get; set; }
    }

    public class Manana
    {
        [JsonProperty("@attributes")]
        public Attributes attributes { get; set; }
        public List<string> estado_cielo { get; set; }
        public List<string> precipitacion { get; set; }
        public List<string> prob_precipitacion { get; set; }
        public List<string> prob_tormenta { get; set; }
        public List<string> nieve { get; set; }
        public List<string> prob_nieve { get; set; }
        public List<string> temperatura { get; set; }
        public List<string> sens_termica { get; set; }
        public List<string> humedad_relativa { get; set; }
        public List<Viento> viento { get; set; }
        public List<string> racha_max { get; set; }
        public List<string> estado_cielo_descripcion { get; set; }
    }

    public class Municipio
    {
        public string CODIGOINE { get; set; }
        public string ID_REL { get; set; }
        public string COD_GEO { get; set; }
        public string CODPROV { get; set; }
        public string NOMBRE_PROVINCIA { get; set; }
        public string NOMBRE { get; set; }
        public int POBLACION_MUNI { get; set; }
        public double SUPERFICIE { get; set; }
        public int PERIMETRO { get; set; }
        public string CODIGOINE_CAPITAL { get; set; }
        public string NOMBRE_CAPITAL { get; set; }
        public string POBLACION_CAPITAL { get; set; }
        public string HOJA_MTN25 { get; set; }
        public double LONGITUD_ETRS89_REGCAN95 { get; set; }
        public double LATITUD_ETRS89_REGCAN95 { get; set; }
        public string ORIGEN_COORD { get; set; }
        public int ALTITUD { get; set; }
        public string ORIGEN_ALTITUD { get; set; }
        public int DISCREPANTE_INE { get; set; }
    }

    public class Origin
    {
        public string productor { get; set; }
        public string web { get; set; }
        public string language { get; set; }
        public string copyright { get; set; }
        public string nota_legal { get; set; }
        public string descripcion { get; set; }
    }

    public class Pronostico
    {
        public Hoy hoy { get; set; }
        public Manana manana { get; set; }
    }

    public class ProximosDia
    {
        [JsonProperty("@attributes")]
        public Attributes attributes { get; set; }
        public object prob_precipitacion { get; set; }
        public object cota_nieve_prov { get; set; }
        public object estado_cielo { get; set; }
        public object viento { get; set; }
        public object racha_max { get; set; }
        public Temperatura temperatura { get; set; }
        public SensTermica sens_termica { get; set; }
        public HumedadRelativa humedad_relativa { get; set; }
        public string uv_max { get; set; }
        public object estado_cielo_descripcion { get; set; }
    }

    public class MeteoModel
    {
        public Origin origin { get; set; }
        public string title { get; set; }
        public string metadescripcion { get; set; }
        public string keywords { get; set; }
        public Municipio municipio { get; set; }
        public string fecha { get; set; }
        public StateSky stateSky { get; set; }
        public string temperatura_actual { get; set; }
        public Temperaturas temperaturas { get; set; }
        public string humedad { get; set; }
        public string viento { get; set; }
        public string precipitacion { get; set; }
        public string lluvia { get; set; }
        public object imagen { get; set; }
        public Pronostico pronostico { get; set; }
        public List<ProximosDia> proximos_dias { get; set; }
        public List<Breadcrumb> breadcrumb { get; set; }
    }

    public class SensTermica
    {
        public string maxima { get; set; }
        public string minima { get; set; }
    }

    public class StateSky
    {
        public string description { get; set; }
        public string id { get; set; }
    }

    public class Temperatura
    {
        public string maxima { get; set; }
        public string minima { get; set; }
    }

    public class Temperaturas
    {
        public string max { get; set; }
        public string min { get; set; }
    }

    public class Viento
    {
        [JsonProperty("@attributes")]
        public Attributes attributes { get; set; }
        public string direccion { get; set; }
        public string velocidad { get; set; }
    }
}
