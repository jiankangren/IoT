using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSensorApp.Models
{
    public abstract class BaseSensor : NotifyPropertyBase
    {

        public abstract string Name { get; }

        private bool _updateIntervalEnabled = false;
        public bool UpdateIntervalEnabled
        {
            get
            {
                return _updateIntervalEnabled;
            }
            set
            {
                _updateIntervalEnabled = value;
                UpdateTimer();
                OnPropertyChanged();
            }
        }

        //DispatcherTimer _timer;
        Timer timer;

        private async void UpdateTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
            }

            if (UpdateIntervalEnabled == false)
            {
                return;
            }

            timer = new Timer(timerCallback, null, App.AppSettings.UpdateInterval, App.AppSettings.UpdateInterval);
        }

        private void timerCallback(object state)
        {
            Task.Run(() =>
            {
                GetValue();
            });
        }
        
        public abstract void GetValue();

        private string _sensorValue = "n/a";
        public string SensorValue
        {
            get { return _sensorValue; }
            set { _sensorValue = value; OnPropertyChanged(); }
        }

        public BaseSensorHub Parent { get; internal set; }

        public void SensorValueUpdated(object sensorValue)
        {
            if (sensorValue == null)
            {
                return;
            }

            SensorValue = sensorValue.ToString();
            if (SensorValuesUpdated != null)
            {
                var args = new SensorValuesUpdatedEventArgs();
                args.SensorName = Name;
                args.SensorValue = sensorValue;
                args.SensorHubName = Parent.Name;
                SensorValuesUpdated(this, args);
            }
        }

        public event EventHandler<SensorValuesUpdatedEventArgs> SensorValuesUpdated;
        
    }
}
