using HomeSensorApp.Models.Fezhat;
using HomeSensorApp.Models.Sensehat;
using HomeSensorApp.Models.Simulator;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HomeSensorApp.Views
{
    public sealed partial class RootPage : Page
    {
        public RootPage()
        {
            this.InitializeComponent();

            this.Loaded += RootPage_Loaded;
            Init();
        }

        #region Page Event Handler

        private void _settings_Click(object sender, RoutedEventArgs e)
        {
            _root.Navigate(typeof(SettingsPage));
        }

        private void _home_Click(object sender, RoutedEventArgs e)
        {
            _root.Navigate(typeof(PureSensorViewPage));
        }

        private void RootPage_Loaded(object sender, RoutedEventArgs e)
        {
            _root.Navigate(typeof(PureSensorViewPage));
            _virtual.Visibility = Settings.IsDisplayConnected ? Visibility.Visible : Visibility.Collapsed;
        }

        private void _virtual_Click(object sender, RoutedEventArgs e)
        {
            _root.Navigate(typeof(VirtualSensorsPage));
        }

        #endregion


        DispatcherTimer _timer = new DispatcherTimer();

        private void Init()
        {
            _timer.Tick += (s, e) =>
            {
                var dt = DateTime.Now;
                _clock.Text = dt.ToString("HH:mm:ss dd.MM.yyyy");
            };
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();

            // Create Sensors
        }

    }
}
