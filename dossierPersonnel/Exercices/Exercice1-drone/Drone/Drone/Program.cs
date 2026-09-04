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

            Drone[] droneArray = new Drone[] {
                new Drone(0, 2, 20),
                new Drone(0, 4, 30),
                new Drone(0, 6, 50),
                new Drone(0, 8, 60),
                new Drone(0, 10, 70),
                new Drone(0, 12, 80)
            };

            while (isOneAlive(droneArray))
            {
                Console.Clear();
                foreach (Drone drone in droneArray)
                {
                    drone.changeState();
                    drone.drawDrone();
                }
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }

        static bool isOneAlive(Drone[] droneArray)
        {
            foreach (Drone drone in droneArray)
            {
                if(drone.Battery > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
