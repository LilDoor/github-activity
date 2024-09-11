using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_activity.Utility
{
    internal class ConsoleMsg
    {
        public static void PrintInfoMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + msg + "\n");
            Console.ResetColor();
        }
        public static void PrintErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + msg + "\n");
            Console.ResetColor();
        }
        public static void PrintCommandMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + msg + "\n");
            Console.ResetColor();
        }
    }
}
