using Navy.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Navy.Services
{
    public class SeriesService
    {
        public static List<SerieModel> CheckCaps(List<SerieModel> series)
        {
            if (series!=null && series.Any())
            {
                for (var index = 0; index < series.Count; index++)
                {
                    series[index] = SearchCaps(series[index]);
                }
            }
            return series;
        }
        private static SerieModel SearchCaps(SerieModel serie)
        {
            if (string.IsNullOrEmpty(serie.UrlLast))
            {
                var response = CallUrl(serie.Url);
                var lines = response.Split(new[] { "\n" }, StringSplitOptions.None);

                foreach (var line in lines)
                {
                    if (line.Contains("/ajax/last_episode/"))
                    {
                        var url = line.Replace("$.get('", "");
                        serie.UrlLast = "https://jkanime.bz" + url.Replace("', function( data ) {", "").Trim();
                        break;
                    }
                }
            }
            var responsejs = GetResponse(serie.UrlLast);
            if (responsejs != null)
            {
                var responseJk = JsonConvert.DeserializeObject<List<ResponseJk>>(responsejs)!.FirstOrDefault();
                if (responseJk != null)
                {
                    if (serie.Number != responseJk.Number)
                    {
                        serie.Nuevo = true;
                        serie.Number = responseJk.Number;
                        serie.Timestamp = responseJk.Timestamp;
                        if (string.IsNullOrEmpty(serie.Title)) serie.Title = responseJk.Title;
                    }
                }
            }
            return serie;
        }
        private static string? GetResponse(string url)
        {
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            var request = new RestRequest(url);
            var response = client.Execute(request);
            return response.Content;
        }

        private static string CallUrl(string fullUrl)
        {
            var client = new HttpClient();
            return client.GetStringAsync(fullUrl).Result;
        }
    }
}
