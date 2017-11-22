using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeSensorApp.Models
{
    public class NotifyPropertyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal async void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                var currentDispatcher = App.AppDispatcher;
                if (currentDispatcher != null)
                {
                    await currentDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    });
                }

                //Task.Run(() => {
                //});
            }
        }
    }
}
