using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging; // for ScreenShot() method
using System.IO;

namespace The_Courage_of_a_Warrior
{
    public partial class GameForm : Form
    {
        private readonly Hero hero = new Hero(17, 10, 30, 30, 30);

        private readonly Villain villain = new Villain(19, 14, 30, 30, 30);//in starter house
        private readonly Villain villain1 = new Villain(52, 82, 30, 30, 35);//guard scabbard
        private readonly Villain villain5 = new Villain(48, 83, 30, 30, 35);//guard scabbard
        private readonly Villain villain2 = new Villain(84, 101, 30, 30, 35);//guard key
        private readonly Villain villain3 = new Villain(80, 103, 30, 30, 35);//guard key
        private readonly Villain villain4 = new Villain(94, 20, 30, 30, 100);//guard banner
        private readonly List<Villain> villains = new List<Villain>();

        private readonly House house = new House(10, 12, 4, 3);
        private readonly House house1 = new House(50, 30, 5, 3);
        private readonly House house2 = new House(40, 82, 5, 3);
        private readonly House house3 = new House(110, 71, 4, 3);
        private readonly List<House> houses = new List<House>();

        private readonly Tree tree = new Tree(19, 5, 3, 2, 10);
        private readonly Tree tree1 = new Tree(8, 23, 3, 3, 10);
        private readonly Tree tree2 = new Tree(68, 37, 3, 2, 5);
        private readonly Tree tree3 = new Tree(98, 83, 3, 3, 5);
        private readonly Tree tree4 = new Tree(42, 43, 3, 2, 15);
        private readonly Tree tree5 = new Tree(35, 64, 3, 3, 15);
        private readonly Tree tree6 = new Tree(100, 30, 3, 2, 10);
        private readonly Tree tree7 = new Tree(35, 21, 3, 2, 5);
        private readonly List<Tree> trees = new List<Tree>();

        private readonly Lake lake = new Lake(102, 93, 14, 7);

        private readonly House caveHouse1 = new House(62, 100, 10, 7);
        private readonly House caveHouse2 = new House(90, 20, 12, 5);
        private readonly List<House> caveHouses = new List<House>();

        private readonly Tool sword = new Tool(74, 45);
        private readonly Tool excalibur = new Tool(111, 101);
        private readonly Tool scabbard = new Tool(53, 81);
        private readonly Tool key = new Tool(88, 101);
        private readonly Tool banner = new Tool(120, 23);

        private readonly List<Point> currSolidPoints = new List<Point>();
        private readonly List<Point> outsideSolidPoints = new List<Point>();
        private readonly List<Point> inStructureSolidPoints = new List<Point>(); // InStructureSolidPoint - LATER TO BE RENAMED!!!

        private readonly List<Point> doorPoints = new List<Point>(); // contains the points INFRONT of each door

        Random villiansMovement = new Random();

        int gameFieldMaxWidth;
        int gameFieldMaxHeight;

        public GameForm()
        {
            InitializeComponent();

            new Settings();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Settings.GoUp = true;
            }
            if (e.KeyCode == Keys.S)
            {
                Settings.GoDown = true;
            }
            if (e.KeyCode == Keys.A)
            {
                Settings.GoLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Settings.GoRight = true;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.F5)
            {
                StartGame();
            }
            if (e.KeyCode == Keys.H)
            {
                buttonHelp_Click(sender, e);
            }
            if (e.KeyCode == Keys.P)
            {
                ScreenShot();
            }
            if (e.KeyCode == Keys.F11)
            {
                FullScreen();
            }

            if (e.KeyCode == Keys.Q)
            {
                Settings.Attack = true;
            }
            if (e.KeyCode == Keys.R)
            {
                Settings.IsItemDropped = true;
            }

            if (Settings.IsPlayButtonPressed)
            {
                HeroActions();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Settings.GoUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                Settings.GoDown = false;
            }
            if (e.KeyCode == Keys.A)
            {
                Settings.GoLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                Settings.GoRight = false;
            }

            if (e.KeyCode == Keys.Q)
            {
                Settings.Attack = false;
            }
            if (e.KeyCode == Keys.R)
            {
                Settings.IsItemDropped = false;
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            if (!textBoxHelp.Visible)
            {
                textBoxHelp.Visible = true;

                string filePath = @"C:\Users\user\Desktop\TCW_tst\GameButtons.txt";
                if (File.Exists(filePath))
                {
                    string gameButtonsText = File.ReadAllText(filePath);
                    textBoxHelp.Text = gameButtonsText;
                }
                else // in case the file path is not correct 
                {
                    //"\n" does not work
                    // or textBoxHelp.Text = "Error loading with Instructions!"
                    textBoxHelp.Text = "You live in a world where the evil lurks\t" +
                                        "and you need to search for the most powerful\t" +
                                        "weapon known to man.\t\t" +
                                        "Are you brave enough to find it ?\t\t" +
                                        "Careful, the danger is everywhere!" +
                                        "______________________________________ " +
                                        "F5 - Start Game\t\t\t  " +
                                        "W, A, S, D - Movement\t\t\t" +
                                        "Q - Attack\t\t\t\t\t" +
                                        "R - Drop Item\t\t\t\t" +
                                        "Eat Apple - Go under a tree\t\t\t" +
                                        "H - Open / Close Help\t\t\t" +
                                        "P - Take Picture\t\t\t" +
                                        "F11 - Fullscreen\t\t\t" +
                                        "Escape - Exit";
                }

            }
            else textBoxHelp.Visible = false;
        }
        private void FullScreen()
        {
            if (this.FormBorderStyle == FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                panel.Height = 660;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                panel.Height = 630;
            }

        }
        private void ScreenShot()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "gameFieldPicture";
            dialog.DefaultExt = "png";
            dialog.Filter = "PNG Image File | *.png";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(panel.Width, panel.Height);
                panel.DrawToBitmap(bmp, new Rectangle(0, 0, panel.Width, panel.Height));
                bmp.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        private void StartGame()
        {
            Settings.IsPlayButtonPressed = true;

            gameFieldMaxWidth = gameField.Width / Settings.Width - 1;
            gameFieldMaxHeight = gameField.Height / Settings.Height - 1;

            //All houses, trees, caveHouses, villains go in their List
            houses.Add(house);
            houses.Add(house1);
            houses.Add(house2);
            houses.Add(house3);

            trees.Add(tree);
            trees.Add(tree1);
            trees.Add(tree2);
            trees.Add(tree3);
            trees.Add(tree4);
            trees.Add(tree5);
            trees.Add(tree6);
            trees.Add(tree7);

            caveHouses.Add(caveHouse1);
            caveHouses.Add(caveHouse2);

            villains.Add(villain);
            villains.Add(villain1);
            villains.Add(villain2);
            villains.Add(villain3);
            villains.Add(villain4);
            villains.Add(villain5);
        }

