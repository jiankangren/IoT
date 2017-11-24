using System;
using System.Collections.Generic;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HomeSensorApp.Controls
{
    public sealed partial class ClockControl : UserControl
    {
        public ClockControl()
        {
            this.InitializeComponent();

            this.Loaded += ClockControl_Loaded;
        }

        DispatcherTimer _timer = new DispatcherTimer();

        private void ClockControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            _timer.Tick += (s, e) =>
            {
                var dt = DateTime.Now;
                _clock.Text = dt.ToString("HH:mm:ss dd.MM.yyyy");
            };
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
    }
}
