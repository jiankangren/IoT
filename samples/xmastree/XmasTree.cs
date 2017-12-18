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

        
        // gpio --> Day
        int[] gpio = new [] {
            9, 
            15, 
            16,
            17,
            18,
            19, 
            20,
            30,
            31,
            32, 
             8, // 0 Checked Stern!!!!
             7, // 1 Checked
             0, // 2 ??? falsch
            23, // 3 c
            29, // 4 C
             6, // 5 C
            10, // 6 C
            21, // 7 tbd falsch
            12, // 8 C
            27, // 9 C
             0, // 10 C
             2, // 11 C
            25, // 12 C
             5, // 13 C
            13, // 14 C
            26, // 15 C
            22, // 16 C
            28, // 17 C
            24, // 18 C
            21, // 19 C tbd
             1, // 20 C
            14, // 21 C
            11, // 22 C
             4, // 23 C
             3, // 24 C
            };

        Random rnd = new Random();

        #region Led Helper

        public void SwitchDay(int day, bool on) 
        {
            Logger.Log($"Day: {day} Status: {on}");
            int pinId = gpio[day];
            SwitchLed(pinId, on);
        }

        public void SwitchLed(int pinId, bool on) 
        {
            Logger.Log($"SwitchLed: GPIO: {pinId} Status: {on}");
            try 
            {
                var led = Pi.Gpio[pinId];
                led.PinMode = GpioPinDriveMode.Output;
                led.Write(on);
            }
            catch (Exception exc) 
            {
                Logger.Log($"SwitchLed: GPIO: {pinId} Status: {on}");
                Logger.Log(exc);
            }
        }

        public void ToggleLed(int pinId) 
        {
            try 
            {
                var led = Pi.Gpio[pinId];
                led.PinMode = GpioPinDriveMode.Input;
                bool on = led.Read();
                SwitchLed(pinId, !on);
            }
            catch (Exception exc) 
            {
                Logger.Log($"ToggleLed: GPIO-{pinId} ");
                Logger.Log(exc);
            }
        }

        #endregion

        #region Pin Checker

        public void PinChecker() 
        {
            int sleep = 5000;
            for(int i=0; i<60; i++)
            {
                Logger.Log($"Pin: {i}");
                SwitchLed(i, true);
                Thread.Sleep(sleep);
                SwitchLed(i, false);
                Thread.Sleep(sleep);
            }
        }

        #endregion

        #region Program AllOn

        public void AllOn() 
        {
            Logger.Log("All On");
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(gpio[i], true);
                Thread.Sleep(10);
            }
        }

        public void AllOff() 
        {
            Logger.Log("All Off");
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(gpio[i], false);
                Thread.Sleep(10);
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
            int sleep = 5000;

            // On
            AllOff();
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchLed(i, true);
                Thread.Sleep(sleep);
                SwitchLed(i, false);
                Thread.Sleep(sleep);
            }
        }

        public void DayUp() 
        {
            AllOff();
            int sleep = 3000;
            for(int i=0;i<gpio.Length;i++)
            {
                SwitchDay(i, true);
                Thread.Sleep(sleep);
                SwitchDay(i, false);
                Thread.Sleep(1000);
            }
        }


        public void PinDown() 
        {
            int sleep = 3000;
            for(int i=39;i>0;i--)
            {
                Logger.Log($"---> PIN {i}");
                SwitchLed(i, true);
                Thread.Sleep(sleep);
                SwitchLed(i, false);
                Thread.Sleep(500);
            }
        }
        #endregion

    }
}