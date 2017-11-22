using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSensorApp.Models.Simulator
{
    public class SimulatedTemperature : BaseSensor
    {
        public override string Name => "Simulated Temperature";

        public override void GetValue()
        {
            Random rnd = new Random();
            int temp = rnd.Next(50) - 20;
            SensorValueUpdated(temp);
        }
    }
}
