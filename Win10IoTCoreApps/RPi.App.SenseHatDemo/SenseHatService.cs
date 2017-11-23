using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RPi.App.SenseHatDemo
{
    public class SenseHatService
    {

        public void Run()
        {
            Task.Run(async () =>
            {
                ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

                while (1==1)
                {

                    senseHat.Sensors.HumiditySensor.Update();
                    var temp = senseHat.Sensors.Temperature;
                    UpdateSensor(temp);

                    Sleep(new TimeSpan(0, 0, 1));

                }

            }).ConfigureAwait(false);
        }

        private void UpdateSensor(double? temp)
        {
            //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
            //    CoreDispatcherPriority.Normal,
            //    () =>
            //    {
            //        _output.Text = text;

            //        // Feel free to add more UI stuff here! :-)
            //    });
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
