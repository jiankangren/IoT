namespace HomeSensorApp.Models.Fezhat
{
    public class FezhatTemperatureSensor : BaseFezhatSensor
    {
        public override string Name => "Temperature";

        public override void GetValue()
        {
            var temp = _fezhat.GetTemperature();
            SensorValueUpdated(temp);
        }
    }
}
