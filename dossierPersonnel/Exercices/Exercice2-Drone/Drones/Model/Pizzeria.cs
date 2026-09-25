using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public class Pizzeria
    {
        private int _x;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y;                                 // Position en Y depuis le haut de l'espace aérien
        private string _nom;

        private SolidBrush _pizzeriaBrush = new SolidBrush(Color.Gray);
        public Pizzeria(int x, int y, string nom)
        {
            _x = x;
            _y = y;
            _nom = nom;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.FillRectangle(_pizzeriaBrush, _x, _y, 50, 50);
        }

    }
}
