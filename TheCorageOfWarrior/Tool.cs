using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Courage_of_a_Warrior
{
    class Tool
    {
        private int x_Pos;
        private int y_Pos;

        private bool isSwordPicked = false;
        private bool isExcaliburPicked = false;
        private bool hasScabbard = false;
        private bool hasKey = false;

        public int X_Pos
        {
            get { return x_Pos; }
            set { x_Pos = value; }
        }

        public int Y_Pos
        {
            get { return y_Pos; }
            set { y_Pos = value; }
        }

        public bool IsSwordPicked
        {
            get { return isSwordPicked; }
            set { isSwordPicked = value; }
        }

        public bool IsExcaliburPicked
        {
            get { return isExcaliburPicked; }
            set { isExcaliburPicked = value; }
        }
        public bool HasScabbard
        {
            get { return hasScabbard; }
            set { hasScabbard = value; }
        }

        public bool HasKey
        {
            get { return hasKey; }
            set { hasKey = value; }
        }

        public Tool(int _xPos, int _yPos)
        {
            x_Pos = _xPos;
            y_Pos = _yPos;
        }
    }
}
