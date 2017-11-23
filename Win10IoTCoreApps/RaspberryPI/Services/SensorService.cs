using HomeSensorApp.Models;
using HomeSensorApp.Models.Fezhat;
using HomeSensorApp.Models.Sensehat;
using HomeSensorApp.Models.Simulator;
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
            if (Settings.IsFezhatInstalled)
            {
                var fezhat = new FezhatSensorHub();
                fezhat.SuccessfulInitialized += (s, e) =>
                {
                    if (fezhat.IsSensorHubInstalled)
                    {
                        AddSensorHub(fezhat);
                    }
                };
                fezhat.StartInitialize();
            }

            // 
            // Sensehat
            // 
            if (Settings.IsSensehatInstalled)
            {
                var sensehat = new SenseHatSensorHub();
                sensehat.SuccessfulInitialized += (s, e) =>
                {
                    if (sensehat.IsSensorHubInstalled)
                    {
                        AddSensorHub(sensehat);
                    }
                };
                sensehat.StartInitialize();
            }

            // 
            // Sensor Simulator
            // 
            if (Settings.IsSimulatedSensorAvailable)
            {
                var sim = new SimulatedSensorHub();
                sim.StartInitialize();
                if (sim.IsSensorHubInstalled)
                {
                    AddSensorHub(sim);
                }
            }

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
                });
        }

        private void Sensor_SensorValuesUpdated(object sender, SensorValuePackage e)
        {
            string message = $"{e.SensorHubName}.{e.SensorName} = {e.SensorValue}";
            OnStatusMessageUpdate(message);

            OnSensorValueUpdated(e);
        }

        private void OnSensorValueUpdated(SensorValuePackage e)
        {
            SensorDataUpdated?.Invoke(this, e);
        }

        public event EventHandler<SensorValuePackage> SensorDataUpdated;

        private void OnStatusMessageUpdate(string message)
        {
            StatusMessageUpdated?.Invoke(this, new StatusMessageEventArgs() { Message = message });
        }

        public event EventHandler<StatusMessageEventArgs> StatusMessageUpdated;


    }
}
