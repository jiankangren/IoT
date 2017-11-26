using HomeSensorApp.Models;
using HomeSensorApp.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace HomeSensorApp.Views
{
    public sealed partial class VirtualSensorsPage : Page
    {
        public VirtualSensorsPage()
        {
            this.InitializeComponent();

            this.Loaded += VirtualSensorsPage_Loaded;
        }

        private void VirtualSensorsPage_Loaded(object sender, RoutedEventArgs e)
        {
            _iotHubService = App.IotHubService;
        }

        IotHubService _iotHubService;

        private void _movementDetected_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("Movement", "detected");
        }


        private void _cameraPicture_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _temperature_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SendMessage("Temperature", _temperature.Value);
        }

        private void _light_Toggled(object sender, RoutedEventArgs e)
        {
            SendMessage("LightButton", _light.IsOn);
        }

        private void SendMessage(string sensorName, object sensorValue)
        {
            SensorValuePackage v = new SensorValuePackage();
            v.SensorHubName = "Software Sensors";
            v.SensorName = sensorName;
            v.SensorValue = sensorValue;
            _iotHubService.SendDeviceToCloud(v);
        }

        private void _windSpeed_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SendMessage("Windspeed", _windSpeed.Value);
        }
    }
}
