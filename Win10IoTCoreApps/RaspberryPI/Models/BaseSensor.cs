using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSensorApp.Models
{
    public abstract class BaseSensor : NotifyPropertyBase
    {

        public BaseSensor()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            App.AppSettings.SettingsUpdated += AppSettings_SettingsUpdated;
        }

        private void AppSettings_SettingsUpdated(object sender, EventArgs e)
        {
            UpdateTimer();
        }

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
                GetValue();
                UpdateTimer();
                OnPropertyChanged();
            }
        }

        Timer timer;

        private void UpdateTimer()
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
            set { _sensorValue = value; OnPropertyChanged(); LastUpdate = DateTime.Now;  }
        }

        public BaseSensorHub Parent { get; internal set; }
        public DateTime LastUpdate { get => _lastUpdate; set { _lastUpdate = value; OnPropertyChanged(); } }

        private DateTime _lastUpdate;

        public void SensorValueUpdated(object sensorValue)
        {
            if (sensorValue == null)
            {
                return;
            }

            SensorValue = sensorValue.ToString();
            if (SensorValuesUpdated != null)
            {
                var args = new SensorValuePackage();
                args.SensorName = Name;
                args.SensorValue = sensorValue;
                args.SensorHubName = Parent.Name;
                SensorValuesUpdated(this, args);
            }
        }

        public event EventHandler<SensorValuePackage> SensorValuesUpdated;
        
    }
}
