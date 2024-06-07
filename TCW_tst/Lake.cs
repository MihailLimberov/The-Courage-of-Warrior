using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Courage_of_a_Warrior
{
    class Lake : SolidObject
    {
        private bool arePointsAdded = false;
        public bool ArePointsAdded
        {
            get { return arePointsAdded; }
            set { arePointsAdded = value; }
        }

        public Lake(int startX, int startY, int width, int height) : base(startX, startY, width, height)
        {

        }

        public override void ConstructRectangleObject()
        {

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if ((x == 0 && y == 0) || (x == 0 && y == height - 1) ||
                        (x == width - 1 && y == 0) || (x == width - 1 && y == height - 1) ||
                        (x == 0 && y == 1) || (x == 1 && y == 0) || (x == 0 && y == height - 2) || (x == 1 && y == height - 1) ||
                        (x == width - 2 && y == 0) || (x == width - 1 && y == 1) || (x == width - 2 && y == height - 1) || (x == width - 1 && y == height - 2))
                    {
                        continue;
                    }
                    Points.Add(new Point(x + startX, y + startY));
                }
            }

        }
    }
}
