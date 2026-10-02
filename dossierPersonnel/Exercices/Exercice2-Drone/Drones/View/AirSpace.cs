using Drones.Helpers;
using Drones.Model;
using System.Runtime.InteropServices;

namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {

        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private List<Drone> fleet;
        private Charger charger;
        static private List<Pizzeria> _pizzeriaChain = new List<Pizzeria>();
        static private List<Client> _allClients = new List<Client>();

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        private const int numberOfPizzerias = 5;
        private const int numberOfClients = 20;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(List<Drone> fleet, Charger charger)
        {
            InitializeComponent();
            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.fleet = fleet;
            this.charger = charger;

            while(_pizzeriaChain.Count < numberOfPizzerias)
            {
                try
                {
                    _pizzeriaChain = RegisterPizzeria(_pizzeriaChain, new Pizzeria(randomValuesHelper.Alea.Next(Pizzeria.dimension, Config.AIRSPACE_WIDTH - Pizzeria.dimension), randomValuesHelper.Alea.Next(Pizzeria.dimension, Config.AIRSPACE_HEIGHT- Pizzeria.dimension), $"Pizzeria number {_pizzeriaChain.Count + 1}"));
                }
                catch{ }
            }

            while (_allClients.Count < numberOfClients)
            {
                try
                {
                    _allClients = RegisterClient(_pizzeriaChain,_allClients, new Client(randomValuesHelper.Alea.Next(Client.dimension, Config.AIRSPACE_WIDTH- Client.dimension), randomValuesHelper.Alea.Next(Client.dimension, Config.AIRSPACE_HEIGHT - Client.dimension), $"Client number {_allClients.Count + 1}"));
                }
                catch { }
            }
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);

            foreach (Pizzeria pizzeria in _pizzeriaChain)
            {
                pizzeria.Render(airspace);
            }

            foreach (Client client in _allClients)
            {
                client.Render(airspace);
            }

            // draw drones
            foreach (Drone drone in fleet)
            {
                drone.Render(airspace);
            }





            charger.Render(airspace);

            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            foreach (Drone drone in fleet)
            {
                drone.Update(interval, charger);
            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        static private List<Pizzeria> RegisterPizzeria(List<Pizzeria> pizzeriaChain, Pizzeria newPizzeria)
        {
            foreach (Pizzeria pizzeria in pizzeriaChain)
            {
                if(Math.Abs(newPizzeria.X - pizzeria.X) < Pizzeria.dimension && Math.Abs(newPizzeria.Y - pizzeria.Y) < Pizzeria.dimension)
                {
                    throw new Exception("Collision !");
                }
            }
            
            pizzeriaChain.Add(newPizzeria);
            return pizzeriaChain;
        }

        static private List<Client> RegisterClient(List<Pizzeria> pizzeriaChain, List<Client> allClients, Client newClient)
        {
            foreach (Client client in allClients)
            {
                if (Math.Abs(newClient.X - client.X) < Client.dimension + Client.minDistanceBetweenClients && Math.Abs(newClient.Y - client.Y) < Client.dimension + Client.minDistanceBetweenClients)
                {
                    throw new Exception("Collision !");
                }
            }
            
            foreach (Pizzeria pizzeria in pizzeriaChain)
            {
                if (Math.Abs(newClient.X - pizzeria.X) < Pizzeria.dimension && Math.Abs(newClient.Y - pizzeria.Y) < Pizzeria.dimension)
                {
                    throw new Exception("Collision !");
                }
            }

            allClients.Add(newClient);
            return allClients;
        }

    }
}