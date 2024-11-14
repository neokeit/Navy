using System.Globalization;
using Navy.Models;
using Navy.Services.Helper;

namespace Navy.Services
{
    internal class MeteoAlertServicce
    {       
        internal static string GetTiempo()
        {
            var meteo = RestApiHelper.SendRestApi<MeteoModel>("https://www.el-tiempo.net/api/json/v2/provincias/28/municipios/28007");
            if (!string.IsNullOrEmpty(meteo?.lluvia))
            {
                double total = 0.0;
                foreach (var valor in meteo.pronostico.hoy.precipitacion)
                {
                    total += double.Parse(valor);
                }
                return total != 0 ? total.ToString(CultureInfo.InvariantCulture) : meteo.lluvia;
            }
            return "";
        }
    }
}
