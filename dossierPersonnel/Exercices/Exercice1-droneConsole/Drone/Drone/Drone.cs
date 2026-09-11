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
        private string _name;

        public Drone(string name,int posX, int posY, int battery)
        {
            this._name = name;
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
            {
                Console.Write("x-0-x");
                Console.SetCursorPosition(this._posX, this._posY + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(this._name);
            }
            else
                Console.Write("_____");

            
            Console.SetCursorPosition(this._posX, this._posY - 1);
            Console.ForegroundColor = ConsoleColor.White;

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