namespace HomeSensorApp.Models
{
    public class SensorValuePackage
    {
        public object SensorValue { get; internal set; }
        public string SensorName { get; internal set; }
        public string SensorHubName { get; internal set; }
        public string SensorValueAsText { get; internal set; }
    }
}