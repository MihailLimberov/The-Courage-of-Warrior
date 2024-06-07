using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Courage_of_a_Warrior
{
    class Hero : Person
    {
        private int xPosToMoveField = 0;
        private int yPosToMoveField = 0;

        private int hitPoints;// it would be better to be in the abstract Person class
        public int XPosToMoveField
        {
            get { return xPosToMoveField; }
            set { xPosToMoveField = value; }
        }

        public int YPosToMoveField
        {
            get { return yPosToMoveField; }
            set { yPosToMoveField = value; }
        }

        public int HitPoints
        {
            get { return hitPoints; }
            set { hitPoints = value; }
        }

        public Hero(int x_Pos, int y_Pos, int width, int height,int _hitPoints) : base(x_Pos, y_Pos, width, height)
        {
            hitPoints = _hitPoints;
        }

    }
}