        private void HeroActions()
        {
            if (IsInHouse(doorPoints))
            {
                currSolidPoints.Clear();
                foreach (var point in inStructureSolidPoints)
                {
                    currSolidPoints.Add(point);
                }

                //List of villains
                // villains are always IN Structures
                foreach (var villain in villains)
                {
                    AtkAction(villain);
                    MoveVillian(villain);

                }

                HeroWon(banner);
            }
            else
            {
                currSolidPoints.Clear();
                foreach (var point in outsideSolidPoints)
                {
                    currSolidPoints.Add(point);
                }

                //For all trees
                foreach (var tree in trees)
                {
                    HeroEat(tree);
                }
            }


            if (Settings.GoUp && CanMoveUp(currSolidPoints))
            {
                Settings.Direction = "up";
            }
            if (Settings.GoDown && CanMoveDown(currSolidPoints))
            {
                Settings.Direction = "down";
            }
            if (Settings.GoLeft && CanMoveLeft(currSolidPoints))
            {
                Settings.Direction = "left";
            }
            if (Settings.GoRight && CanMoveRight(currSolidPoints))
            {
                Settings.Direction = "right";
            }

            switch (Settings.Direction)
            {
                case "up":
                    hero.Y_Pos--;
                    hero.YPosToMoveField--;
                    break;
                case "down":
                    hero.Y_Pos++;
                    hero.YPosToMoveField++;
                    break;
                case "left":
                    hero.X_Pos--;
                    hero.XPosToMoveField--;
                    break;
                case "right":
                    hero.X_Pos++;
                    hero.XPosToMoveField++;
                    break;
            }

            Settings.Direction = null; // reset the already given direction of hero

            //to center the position of hero while walking
            // after each move of hero ==> the gameField "moves" in opposite direction; one block is 30x30 (Settings.Width = Settings.Height = 30)
            if (hero.XPosToMoveField == 1)
            {
                gameField.Left -= Settings.Width;
                hero.XPosToMoveField = 0;
            }
            if (hero.XPosToMoveField == -1)
            {
                gameField.Left += Settings.Width;
                hero.XPosToMoveField = 0;
            }
            if (hero.YPosToMoveField == 1)
            {
                gameField.Top -= Settings.Height;
                hero.YPosToMoveField = 0;
            }
            if (hero.YPosToMoveField == -1)
            {
                gameField.Top += Settings.Height;
                hero.YPosToMoveField = 0;
            }

            gameField.Invalidate();
        }

