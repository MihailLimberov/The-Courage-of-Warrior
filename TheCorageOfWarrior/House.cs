using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Courage_of_a_Warrior
{
    class House : SolidObject
    {
        private bool isDoorAdded = false;
        private bool areInsidePointsAdded = false;
        private bool areOutsidePointsAdded = false;
        private bool isWallAdded = false;

        public bool IsDoorAdded
        {
            get { return isDoorAdded; }
            set { isDoorAdded = value; }
        }

        public bool ArePointsAdded
        {
            get { return areInsidePointsAdded;  }
            set { areInsidePointsAdded = value; }
        }
        public bool AreInsidePointsAdded
        {
            get { return areOutsidePointsAdded; }
            set { areOutsidePointsAdded = value; }
        }
        public bool IsWallAdded
        {
            get { return isWallAdded; }
            set { isWallAdded = value; }
        }
        public House(int startX, int startY, int width, int height) : base(startX, startY, width, height)
        {

        }

        public override void ConstructRectangleObject()
        {
            // Draw rectangle from upper left position with coordinates (startX, startY) with given width and height
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Points.Add(new Point(x + startX, y + startY));
                }
            }
        }

        public void ConstructRoof() // Draw roof of house
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < width + 2; x++)
                {
                    if (y == 0 && (x == 0 || x == width + 1))
                    {
                        continue;
                    }

                    Points.Add(new Point(x + startX - 1, y + startY - 2));
                }
            }
        }

        public void ConstructDoor()
        {
            Points.Add(new Point(startX + 1, startY + height - 1));
        }
        public void ConstructFakeDoor() // 2 blocks below the real door
        {
            Points.Add(new Point(startX + 1, startY + height + 1));
        }
        public void ConstructWalls()
        {
            for (int y = -2; y <= height + 1; y++) // -2 to "fill" the space for the unused roof
            {
                for (int x = -1; x < 3 * width; x++) // 3 * width to make more space inside the house; -1 to construct 1 column of blocks of the left of the door
                {
                    if (y == -2 || y == height + 1 || x == -1 || x == 3 * width - 1)
                    {
                        Points.Add(new Point(x + startX, y + startY));
                    }
                }
            }
        }

        public void ConstructFloor()
        {
            // to fill the floor space between the walls
            for (int y = -1; y <= height; y++)
            {
                for (int x = 0; x < 3 * width - 1; x++)
                {
                    Points.Add(new Point(x + startX, y + startY));
                }
            }
        }

        public void ConstuctWallPartition()
        {
            for (int y = -1; y <= height; y++)
            {
                Points.Add(new Point(7 + startX, y + startY));
            }
        }

    }
}
