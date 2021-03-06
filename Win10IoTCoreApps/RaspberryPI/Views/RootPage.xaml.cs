﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HomeSensorApp.Views
{
    public sealed partial class RootPage : Page
    {
        public RootPage()
        {
            this.InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateTo("sensors");
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                NavigateTo((string)item.Tag);
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigateTo((string)args.InvokedItem);
            }
        }

        private void NavigateTo(string invokedItem)
        {
            switch (invokedItem)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(HomePage));
                    break;
                
                case "sensors":
                    ContentFrame.Navigate(typeof(PureSensorViewPage));
                    break;
                case "softwaresensors":
                    ContentFrame.Navigate(typeof(VirtualSensorsPage));
                    break;

                case "devicetwin":
                    ContentFrame.Navigate(typeof(DeviceTwinPage));
                    break;
            }
            NavView.IsPaneOpen = false;
        }
    }
}