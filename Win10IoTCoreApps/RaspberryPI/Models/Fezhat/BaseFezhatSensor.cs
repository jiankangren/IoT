using GHIElectronics.UWP.Shields;

namespace HomeSensorApp.Models.Fezhat
{
    public abstract class BaseFezhatSensor : BaseSensor
    {
        public FEZHAT _fezhat;

        public void SetFezhat(FEZHAT fezhat)
        {
            _fezhat = fezhat;
        }
    }
}
