namespace HomeSensorApp.Models.Simulator
{
    public class SimulatedSensorHub : BaseSensorHub
    {
        public override bool IsSensorHubInstalled { get => true;  }

        public override string Name => "Simulated Sensor Hub";

        public SimulatedSensorHub()
        {
            
        }

        public override void StartInitialize()
        {
            var temperature = new SimulatedTemperature();
            //temperature.UpdateIntervalEnabled = true;
            AddSensor(temperature);

            var windspeed = new SimulatedWindspeed();
            //windspeed.UpdateIntervalEnabled = true;
            AddSensor(windspeed);

            //foreach(var sensor in Sensors)
            //{
            //    sensor.UpdateIntervalEnabled = true;
            //}
        }
    }
}
