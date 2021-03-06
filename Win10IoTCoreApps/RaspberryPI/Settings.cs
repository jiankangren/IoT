﻿namespace HomeSensorApp
{
    public static class Settings
    {
        public static int DefaultUpdateInterval = 30000;
        public static string DefaultHostName = "oscheeriothub.azure-devices.net";

        // 
        // Windows App on Windows - Surface
        // 
        //public static string DefaultDeviceId = "windows001";
        //public static string DefaultSharedAccessKey = "Hmg0mqGgNjzW2B3nJnH/S0obyblhdSddm4ioHKbnVpc=";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = false;
        //public static bool IsDisplayConnected = true;
        //public static bool IsSimulatedSensorAvailable = true;

        // 
        // Windows App on Windows - HP7Stream
        // 
        //public static string DefaultDeviceId = "windows002";
        //public static string DefaultSharedAccessKey = "";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = false;
        //public static bool IsDisplayConnected = true;
        //public static bool IsSimulatedSensorAvailable = true;

        // 
        // winpi001 Settings - Smarti PI
        // 
        public static string DefaultDeviceId = "winpi001";
        public static string DefaultSharedAccessKey = "V05R254hEkusyL6ud27HVgeQpzAoAmNIXQRtDUBDAaI=";
        public static bool IsSensehatInstalled = true;
        public static bool IsFezhatInstalled = !IsSensehatInstalled;
        public static bool IsDisplayConnected = true;
        public static bool IsSimulatedSensorAvailable = false;

        //
        // winpi002 Settings
        // 
        //public static string DefaultDeviceId = "winpi002";
        //public static string DefaultSharedAccessKey = "MzPyxK/gKuPTEWZC//fU+vOdPUqVCf9VVrTzhNNUZug=";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = !IsSensehatInstalled;
        //public static bool IsDisplayConnected = false;
        //public static bool IsSimulatedSensorAvailable = false;

        // 
        // winpi003 Settings
        // 
        //public static string DefaultDeviceId = "winpi003";
        //public static string DefaultSharedAccessKey = "V9RT1ug5OgOup+lErYvpOTFVh5ZyZeGh1m36TNWl1CE=";
        //public static bool IsSensehatInstalled = false;
        //public static bool IsFezhatInstalled = !IsSensehatInstalled;
        //public static bool IsDisplayConnected = false;
        //public static bool IsSimulatedSensorAvailable = false;



    }
}
