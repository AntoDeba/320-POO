using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Drone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            string drone = "x-0-x";
            int posX = 0;
            const int posY = 10;
            int battery = 50;
            while (battery > 0)
            {
                Console.Clear();
                Console.SetCursorPosition(posX, posY);
                Console.Write(drone);
                Console.SetCursorPosition(posX, posY-1);
                Console.Write(battery + "%");
                posX += 1;
                battery -= 2;
                Thread.Sleep(100);
            }
            Console.Clear();
            Console.SetCursorPosition(posX, posY);
            Console.Write("____");
            Console.SetCursorPosition(posX, posY - 1);
            Console.Write("we dead aight");
            Console.ReadKey();
        }
    }
}
