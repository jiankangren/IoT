using HomeSensorApp.Models;
using HomeSensorApp.Models.Sensehat;
using HomeSensorApp.Models.Simulator;

namespace HomeSensorApp.Services
{
    public class SensorService
    {
        public SensorService(ApplicationSettings appSettings)
        {
            CreateSensors(appSettings);
        }

        private void CreateSensors(ApplicationSettings appSettings)
        {
            // WARNING: You can't try to initialize both Fezhat and Sensehat

            // 
            // Fezhat
            //  
            //var fezhat = new FezhatSensorHub();
            //fezhat.SuccessfulInitialized += (s, e) =>
            //{
            //    if (fezhat.IsSensorHubInstalled)
            //    {
            //        App.AppSettings.AddSensorHub(fezhat);
            //    }
            //};
            //fezhat.StartInitialize();

            // 
            // Sensehat
            // 
            var sensehat = new SenseHatSensorHub();
            sensehat.SuccessfulInitialized += (s, e) =>
            {
                if (sensehat.IsSensorHubInstalled)
                {
                    appSettings.AddSensorHub(sensehat);
                }
            };
            sensehat.StartInitialize();

            //// 
            //// Sensor Simulator
            //// 
            //var sim = new SimulatedSensorHub();
            //sim.StartInitialize();
            //if (sim.IsSensorHubInstalled)
            //{
            //    App.AppSettings.AddSensorHub(sim);
            //}
        }



        
    }
}
