using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Courage_of_a_Warrior
{
    abstract class SolidObject
    {
        private readonly List<Point> points = new List<Point>();
        protected readonly int startX;
        protected readonly int startY;
        protected readonly int width;
        protected readonly int height;

        public List<Point> Points
        {
            get { return points; }
        }

        public SolidObject(int _startX, int _startY, int _width, int _height)
        {
            startX = _startX;
            startY = _startY;
            width = _width;
            height = _height;
        }

        public abstract void ConstructRectangleObject();
    }
}
