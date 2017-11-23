using HomeSensorApp.Services;
using System;
using Windows.Storage;

namespace HomeSensorApp.Models
{
    public class ApplicationSettings : NotifyPropertyBase
    {
        public ApplicationSettings()
        {
            _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }

        ApplicationDataContainer _localSettings;

        private void UpdateConnection()
        {
            SettingsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler SettingsUpdated;

        #region Strings

        public string ApplicationTitle
        {
            get { return "Home Sensors"; }
            set { }
        }

        #endregion

        #region User Settings

        public string DeviceConnectionString
        {
            get
            {
                string result = $"HostName={this.HostName};DeviceId={this.DeviceId};SharedAccessKey={this.SharedAccessKey}";
                return result;
            }
        }
        
        public int UpdateInterval
        {
            get
            {
                return GetValueAsInt("UpdateInterval", Settings.DefaultUpdateInterval);
            }
            set
            {
                SetValue("UpdateInterval", value);
            }
        }

        public string DeviceId
        {
            get
            {
                return GetValueAsString("DeviceId", Settings.DefaultDeviceId);
            }
            set
            {
                SetValue("DeviceId", value);
                UpdateConnection();
            }
        }

        public string HostName
        {
            get
            {
                return GetValueAsString("HostName", Settings.DefaultHostName);
            }
            set
            {
                SetValue("HostName", value);
                UpdateConnection();
            }
        }

        public string SharedAccessKey
        {
            get
            {
                return GetValueAsString("SharedAccessKey", Settings.DefaultSharedAccessKey);
            }
            set
            {
                SetValue("SharedAccessKey", value);
                UpdateConnection();
            }
        }

        public string Location { get; set; }

        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value; OnPropertyChanged();
            }
        }

        #endregion

        #region Internal Settings 

        private void SetValue(string propertyKey, object propertyValue)
        {
            _localSettings.Values[propertyKey] = propertyValue;
        }

        private object GetValue(string propertyKey, object defaultValue = null)
        {
            if (_localSettings.Values.ContainsKey(propertyKey))
            {
                return _localSettings.Values[propertyKey];
            }
            else
            {
                return defaultValue;
            }
        }

        private string GetValueAsString(string propertyKey, object defaultValue = null)
        {
            return (string)GetValue(propertyKey, (string)defaultValue);
        }

        private int GetValueAsInt(string propertyKey, object defaultValue = null)
        {
            return (int)GetValue(propertyKey, (int)defaultValue);
        }

        #endregion
    }
}