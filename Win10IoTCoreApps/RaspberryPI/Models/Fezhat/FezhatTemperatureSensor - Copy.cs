namespace HomeSensorApp.Models.Fezhat
{
    public class FezhatLightSensor : BaseFezhatSensor
    {
        public override string Name => "Light";

        public override void GetValue()
        {
            var temp = _fezhat.GetLightLevel();
            SensorValueUpdated(temp);
        }
    }
}
