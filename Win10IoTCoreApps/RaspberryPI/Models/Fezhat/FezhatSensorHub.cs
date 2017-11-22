using GHIElectronics.UWP.Shields;
using System;
using System.Threading.Tasks;

namespace HomeSensorApp.Models.Fezhat
{
    public class FezhatSensorHub : BaseSensorHub
    {
        public FezhatSensorHub()
        {

        }

        public override async void StartInitialize()
        {
            try
            {
                _FEZHAT = await GHIElectronics.UWP.Shields.FEZHAT.CreateAsync();

                // Temperature
                var temp = new FezhatTemperatureSensor();
                temp.SetFezhat(_FEZHAT);
                AddSensor(temp);

                // Light
                var light = new FezhatLightSensor();
                light.SetFezhat(_FEZHAT);
                AddSensor(light);

                OnSuccessfulInitialized();
            }
            catch (Exception exc)
            {
                _isSensorHubInstalled = false;
                Console.WriteLine(exc.Message);

                OnInitializationFailed(exc);
            }
        }

        private bool _isSensorHubInstalled = true;
        private FEZHAT _FEZHAT;

        public override bool IsSensorHubInstalled { get { return _isSensorHubInstalled; } }

        public override string Name => "Fezhat";

    }
}
