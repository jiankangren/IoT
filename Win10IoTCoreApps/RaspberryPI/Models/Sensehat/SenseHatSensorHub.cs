using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using System.Threading;

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
                _sensehat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

                if (_sensehat == null)
                {
                    _isSensorHubInstalled = false;
                    return;
                }

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

                // Compass
                var compass = new Compass();
                compass.SetSensehat(_sensehat);
                AddSensor(compass);

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


        private void UpdateSensor(double? temp)
        {
            SensorUpdated?.Invoke(this, temp?.ToString());
        }

        public event EventHandler<string> SensorUpdated;


        private readonly ManualResetEventSlim _waitEvent = new ManualResetEventSlim(false);
        protected void Sleep(TimeSpan duration)
        {
            _waitEvent.Wait(duration);
        }

    }
}
