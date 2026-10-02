using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public class Client
    {
        private int _x;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y;                                 // Position en Y depuis le haut de l'espace aérien
        private string _nom;

        public const int dimension = 10;
        public const int minDistanceBetweenClients = 50;

        private SolidBrush _clientBrush = new SolidBrush(Color.Green);

        public int X { get => _x;}
        public int Y { get => _y;}

        public Client(int x, int y, string nom)
        {
            _x = x;
            _y = y;
            _nom = nom;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.FillRectangle(_clientBrush, _x - (dimension / 2), _y - (dimension / 2), dimension, dimension);
        }
    }
}
