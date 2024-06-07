using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Courage_of_a_Warrior
{
    class Tree : SolidObject
    {
        private readonly int applesCount;
        private bool isAppleEaten = false;
        private int pickApplePos_X;
        private int pickApplePos_Y;
        private bool arePointsAdded = false;

        public int ApplesCount
        {
            get { return applesCount; }
        }
        public bool IsAppleEaten
        {
            get { return isAppleEaten; }
            set { isAppleEaten = value; }
        }
        public int PickApplePos_X
        {
            get { return pickApplePos_X; }
            set { pickApplePos_X = value; }
        }

        public int PickApplePos_Y
        {
            get { return pickApplePos_Y; }
            set { pickApplePos_Y = value; }
        }
        public bool ArePointsAdded
        {
            get { return arePointsAdded; }
            set { arePointsAdded = value; }
        }

        public Tree(int startX, int startY, int width, int height, int _applesCount) : base(startX, startY, width, height)
        {
            applesCount = _applesCount;

            //to pick apple ==> go infront of the tree
            pickApplePos_X = startX + width / 2;
            pickApplePos_Y = startY + height + 2;
        }

        public override void ConstructRectangleObject()
        {
            int oddWidth = (width % 2 == 0) ? width + 1 : width; // to make the leaves of the tree symetrical to its stem
            // Draw leaves of tree
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < oddWidth; x++)
                {
                    Points.Add(new Point(x + startX, y + startY));
                }
            }
        }

        public List<Point> AddApplePoints()
        {
            List<Point> applePoints = new List<Point>();
            int oddWidth = (width % 2 == 0) ? width + 1 : width;

            applePoints.Add(new Point(startX, startY));
            applePoints.Add(new Point(startX + oddWidth - 1, startY));
            applePoints.Add(new Point(startX + oddWidth / 2, startY + height - 1));

            return applePoints;
        }

        public void ConstructStem()
        {
            //Draw stem of tree
            Points.Add(new Point(startX + width / 2, startY + height));
            Points.Add(new Point(startX + width / 2, startY + height + 1));
        }

       /* public void Tst(SolidObject s)
        {
           
        }*/
    }
}
