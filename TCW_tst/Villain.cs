using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Courage_of_a_Warrior
{
    class Villain : Person
    {
        private int hitpoints;

        public int HitPoints
        {
            get { return hitpoints; }
            set { hitpoints = value; }
        }
        public Villain(int x_Pos, int y_Pos, int width, int height,int _hitpoints) : base(x_Pos, y_Pos, width, height)
        {
            hitpoints = _hitpoints;
        }
    }
}
