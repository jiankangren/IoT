using System;

namespace HomeSensorApp.Models.Simulator
{
    public class SimulatedWindspeed : BaseSensor
    {
        public override string Name => "Simulated Windspeed";

        public override void GetValue()
        {
            Random rnd = new Random();
            int speed = rnd.Next(200);
            SensorValueUpdated(speed);
        }
    }
}
