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
        public GameArea_()
        {
            InitializeComponent();

            Button[] buttons = new Button[100];
            for(int i = 0; i < 100; i++)
            {
                Button temp = new Button();
                temp.Name = i.ToString();
                temp.Width = 40;
                temp.Height = 40;
                temp.Location = new Point((i%10)*temp.Width,(i / 10) * temp.Height);
                temp.Click += new EventHandler(this.targetHit);
                buttons[i] = temp;
                this.Controls.AddRange(buttons);
            }
        }

        private void targetHit(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            int index;
            Int32.TryParse(pressed.Name, out index);

        }
    }
}
