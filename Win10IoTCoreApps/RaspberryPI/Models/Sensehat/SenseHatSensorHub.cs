using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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

            //Task.Run(async () =>
            //{
            //    try
            //    {

            //        ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

            //        while (1 == 1)
            //        {

            //            senseHat.Sensors.HumiditySensor.Update();
            //            var temp = senseHat.Sensors.Temperature;
            //            Debug.WriteLine($"Temp: {temp}");
            //            //UpdateSensor(temp);
            //            //Sleep(new TimeSpan(0, 0, 1));
            //        }
            //    }
            //    catch (Exception exc)
            //    {
            //        Debug.WriteLine(exc.Message);
            //    }


            //}).ConfigureAwait(false);

            try
            {
                //Task.Run(async () =>
                //{
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

                    OnSuccessfulInitialized();

                    //SenseHatDemo demo = createDemo(senseHat);

                    //demo.Run();
                //}).ConfigureAwait(false);


                //_sensehat = await SenseHatFactory
                //    .GetSenseHat();
                //.ConfigureAwait(false)
                //.GetAwaiter()
                //.GetResult();
                //.ConfigureAwait(false)

                //// Pressure
                //var pressure = new SensehatPressureSensor();
                //pressure.SetSensehat(_sensehat);
                //AddSensor(pressure);

                //// Humidity
                //var humidity = new SensehatHumiditySensor();
                //humidity.SetSensehat(_sensehat);
                //AddSensor(humidity);

                //// Temperature Sensor
                //var temperature = new SensehatTemperatureSensor();
                //temperature.SetSensehat(_sensehat);
                //AddSensor(temperature);

                

                //OnSuccessfulInitialized();

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
