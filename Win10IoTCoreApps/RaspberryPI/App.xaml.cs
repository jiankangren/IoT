using HomeSensorApp.Models;
using HomeSensorApp.Services;
using HomeSensorApp.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HomeSensorApp
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static CoreDispatcher AppDispatcher { get; internal set; }

        private static ApplicationSettings _appSettings;
        public static ApplicationSettings AppSettings { get => _appSettings; set => _appSettings = value; }

        private static SensorService _sensorService;
        public static SensorService SensorService { get => _sensorService; set => _sensorService = value; }


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    //rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    rootFrame.Navigate(typeof(RootPage), e.Arguments);
                }
                // Ensure the current window is active

                // 
                // Custom
                //

                AppSettings = (ApplicationSettings)Resources["ApplicationSettings"];
                SensorService = (SensorService)Resources["SensorService"]; // new SensorService();
                //SensorService.CreateSensors(AppSettings);

                Window.Current.Activate();
                AppDispatcher = Window.Current.Dispatcher;
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
