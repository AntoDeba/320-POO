using Drones.Helpers;
using Drones.Properties;

namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public partial class Drone
    {
        private int charge;                            // La charge actuelle de la batterie
        private string name;                           // Un nom
        private int x;                                 // Position en X depuis la gauche de l'espace aérien
        private int y;                                 // Position en Y depuis le haut de l'espace aérien
        private int OriginX;
        private int OriginY;
        private int ObjX;
        private int ObjY;
        private int completionPercentage = 101;

        // Constructeur
        public Drone(string name)
        {
            this.x = Config.AIRSPACE_WIDTH / 2;
            this.y = Config.AIRSPACE_HEIGHT / 2;
            OriginX = x;
            OriginY = y;
            this.name = name;
            charge = randomValuesHelper.Alea.Next(Config.MAX_LOAD); // La charge initiale de la batterie est choisie aléatoirement
        }

        #region ================ Modelisation du drone et de son comportement ================

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            if (charge <= 0) return;                     // S'il n'a plus de charge, il ne peut plus bouger
            if(completionPercentage >= 100)
            {
                completionPercentage = 0;
                ObjX = randomValuesHelper.Alea.Next(200, Config.AIRSPACE_WIDTH-200);
                ObjY = randomValuesHelper.Alea.Next(200, Config.AIRSPACE_HEIGHT-200);
                OriginX = x;
                OriginY = y;
            }
            else
            {
                completionPercentage += 10;
                x = OriginX + ((ObjX - OriginX) * completionPercentage / 100);
                y = OriginY + ((ObjY - OriginY) * completionPercentage / 100);
            }

            charge--;                                  // Il a dépensé de l'énergie
        }

        #endregion

        #region  ================ Rendu graphique  ================

        private const int SIZE = 50;
        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(charge > 0 ? Resources.drone : Resources.boom, x-Drone.SIZE/2, y - Drone.SIZE / 2, Drone.SIZE, Drone.SIZE);
            drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, x + 5, y - 25);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name} ({((int)((double)charge / 1000 * 100)).ToString()}%)";
        }
        #endregion

    }
}
