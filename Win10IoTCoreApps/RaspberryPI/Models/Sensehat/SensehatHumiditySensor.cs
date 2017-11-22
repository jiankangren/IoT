namespace HomeSensorApp.Models.Sensehat
{
    public class SensehatHumiditySensor : BaseSensehatSensor
    {
        public override string Name => "Humidity";

        public override void GetValue()
        {
            _sensehat.Sensors.HumiditySensor.Update();
            var pressure = _sensehat.Sensors.Humidity;
            SensorValueUpdated(pressure);
        }
        
    }
}
