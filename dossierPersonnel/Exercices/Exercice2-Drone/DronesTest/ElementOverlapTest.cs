using Drones;
using Drones.Model;
namespace DronesTest
{
    [TestClass]
    public sealed class ElementOverlapTest
    {
        [TestMethod]
        public void pizzeriaTooCloseToPizzeria()
        {
            //Arrange
            Charger charger = new Charger(0, 0);
            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();
            pizzeriaChain.Add(new Pizzeria(50, 50, "David"));
            List<Client> allClients = new List<Client>();

            Pizzeria pizzeria = new Pizzeria(49, 49, "joe");

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                pizzeriaChain = AirSpace.RegisterPizzeria(allClients,charger, pizzeriaChain, pizzeria)
            );
        }

        [TestMethod]
        public void clientTooCloseToClient()
        {
            //Arrange
            Charger charger = new Charger(0, 0);
            
            List<Client> allClients = new List<Client>();
            allClients.Add(new Client(50, 50, "David"));

            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();

            Client client = new Client(49, 49, "joe");

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                allClients = AirSpace.RegisterClient(charger,pizzeriaChain, allClients, client)
            );
        }

        [TestMethod]
        public void clientOnPizzeria()
        {
            //Arrange
            Charger charger = new Charger(0, 0);

            List<Client> allClients = new List<Client>();

            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();
            pizzeriaChain.Add(new Pizzeria(49, 49, "David"));

            Client client = new Client(50, 50, "joe");

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                allClients = AirSpace.RegisterClient(charger, pizzeriaChain, allClients, client)
            );
        }

        [TestMethod]
        public void PizzeriaOnClient()
        {
            //Arrange
            Charger charger = new Charger(0, 0);

            List<Client> allClients = new List<Client>();
            allClients.Add(new Client(49, 49, "David"));

            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();


            Pizzeria pizzeria = new Pizzeria(50, 50, "joe");

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                pizzeriaChain = AirSpace.RegisterPizzeria(allClients,charger, pizzeriaChain, pizzeria)
            );
        }

        [TestMethod]
        public void pizzeriaTooCloseToCharger()
        {
            //Arrange
            Charger charger = new Charger(49, 49);
            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();
            pizzeriaChain.Add(new Pizzeria(150, 150, "David"));

            Pizzeria pizzeria = new Pizzeria(49, 49, "joe");

            List<Client> allClients = new List<Client>();

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                pizzeriaChain = AirSpace.RegisterPizzeria(allClients,charger, pizzeriaChain, pizzeria)
            );
        }

        [TestMethod]
        public void clientTooCloseToCharger()
        {
            //Arrange
            Charger charger = new Charger(50, 50);

            List<Client> allClients = new List<Client>();
            allClients.Add(new Client(200, 200, "David"));

            List<Pizzeria> pizzeriaChain = new List<Pizzeria>();
            pizzeriaChain.Add(new Pizzeria(300, 300, "David"));

            Client client = new Client(50, 50, "joe");

            //Act et Assert
            Assert.ThrowsException<Exception>(() =>
                allClients = AirSpace.RegisterClient(charger, pizzeriaChain, allClients, client)
            );
        }
    }
}
