using HomeSensorApp.Models;
using HomeSensorApp.Models.Sensehat;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace HomeSensorApp.Services
{
    public class SensorService
    {
        public SensorService()
        {
            CreateSensors();
        }

        private void CreateSensors()
        {
            // WARNING: You can't try to initialize both Fezhat and Sensehat

            // 
            // Fezhat
            //  
            //var fezhat = new FezhatSensorHub();
            //fezhat.SuccessfulInitialized += (s, e) =>
            //{
            //    if (fezhat.IsSensorHubInstalled)
            //    {
            //        App.AppSettings.AddSensorHub(fezhat);
            //    }
            //};
            //fezhat.StartInitialize();

            // 
            // Sensehat
            // 
            var sensehat = new SenseHatSensorHub();
            sensehat.SuccessfulInitialized += (s, e) =>
            {
                if (sensehat.IsSensorHubInstalled)
                {
                    AddSensorHub(sensehat);
                }
            };
            sensehat.StartInitialize();

            //// 
            //// Sensor Simulator
            //// 
            //var sim = new SimulatedSensorHub();
            //sim.StartInitialize();
            //if (sim.IsSensorHubInstalled)
            //{
            //    App.AppSettings.AddSensorHub(sim);
            //}
        }

        private ObservableCollection<BaseSensorHub> _sensorHubs = new ObservableCollection<BaseSensorHub>();
        public ObservableCollection<BaseSensorHub> SensorHubs { get => _sensorHubs; set => _sensorHubs = value; }

        private async void AddSensorHub(BaseSensorHub sensorHub)
        {
            foreach (var sensor in sensorHub.Sensors)
            {
                sensor.SensorValuesUpdated += Sensor_SensorValuesUpdated;
            }

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    _sensorHubs.Add(sensorHub);
                    //OnPropertyChanged("SensorHubs");
                    //_output.Text = text;
                });
        }

        private void Sensor_SensorValuesUpdated(object sender, SensorValuesUpdatedEventArgs e)
        {
            string message = $"{e.SensorHubName}.{e.SensorName} = {e.SensorValue}";
            //StatusMessage = message + Environment.NewLine + StatusMessage;

            OnStatusMessageUpdate(message);

            // Send Message to IoT Hub
            //_iotHub.SendDeviceToCloud(e.SensorHubName, e.SensorName, e.SensorValue);
        }

        private void OnStatusMessageUpdate(string message)
        {
            StatusMessageUpdated?.Invoke(this, new StatusMessageEventArgs() { Message = message });
        }

        public event EventHandler<StatusMessageEventArgs> StatusMessageUpdated;


    }
}
