using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Drone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int posX = 0;
            int posY = 10;
            int battery = 50;
            while (battery >= 0)
            {
                Console.Clear();
                drawDrone(posX, posY, battery);
                drawDrone(posX, posY - 3, battery);
                drawDrone(posX, posY - 6, battery);
                changeState(ref posX, ref battery);
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }

        static void drawDrone(int posX, int posY, int battery)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(posX, posY);
            if (battery > 0)
                Console.Write("x-0-x");
            else
                Console.Write("_____");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(posX, posY - 1);

            if (battery > 0)
                Console.Write(battery + "%");
            else
                Console.Write("Drone appears to be suffering from lack of electrical input");
        }
        static void changeState(ref int posX, ref int battery)
        {
            posX += 1;
            battery -= 2;
        }
    }
}
