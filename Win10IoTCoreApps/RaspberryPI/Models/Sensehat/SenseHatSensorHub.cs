using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using System.Threading;
using Windows.UI.Xaml;

namespace HomeSensorApp.Models.Sensehat
{
    public class SenseHatSensorHub : BaseSensorHub
    {
        public SenseHatSensorHub()
        {
            
        }

        public override async void StartInitialize()
        {
            try
            {
                _sensehat = await SenseHatFactory.GetSenseHat();

                // Pressure
                var pressure = new SensehatPressureSensor();
                pressure.SetSensehat(_sensehat);
                AddSensor(pressure);

                // Humidity
                var humidity = new SensehatHumiditySensor();
                humidity.SetSensehat(_sensehat);
                AddSensor(humidity);

                // Temperature Sensor
                var temperature = new SensehatTemperatureSensor();
                temperature.SetSensehat(_sensehat);
                AddSensor(temperature);

                if (_sensehat != null)
                {
                    _isSensorHubInstalled = true;
                }

                OnSuccessfulInitialized();
                
            }
            catch (Exception exc)
            {
                _isSensorHubInstalled = false;
                Console.WriteLine(exc.Message);
                OnInitializationFailed(exc);
            }
        }

        ISenseHat _sensehat;

        private bool _isSensorHubInstalled = true;
        public override bool IsSensorHubInstalled { get { return _isSensorHubInstalled; } }

        public override string Name => "Sensehat";

        
    }
}
