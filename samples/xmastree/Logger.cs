using System;

namespace XmasTreeApp
{

    public static class Logger 
    {

        public static void Log(string message) 
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}-{message}");
        }

        public static void Log(Exception exc) 
        {
            Console.WriteLine(exc.Message);
            Console.WriteLine(exc.StackTrace);
            if (exc.InnerException != null)
            {
                Log(exc.InnerException);
            }
        }

    }
}