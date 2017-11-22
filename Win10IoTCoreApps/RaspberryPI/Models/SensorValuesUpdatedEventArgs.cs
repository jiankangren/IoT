namespace HomeSensorApp.Models
{
    public class SensorValuesUpdatedEventArgs
    {
        public object SensorValue { get; internal set; }
        public string SensorName { get; internal set; }
        public string SensorHubName { get; internal set; }
    }
}