using System;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;


namespace XmasTreeApp
{
    public class XmasTree
    {
        public XmasTree() 
        {
            Logger.Log("XmasTree created.");
        }

        
        int[] gpio = new [] {24,6,27,14,2,21,25,13,7,23,1,3,22,28,26,5,4,12,11,29};
        Random rnd = new Random();

        public void SwitchLed(int pinId, bool on) 
        {
            // Version 1
            Logger.Log($"SwitchLed: GPIO: {pinId} Status: {on}");
            try 
            {
                var led = Pi.Gpio[pinId];
                led.PinMode = GpioPinDriveMode.Output;
                led.Write(on);
            }
            catch (Exception exc) 
            {
                Logger.Log(exc);
            }
        }

        public void ToggleLed(int pinId) 
        {
            Logger.Log($"ToggleLed: GPIO-{pinId} ");
            try 
            {
                var led = Pi.Gpio[pinId];
                led.PinMode = GpioPinDriveMode.Input;
                bool on = led.Read();
                SwitchLed(pinId, !on);
            }
            catch (Exception exc) 
            {
                Logger.Log(exc);
            }
        }

        #region Program AllOn

        public void AllOn() 
        {
            Logger.Log("All On");
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(gpio[i], true);
            }
        }

        public void AllOff() 
        {
            Logger.Log("All Off");
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(gpio[i], false);
            }
        }

        #endregion

        #region Random Blink

        public void JustBlink() 
        {
            int dueTime = 100;
            int period = 100;
                        
            Timer timer = new Timer(timerTick, null, dueTime, period);
        }
        
        private void timerTick(object state)
        {
            int pin = gpio[rnd.Next(gpio.Length)];
            bool on = rnd.Next(2) == 0;
            SwitchLed(pin, on);
            Thread.Sleep(100);
        }

        #endregion

        #region UpAndDown

        public void UpAndDown() 
        {
            // On
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(i, true);
                Thread.Sleep(1000);
            }

            // Off
            for(int i=gpio.Length-1;i>=0;i--)
            {
                SwitchLed(i, false);
                Thread.Sleep(1000);
            }
        }

        #endregion

    }
}