namespace HomeSensorApp
{
    public static class Settings
    {
        public static int DefaultUpdateInterval = 60000;
        public static string DefaultHostName = "oscheeriothub.azure-devices.net";

        // winpi001 Settings
        public static string DefaultDeviceId = "winpi001";
        public static string DefaultSharedAccessKey = "V05R254hEkusyL6ud27HVgeQpzAoAmNIXQRtDUBDAaI=";
        public static bool IsSensehatInstalled = true;
        public static bool IsFezhatInstalled = !IsSensehatInstalled;

        // winpi002 Settings
        //public static string DefaultDeviceId = "winpi002";
        //public static string DefaultSharedAccessKey = "MzPyxK/gKuPTEWZC//fU+vOdPUqVCf9VVrTzhNNUZug=";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = !IsSensehatInstalled;

        // winpi003 Settings
        //public static string DefaultDeviceId = "winpi003";
        //public static string DefaultSharedAccessKey = "V9RT1ug5OgOup+lErYvpOTFVh5ZyZeGh1m36TNWl1CE=";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = !IsSensehatInstalled;

        public static bool IsSimulatedSensorAvailable = false;


    }
}
