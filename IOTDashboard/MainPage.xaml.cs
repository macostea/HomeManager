using Common.SensorServiceAPI;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IOTDashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Weather LastWeather { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            this.GetWeather();
        }

        private async void GetWeather()
        {
            var client = new SensorServiceAPI("http://sensor-service.mcostea.com");
            var weatherList = await client.GetWeather("24039b7c-bf70-4a7d-b562-96ff4382ba78", DateTime.Today, DateTime.Now);

            weatherList.Sort((w1, w2) => w1.Timestamp.CompareTo(w2.Timestamp));

            this.LastWeather = weatherList.Last();
        }
    }
}
