using Drones.Helpers;
using Drones.Model;

namespace Drones
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Création de la flotte de drones
            List<Drone> fleet= new List<Drone>();
            fleet.Add(new Drone("Joe"));
            fleet.Add(new Drone("Joe"));
            fleet.Add(new Drone("Joe"));
            Charger charger = new Charger(Config.AIRSPACE_WIDTH / 2, Config.AIRSPACE_HEIGHT /2);

            // Démarrage
            Application.Run(new AirSpace(fleet, charger));
        }
    }
}