using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Navy.Models;
using Navy.Services;
using Newtonsoft.Json;

namespace Navy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _filePath = "Config.conf";
        private readonly DispatcherTimer _timer;
        private ConfigModel _config;

        public MainWindow()
        {
            InitializeComponent();
            
            _timer = new DispatcherTimer();
            _timer.Tick += timer_elapsed;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }
        private void LoadConfig()
        {
            if (!File.Exists(_filePath)) File.WriteAllText(_filePath,"");
            var file =  File.ReadAllText(_filePath);
            _config = JsonConvert.DeserializeObject<ConfigModel>(file);
            LoadSeries();
        }

        private void LoadSeries()
        {
            lstSeries.Items.Clear();
            foreach (var serie in _config.Series)
            {
                lstSeries.Items.Add(string.IsNullOrEmpty(serie.Title) ? serie.Url : serie.Title);    
            }
        }

        private void timer_elapsed(object? sender, EventArgs e)
        {
            lbLog.Text += "Paramos el timer";
            _timer.Stop();
            lbLog.Text += Environment.NewLine + "Cargamos configuración";
            LoadConfig();
            lbLog.Text += Environment.NewLine + "Check servicios";
            CheckServices();
            SaveConfig();
#if !DEBUG
            lbLog.Text += "Cerramos aplicacion";
            Thread.Sleep(5000);
            Application.Current.Shutdown();
#endif
        }

        private void CheckServices()
        {
            
            lbLog.Text += Environment.NewLine + " * Check Tiempo";
            var lluvias = MeteoAlertServicce.GetTiempo();
            lbLog.Text += Environment.NewLine + " * Check Capitulos";
            _config.Series = SeriesService.CheckCaps(_config.Series);
            lbLog.Text += Environment.NewLine + " Enviamos Mail";
            Mail(lluvias,_config.Series);
        }

        private void Mail(string lluvia, List<SerieModel> series)
        {
            string subject ="Resumen diario - " + DateTime.Now.ToString("f");
            string body= "Hoy parece que " +  (string.IsNullOrEmpty(lluvia) ? "no va a llover" : " va a llover un " + lluvia);
            string serieBody = "";

            foreach (var serie in series.Where(e=> e.Nuevo))
            {
                serieBody += "<li>" + serie.Title + "</li>";
            }

            if (!string.IsNullOrEmpty(serieBody))
            {
                serieBody = "<ul>" + serieBody+  "</ul>";
            }
            else
            {
                serieBody = "NO HAY CAPIS";
            }
            MailService.Send(subject,body,serieBody);
        }

        private void SaveConfig()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_config));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AñadirSerie();
        }

        private void AñadirSerie()
        {
            _config.Series.Add(new() { Url = NuevaSerie.Text });
            LoadSeries();
            SaveConfig();
            NuevaSerie.Text = "";
        }
    }
}