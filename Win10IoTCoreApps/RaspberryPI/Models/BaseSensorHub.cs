using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace HomeSensorApp.Models
{
    public abstract class BaseSensorHub : NotifyPropertyBase
    {
        public abstract bool IsSensorHubInstalled { get; }

        internal bool _isInitialized = false;

        private List<BaseSensor> _sensors = new List<BaseSensor>();
        public BaseSensor[] Sensors
        {
            get
            {
                return _sensors.ToArray();
            }
        }

        public abstract string Name { get; }

        public string StatusMessage { get; set; }

        public void AddSensor(BaseSensor sensor)
        {
            sensor.Parent = this;
            sensor.UpdateIntervalEnabled = true;
            _sensors.Add(sensor);
        }

        public abstract void StartInitialize();

        public event EventHandler SuccessfulInitialized;
        public event EventHandler InitializationFailed;

        public async void OnSuccessfulInitialized()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    _isInitialized = true;
                    this.StatusMessage = "Running";
                    SuccessfulInitialized?.Invoke(this, EventArgs.Empty);
                    StartTimer();
                });
        }

        private void StartTimer()
        {
            DispatcherTimer delayTimer = new DispatcherTimer();
            delayTimer.Interval = new TimeSpan(0, 0, App.AppSettings.UpdateInterval);
            delayTimer.Tick += (s, e) =>
            {
                delayTimer.Stop();
                delayTimer.Interval = new TimeSpan(0, 0, App.AppSettings.UpdateInterval);

                foreach (var sensor in Sensors)
                {
                    sensor.UpdateIntervalEnabled = true;
                }
                delayTimer.Start();
            };
            delayTimer.Start();
        }

        public void OnInitializationFailed(Exception exc)
        {
            this.StatusMessage = "Exc: " + exc.Message;
            if (InitializationFailed != null)
            {
                InitializationFailed(this, EventArgs.Empty);
            }
        }
    }
}
