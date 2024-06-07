using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Courage_of_a_Warrior
{
    abstract class Person
    {

        private int x_Pos;
        private int y_Pos;

        private readonly int width;
        private readonly int height;
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

        public int Width { get { return width; } }
        public int Height { get { return height; } }
       
        
        public Person(int _xPos, int _yPos,int _width, int _height)
        {
            x_Pos = _xPos;
            y_Pos = _yPos;
            width = _width;
            height = _height;
        }

    }
}
