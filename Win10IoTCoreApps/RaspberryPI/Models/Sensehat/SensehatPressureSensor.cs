namespace HomeSensorApp.Models.Sensehat
{
    public class SensehatPressureSensor : BaseSensehatSensor
    {
        public override string Name => "Pressure";

        public override void GetValue()
        {
            _sensehat.Sensors.PressureSensor.Update();
            var pressure = _sensehat.Sensors.Pressure;
            SensorValueUpdated(pressure);
        }
        
    }
}
