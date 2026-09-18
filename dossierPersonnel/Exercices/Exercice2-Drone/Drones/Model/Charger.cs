using Drones.Helpers;
using Drones.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Model
{
    public class Charger
    {
        private float _x ;                                 // Position en X depuis la gauche de l'espace aérien
        private float _y ;                                 // Position en Y depuis le haut de l'espace aérien

        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        public Charger(float x, float y)
        {
            _x = x;
            _y = y;
        }

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawEllipse(droneBrush, _x, _y, 20,20);
        }
    }
}