        private bool CanMoveUp(List<Point> solidPoints)
        {
            if (hero.Y_Pos == 1)// 1 ==> bacause the hero is larger than 1 square; if not a problem it can be 0
            {
                return false;
            }

            if (solidPoints.Contains(new Point(hero.X_Pos, hero.Y_Pos - 1)))
            {

            }
            foreach (var point in solidPoints)
            {
                if (hero.Y_Pos - 1 == point.Y && hero.X_Pos == point.X)
                {
                    return false;
                }
            }

            return true;
        }
        private bool CanMoveDown(List<Point> solidPoints)
        {
            if (hero.Y_Pos == gameFieldMaxHeight)
            {
                return false;
            }

            foreach (var point in solidPoints)
            {
                if (hero.Y_Pos + 1 == point.Y && hero.X_Pos == point.X)
                {
                    return false;
                }
            }

            return true;
        }
        private bool CanMoveLeft(List<Point> solidPoints)
        {
            if (hero.X_Pos == 0)
            {
                return false;
            }

            foreach (var point in solidPoints)
            {
                if (hero.X_Pos - 1 == point.X && hero.Y_Pos == point.Y)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanMoveRight(List<Point> solidPoints)
        {
            if (hero.X_Pos == gameFieldMaxWidth)
            {
                return false;
            }

            foreach (var point in solidPoints)
            {
                if (hero.X_Pos + 1 == point.X && hero.Y_Pos == point.Y)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsInHouse(List<Point> doorPoints)
        {
            Point heroPos = new Point(hero.X_Pos, hero.Y_Pos);
            Settings.IsHeroLeavingHouse = false;

            foreach (var doorPoint in doorPoints)
            {
                if (heroPos == doorPoint && Settings.IsHeroInHouse == false)
                {
                    gameField.BackColor = Color.FromArgb(10, 10, 10); // custom black colour in the background
                    Settings.IsHeroInHouse = true;
                }
                else if (heroPos == doorPoint && Settings.IsHeroInHouse == true)
                {
                    Settings.IsHeroLeavingHouse = true;
                    Settings.IsHeroInHouse = false;
                }
            }

            return Settings.IsHeroInHouse;
        }
        private void AtkAction(Villain villain)
        {
            // Hero Attacks
            if (hero.X_Pos - 1 <= villain.X_Pos && hero.X_Pos + 1 >= villain.X_Pos &&
                hero.Y_Pos - 1 <= villain.Y_Pos && hero.Y_Pos + 1 >= villain.Y_Pos && Settings.Attack)
            {
                int heroDamage;
                if (sword.IsSwordPicked) heroDamage = 5;
                else if (excalibur.IsExcaliburPicked) heroDamage = 15;
                else heroDamage = 2;

                villain.HitPoints -= heroDamage;
                if (villain.HitPoints <= 0)
                {
                    VillainDie(villain);
                }
            }

            //Villain Attacks
            if (villain.X_Pos - 1 <= hero.X_Pos && villain.X_Pos + 1 >= hero.X_Pos &&
               villain.Y_Pos - 1 <= hero.Y_Pos && villain.Y_Pos + 1 >= hero.Y_Pos)
            {
                int villainDamage;
                if (scabbard.HasScabbard) villainDamage = 0;
                else villainDamage = 4;

                hero.HitPoints -= villainDamage;
                if (hero.HitPoints <= 0)
                {
                    HeroDie();
                }
            }
        }
        private void MoveVillian(Villain villain)
        {
            if (villain.X_Pos - 6 <= hero.X_Pos && villain.X_Pos + 6 >= hero.X_Pos &&
                villain.Y_Pos - 6 <= hero.Y_Pos && villain.Y_Pos + 6 >= hero.Y_Pos)
            {
                int villainRandMove = villiansMovement.Next(1, 3);

                if (villainRandMove == 1 && villain.Y_Pos > hero.Y_Pos)
                {
                    VillainMoveUp(villain);
                }
                if (villainRandMove == 1 && villain.Y_Pos < hero.Y_Pos)
                {
                    VillainMoveDown(villain);
                }
                if (villainRandMove == 2 && villain.X_Pos < hero.X_Pos)
                {
                    VillainMoveRight(villain);
                }
                if (villainRandMove == 2 && villain.X_Pos > hero.X_Pos)
                {
                    VillainMoveLeft(villain);
                }
            }
        }

        private void VillainMoveUp(Villain villain)
        {
            bool canMoveUp = true;

            if (villain.Y_Pos - 1 == hero.Y_Pos && villain.X_Pos == hero.X_Pos)//if Hero is above villain ==> it cannot move
            {
                canMoveUp = false;
            }
            if (inStructureSolidPoints.Contains(new Point(villain.X_Pos, villain.Y_Pos - 1)))//if there is solid point above villain ==> it cannot move
            {
                canMoveUp = false;
            }

            if (canMoveUp)
            {
                villain.Y_Pos--;
            }
        }
        private void VillainMoveDown(Villain villain)
        {
            bool canMoveDown = true;

            if (villain.Y_Pos + 1 == hero.Y_Pos && villain.X_Pos == hero.X_Pos)
            {
                canMoveDown = false;
            }
            if (inStructureSolidPoints.Contains(new Point(villain.X_Pos, villain.Y_Pos + 1)))
            {
                canMoveDown = false;
            }

            if (canMoveDown)
            {
                villain.Y_Pos++;
            }

        }

        private void VillainMoveRight(Villain villain)
        {
            bool canMoveRight = true;

            if (villain.X_Pos + 1 == hero.X_Pos && villain.Y_Pos == hero.Y_Pos)
            {
                canMoveRight = false;
            }
            if (inStructureSolidPoints.Contains(new Point(villain.X_Pos + 1, villain.Y_Pos)))
            {
                canMoveRight = false;
            }

            if (canMoveRight)
            {
                villain.X_Pos++;
            }
        }

        private void VillainMoveLeft(Villain villain)
        {
            bool canMoveLeft = true;

            if (villain.X_Pos - 1 == hero.X_Pos && villain.Y_Pos == hero.Y_Pos)
            {
                canMoveLeft = false;
            }
            if (inStructureSolidPoints.Contains(new Point(villain.X_Pos - 1, villain.Y_Pos)))
            {
                canMoveLeft = false;
            }

            if (canMoveLeft)
            {
                villain.X_Pos--;
            }
        }
        private void HeroDie()
        {
            DialogResult result = MessageBox.Show("Take a screenshot?", "You Died!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ScreenShot();
            }

            Application.Exit();
        }

        private void HeroWon(Tool banner)
        {
            if (hero.X_Pos == banner.X_Pos && hero.Y_Pos == banner.Y_Pos)
            {
                MessageBox.Show("You found an old banner. It's origin is unknown. The insignia on it is written in an " +
                    "ancient language you cannot understand.", "Old Banner");
                MessageBox.Show("In the end the banner you found is not even a weapon. But even so, in search for " +
                    "the greatest power in the world you realized that it is not in the warrior's weapon but " +
                    "in the warrior's courage to fight.");
                DialogResult result = MessageBox.Show("Take a screenshot?", "You Won!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ScreenShot();
                }

                Application.Exit();
            }
        }
        private void HeroEat(Tree tree)
        {
            //In Hero Actoin as Another method
            if (hero.X_Pos == tree.PickApplePos_X && hero.Y_Pos == tree.PickApplePos_Y)
            {
                hero.HitPoints += tree.ApplesCount;
                tree.PickApplePos_X = -10;
                tree.PickApplePos_Y = -10;
                tree.IsAppleEaten = true;
            }
        }
        private void VillainDie(Villain villain)
        {
            // villain "dies" by teleporting to a location outside the GameField
            villain.X_Pos = -15;
            villain.Y_Pos = -15;
        }
        private void UpdateGameField(object sender, PaintEventArgs e)
        {
            Graphics image = e.Graphics;

            // if hero is outside
            if (!IsInHouse(doorPoints) && !Settings.IsHeroLeavingHouse)
            {
                gameField.BackColor = Color.FromArgb(0, 192, 0); // green colour for the outside world

                //Draw all houses
                foreach (var house in houses) DrawHouse(image, house);

                //Draw All trees
                foreach (var tree in trees) DrawTree(image, tree);

                //Draw All Outside Cave houses
                foreach (var caveHouse in caveHouses) DrawCaveHouse(image, caveHouse);

                DrawLake(image, lake);
                DrawSword(image, sword, excalibur);
                DrawExcalibur(image, excalibur, sword);
            }

            //if hero is in house
            if (IsInHouse(doorPoints))
            {
                //Draw Inside All houses
                foreach (var house in houses) DrawInsideHouse(image, house);

                //Draw Inside All Cave Houses
                foreach (var caveHouse in caveHouses) DrawInsideCaveHouse(image, caveHouse);

                DrawWallPartition(image, caveHouse2);

                //Draw All Villains
                foreach (var villain in villains) DrawVillain(image, villain);

                DrawScabbard(image, scabbard);
                DrawKey(image, key);
                DrawBanner(image, banner);
            }

            DrawHero(image, hero);
        }
        private void DrawBanner(Graphics image, Tool banner)
        {
            SolidBrush handleColour = new SolidBrush(Color.FromArgb(98, 49, 0));
            SolidBrush yellow = new SolidBrush(Color.FromArgb(200, 200, 0));
            SolidBrush yellow1 = new SolidBrush(Color.FromArgb(220, 220, 30));

            image.FillRectangle(handleColour, banner.X_Pos * Settings.Width, banner.Y_Pos * Settings.Height - 24, Settings.Width / 7, Settings.Height * 2);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 18, Settings.Width * 1.5f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height + 2, Settings.Width * 1.5f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 14, Settings.Width * 1.3f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 2, Settings.Width * 1.3f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 12, Settings.Width * 1.1f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 4, Settings.Width * 1.1f, Settings.Height / 7);
            image.FillRectangle(Brushes.WhiteSmoke, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 8, Settings.Width, Settings.Height / 7);

            image.FillRectangle(yellow, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height - 16, Settings.Width * 1.5f, Settings.Height / 12);
            image.FillRectangle(yellow, banner.X_Pos * Settings.Width + 2, banner.Y_Pos * Settings.Height + 2, Settings.Width * 1.5f, Settings.Height / 12);

            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 6, banner.Y_Pos * Settings.Height - 4, Settings.Width / 3, Settings.Height / 20);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 21, banner.Y_Pos * Settings.Height - 6, Settings.Width / 4, Settings.Height / 20);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 6, banner.Y_Pos * Settings.Height - 6, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 8, banner.Y_Pos * Settings.Height - 8, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 10, banner.Y_Pos * Settings.Height - 10, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 12, banner.Y_Pos * Settings.Height - 12, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 26, banner.Y_Pos * Settings.Height - 5, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 28, banner.Y_Pos * Settings.Height - 3, Settings.Width / 12, Settings.Height / 12);
            image.FillRectangle(yellow1, banner.X_Pos * Settings.Width + 23, banner.Y_Pos * Settings.Height - 10, Settings.Width / 20, Settings.Height / 4);

            image.FillRectangle(Brushes.DimGray, banner.X_Pos * Settings.Width - 0.9f, banner.Y_Pos * Settings.Height - 26, Settings.Width / 6.1f, Settings.Height / 11);
            image.FillRectangle(Brushes.DimGray, banner.X_Pos * Settings.Width + 1, banner.Y_Pos * Settings.Height - 29, Settings.Width / 11, Settings.Height / 6.8f);
        }
        private void DrawKey(Graphics image, Tool key)
        {
            SolidBrush keyColor = new SolidBrush(Color.FromArgb(10, 10, 10)); // black colour for key
            SolidBrush floorColour = new SolidBrush(Color.FromArgb(35, 35, 35)); //gray colour for floor

            if (hero.X_Pos == key.X_Pos && hero.Y_Pos == key.Y_Pos)// if we find the key
            {
                key.HasKey = true;

                //removes key from the Game Field
                key.X_Pos = -10;
                key.Y_Pos = -10;
            }
            //Draw Key
            image.FillEllipse(keyColor, key.X_Pos * Settings.Width, key.Y_Pos * Settings.Height + 6, Settings.Width / 1.9f, Settings.Height / 1.9f);
            image.FillRectangle(keyColor, key.X_Pos * Settings.Width + 6, key.Y_Pos * Settings.Height + 11, Settings.Width / 1.3f, Settings.Height / 5);
            image.FillRectangle(keyColor, key.X_Pos * Settings.Width + 27, key.Y_Pos * Settings.Height + 17, Settings.Width / 8, Settings.Height / 4.5f);
            image.FillRectangle(keyColor, key.X_Pos * Settings.Width + 20, key.Y_Pos * Settings.Height + 17, Settings.Width / 8, Settings.Height / 4.5f);
            image.FillEllipse(floorColour, key.X_Pos * Settings.Width + 4, key.Y_Pos * Settings.Height + 9.5f, Settings.Width / 3.5f, Settings.Height / 3.5f);
        }
        private void DrawHero(Graphics image, Hero hero)
        {
            //Draw Hero
            SolidBrush brown = new SolidBrush(Color.FromArgb(98, 49, 0));
            SolidBrush blue = new SolidBrush(Color.FromArgb(0, 98, 145));
            SolidBrush red = new SolidBrush(Color.FromArgb(196, 0, 0));

            SolidBrush yellow = new SolidBrush(Color.FromArgb(153, 153, 0));
            SolidBrush silver = new SolidBrush(Color.FromArgb(127, 127, 127));

            SolidBrush humanColour = new SolidBrush(Color.FromArgb(231, 158, 109));

            image.FillRectangle(blue, hero.X_Pos * Settings.Width + 5, hero.Y_Pos * Settings.Height + 15, hero.Width / 6, hero.Height / 2);
            image.FillRectangle(Brushes.Black, hero.X_Pos * Settings.Width + 5, hero.Y_Pos * Settings.Height + 26, hero.Width / 6, hero.Height / 7);
            image.FillRectangle(blue, hero.X_Pos * Settings.Width + 20, hero.Y_Pos * Settings.Height + 15, hero.Width / 6, hero.Height / 2);
            image.FillRectangle(Brushes.Black, hero.X_Pos * Settings.Width + 20, hero.Y_Pos * Settings.Height + 26, hero.Width / 6, hero.Height / 7);
            image.FillRectangle(red, hero.X_Pos * Settings.Width + 5, hero.Y_Pos * Settings.Height, hero.Width / 1.5f, hero.Height / 1.5f);//RED
            image.FillRectangle(blue, hero.X_Pos * Settings.Width + 5, hero.Y_Pos * Settings.Height + 16, hero.Width / 1.5f, hero.Height / 6);
            image.FillRectangle(humanColour, hero.X_Pos * Settings.Width + 10, hero.Y_Pos * Settings.Height - 9, hero.Width / 3, hero.Height / 3);
            image.FillRectangle(brown, hero.X_Pos * Settings.Width + 10, hero.Y_Pos * Settings.Height - 9, hero.Width / 3, hero.Height / 10);

            //Draw only if hero has a sword
            if (sword.IsSwordPicked || excalibur.IsExcaliburPicked)
            {
                image.FillRectangle(brown, hero.X_Pos * Settings.Width + 21, hero.Y_Pos * Settings.Height + 14, hero.Width / 3, hero.Height / 10);
                image.FillRectangle(yellow, hero.X_Pos * Settings.Width + 20, hero.Y_Pos * Settings.Height + 11, hero.Width / 10, hero.Height / 3.5f);
                image.FillRectangle(silver, hero.X_Pos * Settings.Width + 9, hero.Y_Pos * Settings.Height + 14, hero.Width / 2.5f, hero.Height / 10);
            }

            image.FillRectangle(Brushes.Black, hero.X_Pos * Settings.Width + 11, hero.Y_Pos * Settings.Height - 5, hero.Width / 11, hero.Height / 11);
            image.FillRectangle(Brushes.Black, hero.X_Pos * Settings.Width + 17, hero.Y_Pos * Settings.Height - 5, hero.Width / 11, hero.Height / 11);
            image.FillRectangle(red, hero.X_Pos * Settings.Width + 24, hero.Y_Pos * Settings.Height + 1, hero.Width / 6, hero.Height / 2.5f);
            image.FillRectangle(humanColour, hero.X_Pos * Settings.Width + 24, hero.Y_Pos * Settings.Height + 12, hero.Width / 6, hero.Height / 7);
            image.FillRectangle(red, hero.X_Pos * Settings.Width + 1, hero.Y_Pos * Settings.Height + 1, hero.Width / 6, hero.Height / 2.5f);
            image.FillRectangle(humanColour, hero.X_Pos * Settings.Width + 1, hero.Y_Pos * Settings.Height + 12, hero.Width / 6, hero.Height / 7);


            //Draw HealthBar
            int showHealthBarPoints = (hero.HitPoints <= 30) ? hero.HitPoints : 30;
            if (scabbard.HasScabbard)
            {
                showHealthBarPoints = 30;
            }

            image.FillRectangle(Brushes.DarkGreen, hero.X_Pos * Settings.Width,
                hero.Y_Pos * Settings.Height - 17, showHealthBarPoints, 5);
        }
        private void DrawVillain(Graphics image, Villain villain)
        {
            //Draw Villain
            SolidBrush darkBrown = new SolidBrush(Color.FromArgb(60, 30, 0));
            SolidBrush red = new SolidBrush(Color.FromArgb(23, 23, 23));
            SolidBrush humanColour = new SolidBrush(Color.FromArgb(231, 158, 109));

            image.FillRectangle(darkBrown, villain.X_Pos * Settings.Width + 5, villain.Y_Pos * Settings.Height + 15, villain.Width / 6, villain.Height / 2);
            image.FillRectangle(Brushes.Black, villain.X_Pos * Settings.Width + 5, villain.Y_Pos * Settings.Height + 26, villain.Width / 6, villain.Height / 7);
            image.FillRectangle(darkBrown, villain.X_Pos * Settings.Width + 20, villain.Y_Pos * Settings.Height + 15, villain.Width / 6, villain.Height / 2);
            image.FillRectangle(Brushes.Black, villain.X_Pos * Settings.Width + 20, villain.Y_Pos * Settings.Height + 26, villain.Width / 6, villain.Height / 7);
            image.FillRectangle(red, villain.X_Pos * Settings.Width + 5, villain.Y_Pos * Settings.Height, villain.Width / 1.5f, villain.Height / 1.5f);//RED
            image.FillRectangle(darkBrown, villain.X_Pos * Settings.Width + 5, villain.Y_Pos * Settings.Height + 16, villain.Width / 1.5f, villain.Height / 6);
            image.FillRectangle(humanColour, villain.X_Pos * Settings.Width + 10, villain.Y_Pos * Settings.Height - 9, villain.Width / 3, villain.Height / 3);
            image.FillRectangle(Brushes.Black, villain.X_Pos * Settings.Width + 10, villain.Y_Pos * Settings.Height - 9, villain.Width / 3, villain.Height / 10);

            image.FillRectangle(Brushes.DarkRed, villain.X_Pos * Settings.Width + 11, villain.Y_Pos * Settings.Height - 5, villain.Width / 11, villain.Height / 11);
            image.FillRectangle(Brushes.DarkRed, villain.X_Pos * Settings.Width + 17, villain.Y_Pos * Settings.Height - 5, villain.Width / 11, villain.Height / 11);
            image.FillRectangle(red, villain.X_Pos * Settings.Width + 24, villain.Y_Pos * Settings.Height + 1, villain.Width / 6, villain.Height / 2.5f);
            image.FillRectangle(humanColour, villain.X_Pos * Settings.Width + 24, villain.Y_Pos * Settings.Height + 12, villain.Width / 6, villain.Height / 7);
            image.FillRectangle(red, villain.X_Pos * Settings.Width + 1, villain.Y_Pos * Settings.Height + 1, villain.Width / 6, villain.Height / 2.5f);
            image.FillRectangle(humanColour, villain.X_Pos * Settings.Width + 1, villain.Y_Pos * Settings.Height + 12, villain.Width / 6, villain.Height / 7);


            //Draw HealthBar
            int showHealthBarPoints = (villain.HitPoints <= 30) ? villain.HitPoints : 30;

            image.FillRectangle(Brushes.DarkRed, villain.X_Pos * Settings.Width,
                villain.Y_Pos * Settings.Height - 17, showHealthBarPoints, 5);
        }

