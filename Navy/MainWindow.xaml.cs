using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using RestSharp;
using Navy.Services;

namespace Navy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _filePath = "Config.conf";
        public MainWindow()
        {
            InitializeComponent();
            LoadInfo();
        }

        private void LoadInfo()
        {
            var lluvias = MeteoAlertServicce.GetTiempo();
            Mail(lluvias);
            //CheckCaps();
        }

        private void Mail(string lluvia)
        {
            string subject ="Resumen diario - " + DateTime.Now.ToString("f");
            string body= "Hoy parece que " +  (string.IsNullOrEmpty(lluvia) ? "no va a llover" : " va a llover un " + lluvia);
            MailService.Send(subject,body);
        }


        private void CheckCaps()
        {
            if (!File.Exists(_filePath)) File.WriteAllText(_filePath,"");
            var file =  File.ReadAllText(_filePath);
            var series = JsonConvert.DeserializeObject<List<ListaCapis>>(file);
            if (series != null && series.Any())
            {
                Parallel.ForEach(series, serie =>
                {
                    SearchCaps(serie);
             
                });
                SaveInfoCaps(series);
            }
        }
        private void SearchCaps(ListaCapis serie)
        {
            var response = CallUrl(serie.Url);
            string[] lines = response.Split(new[] { "\n" }, StringSplitOptions.None);

            
            foreach (var line in lines)
            {
                if (line.Contains("/ajax/last_episode/"))
                {
                    var url = line.Replace("$.get('", "");
                    serie.UrlLast ="https://jkanime.bz"+ url.Replace("', function( data ) {", "").Trim();
                    break;
                }
            }

            if (!string.IsNullOrEmpty(serie.UrlLast))
            {    var responsejs =  GetResponse(serie.UrlLast);
                var ResponseJk = JsonConvert.DeserializeObject<List<ResponseJk>>(responsejs).FirstOrDefault();
                serie.Number = ResponseJk.Number;
                serie.Timestamp = ResponseJk.Timestamp;
                if (string.IsNullOrEmpty(serie.Title)) serie.Title = ResponseJk.Title;
            }
        }

        private void SaveInfoCaps(List<ListaCapis> series)
        {
            var json = JsonConvert.SerializeObject(series);
            File.WriteAllText(_filePath, json);
        }

        private string GetResponse(string url)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url, Method.Get);
            RestResponse response = client.Execute(request);
            return response.Content;
        }

        private string CallUrl(string fullUrl)
        {
            var client = new HttpClient();
            var response = client.GetStringAsync(fullUrl).Result;
            return response;
        }
    }
}