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
                    _pizzeriaChain = RegisterPizzeria(_pizzeriaChain, new Pizzeria(randomValuesHelper.Alea.Next(0, Config.AIRSPACE_WIDTH), randomValuesHelper.Alea.Next(0, Config.AIRSPACE_HEIGHT), $"Pizzeria number {_pizzeriaChain.Count + 1}"));
                }
                catch{}
            }
            _allClients = ClientGenerator(20);
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
                    Console.WriteLine("collision");
                    throw new Exception("Collision !");
                }
            }
            
            pizzeriaChain.Add(newPizzeria);
            return pizzeriaChain;
        }

        private List<Client> ClientGenerator(int numberOfClient)
        {
            List<Client> clientChain = new List<Client>();
            for (int i = 0; i < numberOfClient; i++)
            {
                clientChain.Add(new Client(randomValuesHelper.Alea.Next(0, Config.AIRSPACE_WIDTH), randomValuesHelper.Alea.Next(0, Config.AIRSPACE_HEIGHT), $"Client number {i}"));
            }

            return clientChain;
        }

    }
}