        private void DrawInsideHouse(Graphics image, House house)
        {
            //Draw Walls
            house.ConstructWalls();
            SolidBrush wallcolour = new SolidBrush(Color.FromArgb(64, 64, 64)); // grey colour for walls

            foreach (var point in house.Points)
            {
                if (!house.AreInsidePointsAdded)
                {
                    inStructureSolidPoints.Add(point);
                }
                image.FillRectangle(wallcolour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            house.AreInsidePointsAdded = true;
            house.Points.Clear();

            //Draw Floor
            house.ConstructFloor();
            SolidBrush floorColour = new SolidBrush(Color.FromArgb(172, 86, 0)); // brown colour for floor

            foreach (var point in house.Points)
            {
                image.FillRectangle(floorColour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            house.Points.Clear();

            //Draw "fake door"
            house.ConstructFakeDoor();
            SolidBrush furnitureColour = new SolidBrush(Color.FromArgb(115, 58, 0)); // brown colour for the furniture
            foreach (var point in house.Points)
            {
                image.FillRectangle(Brushes.SaddleBrown, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);

                //Draw table
                image.FillEllipse(furnitureColour, (point.X + 5) * Settings.Width, (point.Y - 3) * Settings.Height, Settings.Width, Settings.Height / 4);
                image.FillRectangle(furnitureColour, (point.X + 5) * Settings.Width + 12, (point.Y - 3) * Settings.Height, Settings.Width / 4, Settings.Height);

                //Draw chair
                image.FillRectangle(furnitureColour, (point.X + 6) * Settings.Width, (point.Y - 3) * Settings.Height + 12, Settings.Width, Settings.Height / 4);
                image.FillRectangle(furnitureColour, (point.X + 6) * Settings.Width + 20, (point.Y - 3) * Settings.Height - 5, Settings.Width / 4, Settings.Height);
                image.FillRectangle(furnitureColour, (point.X + 6) * Settings.Width + 3, (point.Y - 3) * Settings.Height + 18, Settings.Width / 4, Settings.Height / 4);
            }

            house.Points.Clear();
        }

        private void DrawLake(Graphics image, Lake lake)
        {
            //Draw Lake
            lake.ConstructRectangleObject();
            foreach (var point in lake.Points)
            {
                if (!lake.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.DodgerBlue, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            lake.ArePointsAdded = true;
            lake.Points.Clear();
        }

        private void DrawInsideCaveHouse(Graphics image, House caveHouse)
        {
            //Draw Walls
            caveHouse.ConstructWalls();
            SolidBrush wallcolour = new SolidBrush(Color.FromArgb(64, 64, 64)); // grey colour for walls

            foreach (var point in caveHouse.Points)
            {
                if (!caveHouse.AreInsidePointsAdded)
                {
                    inStructureSolidPoints.Add(point);
                }
                image.FillRectangle(wallcolour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            caveHouse.AreInsidePointsAdded = true;
            caveHouse.Points.Clear();

            //Draw Floor
            caveHouse.ConstructFloor();
            SolidBrush floorColour = new SolidBrush(Color.FromArgb(35, 35, 35)); // brown/gray colour for floor

            foreach (var point in caveHouse.Points)
            {
                image.FillRectangle(floorColour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            caveHouse.Points.Clear();

            //Draw "fake door"
            caveHouse.ConstructFakeDoor(); // Black colour for the only 1 fake door point
            image.FillRectangle(Brushes.Black, caveHouse.Points[0].X * Settings.Width, caveHouse.Points[0].Y * Settings.Height, Settings.Width, Settings.Height);

            caveHouse.Points.Clear();
        }

        private void DrawWallPartition(Graphics image, House caveHouse)
        {
            SolidBrush wallcolour = new SolidBrush(Color.FromArgb(64, 64, 64)); // grey colour for walls
            SolidBrush doorColour = new SolidBrush(Color.FromArgb(120, 60, 0)); // custom brown color for door
            SolidBrush floorColour = new SolidBrush(Color.FromArgb(35, 35, 35)); // gray colour for floor

            caveHouse.ConstuctWallPartition();
            foreach (var point in caveHouse.Points)
            {
                if (!caveHouse.IsWallAdded)
                {
                    inStructureSolidPoints.Add(point);
                }

                image.FillRectangle(wallcolour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);

                if (point == caveHouse.Points[caveHouse.Points.Count - 2])
                {
                    //Draw Lock
                    image.FillRectangle(doorColour, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
                    image.FillEllipse(Brushes.Black, point.X * Settings.Width + 8.5f, point.Y * Settings.Height + 3, Settings.Width / 2.5f, Settings.Height / 2.5f);
                    image.FillRectangle(Brushes.Black, point.X * Settings.Width + 11.2f, point.Y * Settings.Height + 7.5f, Settings.Width / 5, Settings.Height / 2);
                }
            }

            if (hero.X_Pos == caveHouse.Points[caveHouse.Points.Count - 2].X - 1 && hero.Y_Pos == caveHouse.Points[caveHouse.Points.Count - 2].Y && key.HasKey)
            {
                inStructureSolidPoints.Remove(caveHouse.Points[caveHouse.Points.Count - 2]);
                Settings.CaveDoorUnlocked = true;
            }
            if (Settings.CaveDoorUnlocked)
            {
                image.FillRectangle(floorColour, caveHouse.Points[caveHouse.Points.Count - 2].X * Settings.Width, caveHouse.Points[caveHouse.Points.Count - 2].Y * Settings.Height, Settings.Width, Settings.Height);
            }

            caveHouse.IsWallAdded = true;
            caveHouse.Points.Clear();
        }
        private void DrawCaveHouse(Graphics image, House caveHouse)
        {
            //Draw House (main part of the house without roof)
            caveHouse.ConstructRectangleObject();
            foreach (var point in caveHouse.Points)
            {
                if (!caveHouse.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.DarkSlateGray, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            caveHouse.ArePointsAdded = true;
            caveHouse.Points.Clear();

            //Draw Door(Cave Entrance)
            caveHouse.ConstructDoor();
            SolidBrush doorColour = new SolidBrush(Color.FromArgb(20, 30, 0)); // dark color for door

            Point doorPoint = caveHouse.Points[0]; // there is only 1 door(Cave Entrance)
            if (!caveHouse.IsDoorAdded)
            {
                doorPoints.Add(new Point(doorPoint.X, doorPoint.Y + 1));//Add points in list to trigger entering in door !!!
                caveHouse.IsDoorAdded = true;
            }
            image.FillRectangle(doorColour, doorPoint.X * Settings.Width, doorPoint.Y * Settings.Height, Settings.Width, Settings.Height);

            caveHouse.Points.Clear();
        }
        private void DrawHouse(Graphics image, House house)
        {
            //Draw House (main part of the house without roof)
            house.ConstructRectangleObject();
            foreach (var point in house.Points)
            {
                if (!house.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.Sienna, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            house.Points.Clear();

            //Draw Roof
            house.ConstructRoof();
            foreach (var point in house.Points)
            {
                if (!house.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.DarkRed, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            house.ArePointsAdded = true;
            house.Points.Clear();

            //Draw Door
            house.ConstructDoor();
            SolidBrush doorColour = new SolidBrush(Color.FromArgb(120, 60, 0)); // custom brown color for door
            SolidBrush gray = new SolidBrush(Color.FromArgb(60, 60, 60)); // gray colour for handle of door

            Point doorPoint = house.Points[0]; // there is only 1 door
            if (!house.IsDoorAdded)
            {
                doorPoints.Add(new Point(doorPoint.X, doorPoint.Y + 1));//Add points in list to trigger entering in door   !!!
                house.IsDoorAdded = true;
            }
            image.FillRectangle(doorColour, doorPoint.X * Settings.Width + 4, doorPoint.Y * Settings.Height, Settings.Width / 1.2f, Settings.Height);
            image.FillEllipse(gray, doorPoint.X * Settings.Width + 5, doorPoint.Y * Settings.Height + 14, Settings.Width / 4, Settings.Height / 4);


            house.Points.Clear();
        }

        private void DrawTree(Graphics image, Tree tree)
        {
            //Draw Tree(crown)
            tree.ConstructRectangleObject();
            foreach (var point in tree.Points)
            {
                if (!tree.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.DarkGreen, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            tree.Points.Clear();

            // Draw Stem
            tree.ConstructStem();
            foreach (var point in tree.Points)
            {
                if (!tree.ArePointsAdded)
                {
                    outsideSolidPoints.Add(point);
                }
                image.FillRectangle(Brushes.SaddleBrown, point.X * Settings.Width, point.Y * Settings.Height, Settings.Width, Settings.Height);
            }

            tree.ArePointsAdded = true;
            tree.Points.Clear();

            //Draw Apples
            List<Point> applePoints = tree.AddApplePoints();

            if (tree.IsAppleEaten)// if hero has eaten apple ==> Do not Draw apples on tree
            {
                applePoints.Clear();
            }
            else
            {
                var firstApple = applePoints.FirstOrDefault();
                var secApple = applePoints.Skip(1).FirstOrDefault();
                var thirdApple = applePoints.LastOrDefault();
                image.FillEllipse(Brushes.DarkRed, firstApple.X * Settings.Width + 10, firstApple.Y * Settings.Height + 5, Settings.Width / 2, Settings.Height / 2);
                image.FillEllipse(Brushes.DarkRed, secApple.X * Settings.Width + 5, secApple.Y * Settings.Height + 20, Settings.Width / 2, Settings.Height / 2);
                image.FillEllipse(Brushes.DarkRed, thirdApple.X * Settings.Width - 5, thirdApple.Y * Settings.Height + 5, Settings.Width / 2, Settings.Height / 2);
            }

        }

        private void DrawSword(Graphics image, Tool sword1, Tool sword2)
        {
            if (hero.X_Pos == sword1.X_Pos && hero.Y_Pos == sword1.Y_Pos && !sword2.IsExcaliburPicked)
            {
                sword1.IsSwordPicked = true;
            }
            if (sword1.IsSwordPicked && Settings.IsItemDropped)
            {
                sword1.X_Pos = hero.X_Pos;
                sword1.Y_Pos = hero.Y_Pos;
            }
            if (!sword1.IsSwordPicked || (sword1.IsSwordPicked && Settings.IsItemDropped))
            {
                DrawDetailedSword(image, sword1);
                sword1.IsSwordPicked = false;
            }
        }

        private void DrawDetailedSword(Graphics image, Tool sword)
        {
            SolidBrush razorColour = new SolidBrush(Color.FromArgb(105, 105, 105));
            SolidBrush handleColour = new SolidBrush(Color.FromArgb(98, 49, 0));
            SolidBrush brown = new SolidBrush(Color.FromArgb(157, 79, 0));

            image.FillRectangle(razorColour, sword.X_Pos * Settings.Width + 11, sword.Y_Pos * Settings.Height, Settings.Width / 5, Settings.Height);
            image.FillRectangle(brown, sword.X_Pos * Settings.Width + 4, sword.Y_Pos * Settings.Height + 5, Settings.Width / 1.5f, Settings.Height / 7);
            image.FillRectangle(handleColour, sword.X_Pos * Settings.Width + 11, sword.Y_Pos * Settings.Height - 5, Settings.Width / 5, Settings.Height / 2.5f);
            image.FillEllipse(brown, sword.X_Pos * Settings.Width + 9.2f, sword.Y_Pos * Settings.Height - 10, Settings.Width / 3.5f, Settings.Height / 3.5f);
            image.FillRectangle(brown, sword.X_Pos * Settings.Width + 3, sword.Y_Pos * Settings.Height + 6, Settings.Width / 16, Settings.Height / 7);
            image.FillRectangle(brown, sword.X_Pos * Settings.Width + 24, sword.Y_Pos * Settings.Height + 6, Settings.Width / 16, Settings.Height / 7);
        }

        private void DrawExcalibur(Graphics image, Tool sword1, Tool sword2)
        {
            if (hero.X_Pos == sword1.X_Pos && hero.Y_Pos == sword1.Y_Pos && !sword2.IsSwordPicked)
            {
                sword1.IsExcaliburPicked = true;
            }
            if (sword1.IsExcaliburPicked && Settings.IsItemDropped)
            {
                sword1.X_Pos = hero.X_Pos;
                sword1.Y_Pos = hero.Y_Pos;
            }
            if (!sword1.IsExcaliburPicked || (sword1.IsExcaliburPicked && Settings.IsItemDropped))
            {
                DrawDetailedExcalibur(image, excalibur);
                sword1.IsExcaliburPicked = false;
            }
        }

        private void DrawDetailedExcalibur(Graphics image, Tool sword)
        {
            SolidBrush handleColour = new SolidBrush(Color.FromArgb(135, 65, 0));
            SolidBrush yellow = new SolidBrush(Color.FromArgb(153, 153, 0));

            SolidBrush silver1 = new SolidBrush(Color.FromArgb(138, 138, 138));
            SolidBrush silver2 = new SolidBrush(Color.FromArgb(127, 127, 127));


            image.FillRectangle(silver2, sword.X_Pos * Settings.Width + 11, sword.Y_Pos * Settings.Height, Settings.Width / 15, Settings.Height);
            image.FillRectangle(silver1, sword.X_Pos * Settings.Width + 13, sword.Y_Pos * Settings.Height, Settings.Width / 15, Settings.Height);
            image.FillRectangle(silver2, sword.X_Pos * Settings.Width + 15, sword.Y_Pos * Settings.Height, Settings.Width / 15, Settings.Height);

            image.FillRectangle(yellow, sword.X_Pos * Settings.Width + 4, sword.Y_Pos * Settings.Height + 5, Settings.Width / 1.5f, Settings.Height / 7);
            image.FillRectangle(handleColour, sword.X_Pos * Settings.Width + 11, sword.Y_Pos * Settings.Height - 5, Settings.Width / 5, Settings.Height / 2.5f);
            image.FillEllipse(yellow, sword.X_Pos * Settings.Width + 9.2f, sword.Y_Pos * Settings.Height - 10, Settings.Width / 3.5f, Settings.Height / 3.5f);
            image.FillRectangle(yellow, sword.X_Pos * Settings.Width + 3, sword.Y_Pos * Settings.Height + 6, Settings.Width / 16, Settings.Height / 7);
            image.FillRectangle(yellow, sword.X_Pos * Settings.Width + 24, sword.Y_Pos * Settings.Height + 6, Settings.Width / 16, Settings.Height / 7);
            image.FillEllipse(Brushes.Blue, sword.X_Pos * Settings.Width + 12, sword.Y_Pos * Settings.Height - 7.53f, Settings.Width / 9, Settings.Height / 9);
        }

        private void DrawScabbard(Graphics image, Tool scabbard)
        {
            if (hero.X_Pos == scabbard.X_Pos && hero.Y_Pos == scabbard.Y_Pos)
            {
                scabbard.HasScabbard = true;
                //remove scabbard
                scabbard.X_Pos = -10;
                scabbard.Y_Pos = -10;
                MessageBox.Show("It is said that it's bearer would never be wounded.\n" +
                    "It is slightly wet. Probably the sword is near water?", "The Scabbard of Excalibur");
            }

            //Draw Scabbard
            SolidBrush yellow = new SolidBrush(Color.FromArgb(200, 200, 0));

            image.FillRectangle(Brushes.Blue, scabbard.X_Pos * Settings.Width + 9, scabbard.Y_Pos * Settings.Height + 1, Settings.Width / 3, Settings.Height / 1.4f + 4);
            image.FillEllipse(Brushes.Blue, scabbard.X_Pos * Settings.Width + 9, scabbard.Y_Pos * Settings.Height + 19, Settings.Width / 3, Settings.Height / 3);
            image.FillRectangle(yellow, scabbard.X_Pos * Settings.Width + 13.1f, scabbard.Y_Pos * Settings.Height + 1, Settings.Width / 18, Settings.Height / 1.1f);
            image.FillRectangle(yellow, scabbard.X_Pos * Settings.Width + 9, scabbard.Y_Pos * Settings.Height + 8, Settings.Width / 3, Settings.Height / 18);
        }

    }
}
