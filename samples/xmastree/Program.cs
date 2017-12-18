using System;
using System.Threading;

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
                Thread.Sleep(200);
                tree.AllOff();
                Thread.Sleep(200);
            }

            // tree.JustBlink();
            tree.UpAndDown();
            // tree.PinChecker();
            // tree.DayUp();

            // tree.PinDown();
            
            Logger.Log("Press key to quit.");
            Console.ReadKey();
        }
    }
}