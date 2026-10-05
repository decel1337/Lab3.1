using System;
using System.Text;

namespace Lab3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var menu = new ConsoleMenu();
            menu.Run();
        }
    }
}