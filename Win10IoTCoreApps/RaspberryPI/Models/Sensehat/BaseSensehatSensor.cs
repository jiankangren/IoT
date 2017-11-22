using Emmellsoft.IoT.Rpi.SenseHat;

namespace HomeSensorApp.Models.Sensehat
{
    public abstract class BaseSensehatSensor : BaseSensor
    {
        public ISenseHat _sensehat;

        public void SetSensehat(ISenseHat sensehat)
        {
            _sensehat = sensehat;
        }
    }
}
