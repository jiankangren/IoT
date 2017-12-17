using System;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace XmasTreeApp
{
    class Program
    {
        
        static void Main(string[] args)
        {   
            Logger.Log("It's christmas :-)");

            var tree = new XmasTree();

            for(int i=0;i<5; i++)
            {
                tree.AllOn();
                Thread.Sleep(1000);
                tree.AllOff();
                Thread.Sleep(1000);
            }

            tree.JustBlink();
            // tree.UpAndDown();
            
            Logger.Log("Press key to quit.");
            Console.ReadKey();
        }

        
    }
}