using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Courage_of_a_Warrior
{
    class Settings
    {
        // the width and height of each rectangle we divide the gameField (pictureBox)
        public static int Height { get; private set; }
        public static int Width { get; private set; }

        private static string direction;
        private static bool goUp, goDown, goLeft, goRight;
        private static bool isPlayButtonPressed;
        private static bool attack;
        private static bool isItemDropped;

        private static bool isHeroInHouse;
        private static bool caveDoorUnlocked;
        private static bool isHeroLeavingHouse;
        public static bool IsHeroLeavingHouse
        {
            get { return isHeroLeavingHouse; }
            set { isHeroLeavingHouse = value; }
        }

        public static string Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public static bool GoUp
        {
            get { return goUp; }
            set { goUp = value; }
        }
        public static bool GoDown
        {
            get { return goDown; }
            set { goDown = value; }
        }
        public static bool GoLeft
        {
            get { return goLeft; }
            set { goLeft = value; }
        }
        public static bool GoRight
        {
            get { return goRight; }
            set { goRight = value; }
        }
        public static bool IsPlayButtonPressed
        {
            get { return isPlayButtonPressed; }
            set { isPlayButtonPressed = value; }
        }
        public static bool Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public static bool IsHeroInHouse
        {
            get { return isHeroInHouse; }
            set { isHeroInHouse = value; }
        }

        public static bool IsItemDropped
        {
            get { return isItemDropped; }
            set { isItemDropped = value; }
        }

        public static bool CaveDoorUnlocked
        {
            get { return caveDoorUnlocked; }
            set { caveDoorUnlocked = value; }
        }
        public Settings()
        {
            Height = 30;
            Width = 30;

            direction = null;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;

            isPlayButtonPressed = false;
            IsHeroInHouse = false;
            //IsHeroLeavingHouse = false; not neccessary - with each check in IsInHouse() IsHeroLeavingHouse = false;
            attack = false;
            isItemDropped = false;
            caveDoorUnlocked = false;
        }
    }
}
