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

            List<Drone> drones = new List<Drone> {
                new Drone("jean",0, 2, 20),
                new Drone("jeanette",0, 6, 30),
                new Drone("alex",0, 12, 10)
            };

            int count = 0;
            while (isOneAlive(drones))
            {
                Console.Clear();
                foreach (Drone drone in drones)
                {
                    drone.changeState();
                    drone.drawDrone();
                }
                Thread.Sleep(100);
            }
            Console.ReadKey();
        }

        static bool isOneAlive(List<Drone> drones)
        {
            foreach (Drone drone in drones)
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
