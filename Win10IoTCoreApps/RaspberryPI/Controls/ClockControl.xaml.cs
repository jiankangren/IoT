using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace HomeSensorApp.Controls
{
    public sealed partial class ClockControl : UserControl
    {
        public ClockControl()
        {
            this.InitializeComponent();

            this.Loaded += ClockControl_Loaded;
            this.Unloaded += ClockControl_Unloaded;
        }

        private void ClockControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        DispatcherTimer _timer = new DispatcherTimer();

        private void ClockControl_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Tick += (s, ee) =>
            {
                var dt = DateTime.Now;
                _clock.Text = dt.ToString("HH:mm:ss dd.MM.yyyy");
            };
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
    }
}
