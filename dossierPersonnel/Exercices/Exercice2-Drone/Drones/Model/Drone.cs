using Drones.Helpers;
using Drones.Model;
using Drones.Properties;

namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public partial class Drone
    {
        private double _charge;                            // La charge actuelle de la batterie
        private string name;                           // Un nom
        private double _x;                                 // Position en X depuis la gauche de l'espace aérien
        private double _y;                                 // Position en Y depuis le haut de l'espace aérien
        private double _OriginX;
        private double _OriginY;
        private double _ObjX;
        private double _ObjY;
        private double _completionIndex = 1;
        private double _distance;
        private State _state = State.ROAMING;

        public enum State { CRASH, LOW_BATTERY, LOADING, ROAMING };

        // Constructeur
        public Drone(string name)
        {
            this._x = Config.AIRSPACE_WIDTH / 2;
            this._y = Config.AIRSPACE_HEIGHT / 2;
            _OriginX = _x;
            _OriginY = _y;
            this.name = name;
            _charge = randomValuesHelper.Alea.Next(Config.MAX_LOAD); // La charge initiale de la batterie est choisie aléatoirement
        }

        #region ================ Modelisation du drone et de son comportement ================

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval,Charger charger)
        {
            if (_charge <= 0)
            {
                _state = State.CRASH;
                return;                     // S'il n'a plus de charge, il ne peut plus bouger
            }
            if (_charge < Config.LOW_BATTERY_THRESHOLD)
            {
                _state = State.LOW_BATTERY; //Le drone se met en batterie basse quand il n'as pas assez d'energie
                
                if (charger.X - _x < Config.CHARGING_DISTANCE_THRESHOLD && charger.X - _x > -Config.CHARGING_DISTANCE_THRESHOLD)
                    _state = State.LOADING; //il se recharge quand il est assez proche de la station de recharge
            }
            else if (_charge >= 1000)
            {
                _state = State.ROAMING; //Il se remet en route quand il est chargé
            }
                
            


            if (_completionIndex >= 1) //se déclanche quand le drone est arrivé à destination
            {
                _completionIndex = 0;
                _OriginX = _x;
                _OriginY = _y;



                if(_state == State.ROAMING)
                {
                    _ObjX = randomValuesHelper.Alea.Next(200, Config.AIRSPACE_WIDTH - 200); //trouve une nouvelle destination
                    _ObjY = randomValuesHelper.Alea.Next(200, Config.AIRSPACE_HEIGHT - 200);
                }
                if(_state == State.LOW_BATTERY)
                {
                    _ObjX = charger.X;
                    _ObjY = charger.Y;
                }

                
                _distance = mathHelper.distance(_OriginX, _OriginY, _ObjX, _ObjY);
            }
            else if(_state != State.LOADING)
            {
                _completionIndex += Config.SPEED/_distance;
                _x = _OriginX + ((_ObjX - _OriginX) * _completionIndex);
                _y = _OriginY + ((_ObjY - _OriginY) * _completionIndex);
            }

            if (_state != State.LOADING)
                _charge--;                                  // Il a dépensé de l'énergie
            else
                _charge += 10;
        }

        #endregion

        #region  ================ Rendu graphique  ================

        private const int SIZE = 50;
        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_charge > 0 ? Resources.drone : Resources.boom, Convert.ToSingle(_x- Drone.SIZE /2), Convert.ToSingle(_y - Drone.SIZE / 2), Drone.SIZE, Drone.SIZE);
            drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, Convert.ToSingle(_x + 5), Convert.ToSingle( _y - 25));
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name} ({((int)((double)_charge / 1000 * 100)).ToString()}%) : {_state}";
        }
        #endregion

    }
}
