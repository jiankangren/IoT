﻿namespace HomeSensorApp.Models.Sensehat
{
    public class SensehatTemperatureSensor : BaseSensehatSensor
    {
        public override string Name => "Temperature";

        public override void GetValue()
        {
            _sensehat.Sensors.ImuSensor.Update();
            var temp = _sensehat.Sensors.Temperature;
            SensorValueUpdated(temp);
        }
    }
}
