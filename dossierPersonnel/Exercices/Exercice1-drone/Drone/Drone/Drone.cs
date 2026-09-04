using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Drone
{
    public class Drone
    {
        private int _posX;
        private int _posY;
        private int _battery;

        public Drone(int posX, int posY, int battery)
        {
            this._posX = posX;
            this._posY = posY;
            this._battery = battery;
        }

        public int Battery { get => this._battery; }

        public void drawDrone()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(this._posX, this._posY);
            if (this._battery > 0)
                Console.Write("x-0-x");
            else
                Console.Write("_____");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(this._posX, this._posY - 1);

            if (this._battery > 0)
                Console.Write(this._battery + "%");
            else
                Console.Write("Drone appears to be suffering from lack of electrical input");
        }

        public void changeState()
        {
            if (this._battery <= 0) return;


            this._posX += 1;
            this._battery -= 2;
        }
    }
}
