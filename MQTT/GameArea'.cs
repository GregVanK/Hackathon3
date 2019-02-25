using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MQTT
{
    public partial class GameArea_ : Form
    {
        public enum ShipType
        {
            Carrier,
            Battleship,
            Cruiser,
            Destroyer,
            Sub,

        }
        // 2 Subs (1 length)
        // 2 Destroyers (2 length)
        // 1 Cruiser (3 length)
        // 1 Battleship (4 length)
        // 1 Airship Carrier (5 length)
        ShipType[] ships = new ShipType[7];
        Button[,] buttons = new Button[10, 10];
        Label lblMessage = new Label();
        int counter = 0;
        //bool canPlaceUp = false;
        //bool canPlaceDown = false;
        //bool canPlaceLeft = false;
        //bool canPlaceRight = false;
        //bool buttonClicked = false;
        public GameArea_()
        {
            ships[0] = ShipType.Sub;
            ships[1] = ShipType.Sub;
            ships[2] = ShipType.Destroyer;
            ships[3] = ShipType.Destroyer;
            ships[4] = ShipType.Cruiser;
            ships[5] = ShipType.Battleship;
            ships[6] = ShipType.Carrier;

            InitializeComponent();


            lblMessage.Text = "Test";
            lblMessage.Location = new Point(420, 180);
            lblMessage.Size = new System.Drawing.Size(900, 50);
            lblMessage.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(lblMessage);


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button temp = new Button();
                    temp.Name = i.ToString() + j.ToString();
                    temp.Width = 40;
                    temp.Height = 40;
                    temp.Location = new Point(j * temp.Width, i * temp.Height);
                    temp.Click += new EventHandler(this.placingShips);
                    temp.Tag = new Tuple<int, int>(j, i);
                    Console.WriteLine(temp.Tag.ToString());
                    buttons[i, j] = temp;
                    this.Controls.Add(temp);
                }
            }
        }

        private void targetHit(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            int index;
            Int32.TryParse(pressed.Name, out index);

        }

        private void placingShips(object sender, EventArgs e)
        {
            if (counter <= 6)
            {
                Button pressed = (Button)sender;
                Tuple<int, int> buttonPressedCoords = (Tuple<int, int>)pressed.Tag;
                Tuple<int, int> newButton = new Tuple<int, int>(0, 0);
                string result = Form1.Prompt.RequestShipDirection("Ship Direction", "Ship Direction");
                lblMessage.Text = result;
                lblMessage.Text = $"You just placed a {ships[counter].ToString()}";
                int length = 0;
                switch (ships[counter])
                {
                    case ShipType.Sub:
                        length = 2;
                        break;
                    case ShipType.Destroyer:
                        length = 3;
                        break;
                    case ShipType.Cruiser:
                        length = 4;
                        break;
                    case ShipType.Carrier:
                        length = 5;
                        break;
                    case ShipType.Battleship:
                        length = 6;
                        break;
                    default:
                        break;
                }

                switch (result)
                {
                    case "left":
                        for (int i = 0; i < length; i++)
                        {
                            newButton = new Tuple<int, int>(buttonPressedCoords.Item1 - i, buttonPressedCoords.Item2);
                            if (newButton.Item2 <= 9 && newButton.Item1 <= 9 && newButton.Item1 >= 0 && newButton.Item2 >= 0)
                            {
                                buttons[newButton.Item2, newButton.Item1].BackColor = Color.Blue;
                            }
                        }
                        break;
                    case "right":
                        for (int i = 0; i < length; i++)
                        {
                            newButton = new Tuple<int, int>(buttonPressedCoords.Item1 + i, buttonPressedCoords.Item2);
                            if (newButton.Item2 <= 9 && newButton.Item1 <= 9 && newButton.Item1 >= 0 && newButton.Item2 >= 0)
                            {
                                buttons[newButton.Item2, newButton.Item1].BackColor = Color.Blue;
                            }
                        }
                        break;
                    case "up":
                        for (int i = 0; i < length; i++)
                        {
                            newButton = new Tuple<int, int>(buttonPressedCoords.Item1, buttonPressedCoords.Item2 - i);
                            buttons[newButton.Item2, newButton.Item1].BackColor = Color.Blue;
                        }
                        break;
                    case "down":
                        for (int i = 0; i < length; i++)
                        {
                            newButton = new Tuple<int, int>(buttonPressedCoords.Item1, buttonPressedCoords.Item2 + i);
                            if (newButton.Item2 <= 9 && newButton.Item1 <= 9 && newButton.Item1 >= 0 && newButton.Item2 >= 0)
                            {
                                buttons[newButton.Item2, newButton.Item1].BackColor = Color.Blue;
                            }
                        }
                        break;
                    default:
                        break;
                }
                if (newButton.Item2 <= 9 && newButton.Item1 <= 9 && newButton.Item1 >= 0 && newButton.Item2 >= 0)
                {
                    int buttonsItem1 = buttonPressedCoords.Item1 - newButton.Item1;
                    buttons[newButton.Item2, newButton.Item1].BackColor = Color.Purple;
                    pressed.BackColor = Color.Red;
                }
                else
                {
                    lblMessage.Text = "Invalid location, choose again";
                }
            }
            counter++;
            Console.WriteLine(counter.ToString());



            //if (!buttonClicked)
            //{
            //    Button pressed = (Button)sender;
            //    pressed.BackColor = Color.Red;
            //    Tuple<int, int> buttonPressedTag = (Tuple<int, int>)pressed.Tag;
            //    Console.WriteLine(pressed.Tag.ToString());
            //    Tuple<int, int> newButtonXPositive = new Tuple<int, int>(0, 0);
            //    //foreach (ShipType type in ships)
            //    //{

            //    //    switch (type)
            //    //    {
            //    //        case ShipType.Sub:
            //    //            break;
            //    //        case ShipType.Destroyer:
            //    //            newButtonXPositive = new Tuple<int, int>(buttonPressedTag.Item1, buttonPressedTag.Item2 + 1);
            //    //            Tuple<int, int> newButton2 = new Tuple<int, int>(buttonPressedTag.Item1, buttonPressedTag.Item2 + 1);
            //    //            break;
            //    //        case ShipType.Cruiser:
            //    //            break;
            //    //        case ShipType.Carrier:
            //    //            break;
            //    //        case ShipType.Battleship:
            //    //            break;
            //    //        default:
            //    //            break;
            //    //    }
            //    //    buttons[newButtonXPositive.Item1, newButtonXPositive.Item2].BackColor = Color.Blue;
            //    //}

            //    newButtonXPositive = new Tuple<int, int>(buttonPressedTag.Item1 + 1, buttonPressedTag.Item2);
            //    if (newButtonXPositive.Item2 <= 9 && newButtonXPositive.Item1 <= 9)
            //    {
            //        buttons[newButtonXPositive.Item2, newButtonXPositive.Item1].BackColor = Color.Blue;
            //        lblMessage.Text = "Choose a blue square for your ship placement.";
            //    }
            //    else
            //    {
            //        lblMessage.Text = "The ship cannot be placed here.";
            //    }
            //    buttonClicked = true;
            //}
            //else
            //{

            //}

        }
    }
}
