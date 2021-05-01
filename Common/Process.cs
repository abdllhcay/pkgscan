using System;

namespace Pkgscan.Common
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
            Console.WriteLine();
            Environment.Exit(0);
        }
    }
}