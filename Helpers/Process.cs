using System;

namespace Pkgscan.Helpers
{
    public static class Process
    {
        public static void Terminate()
        {
            Environment.Exit(0);
        }

        public static void Terminate(string message)
        {
            Console.WriteLine(message);
            Environment.Exit(0);
        }
    }
}