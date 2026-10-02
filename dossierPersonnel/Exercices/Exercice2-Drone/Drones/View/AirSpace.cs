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

            _pizzeriaChain = PizzeriaGenerator(5);
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

            // draw drones
            foreach (Drone drone in fleet)
            {
                drone.Render(airspace);
            }

            foreach (Client client in _allClients)
            {
                client.Render(airspace);
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

        private List<Pizzeria> PizzeriaGenerator(int numberOfPizzerias)
        {
            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();
            for(int i = 0; i < numberOfPizzerias; i++)
            {
                pizzeriaChain.Add(new Pizzeria(randomValuesHelper.Alea.Next(0, Config.AIRSPACE_WIDTH), randomValuesHelper.Alea.Next(0,Config.AIRSPACE_HEIGHT), $"Pizzeria number {i}"));
            }

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