using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT
{
    public partial class Form1 : Form
    {
        private string fleetName;
        private string regionName;
        private string gameName;
        private string enemyName;
        private List<string> playersInGame = new List<string>();
        private bool gameIsRunning = false;
        private bool respondToGameComms = false;
        private bool gameIsStarting = false;
        public Form1()
        {
            regionName = Prompt.RequestRegionName("Region Name", "Region Name");
            fleetName = Prompt.RequestFleetName("Fleet Name", "Fleet Name");
            enemyName = Prompt.RequestEnemyName("Enemy Name", "Enemy Name");
            Console.WriteLine(fleetName);
            Console.WriteLine(regionName);
            while (fleetName == "" || regionName == "" || enemyName == "")
            {
                gameName = Prompt.RequestGameName("Game Name", "Game Name");
                regionName = Prompt.RequestRegionName("Region", "Region Name");
                fleetName = Prompt.RequestFleetName("Fleet", "Fleet Name");
                enemyName = Prompt.RequestEnemyName("Enemy Name", "Enemy Name");
                Console.WriteLine(fleetName);
                Console.WriteLine(regionName);
            }

            InitializeComponent();

        }
        //ip you are connecting to
        static string  IP = "134.41.136.157";
        //unique ID of you, must be different on other client
        string clientID = "VaughnDev";
        //extra step authentication
        string user = "dev";  
        string pass = "dev1234";
        //stored data
        string chatlog = "";

        //Connect to IP
        MqttClient client = new MqttClient(IP);

     
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {

        }
        

        //Recieve message protocol
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Exstract message object string
            string s = Encoding.UTF8.GetString(e.Message);
            //convert message string using Json converter into a Message object
            Message recievedData = JsonConvert.DeserializeObject<Message>(s);
            //Pull data from messae object
            String output = recievedData._name +":"+ recievedData._msg;
            Regex rx = new Regex("^[*]");
            if (!gameIsStarting)
            {
                if (recievedData._msg.Contains("PingResponseComms"))
                {
                    playersInGame.Add(recievedData._name);
                }
                if (recievedData._name != fleetName)
                {
                    if (playersInGame.Count == 0)
                    {
                        playersInGame.Add(fleetName);
                        Message messageData = new Message(fleetName, $"command: PingResponseComms," +
                            $"\nfleetName: [{fleetName}]" +
                            $"\nresponseTo: {recievedData._msg}");
                        //Message responseMessage = new Message(fleetName, $"responseTo: {recievedData._msg}");
                        string smessage = JsonConvert.SerializeObject(messageData);
                        //string smessage2 = JsonConvert.SerializeObject(responseMessage);
                        //Send Json item to people subscribed to "ctrl1"
                        client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(smessage));
                        //client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(smessage2));
                        playersInGame.Add(recievedData._name);
                    }
                    else
                    {
                        playersInGame.Add(fleetName);
                    }

                    for (int i = 0; i < playersInGame.Count; i++)
                    {
                        Console.WriteLine(playersInGame[i]);
                    }
                }
            }
            else if (!respondToGameComms)
            {
                if (recievedData._msg.Contains("GameStartComms"))
                {
                    Message gameStartComms = new Message(fleetName, $"command: GameResponseComms," +
                                $"\nregionName: [{regionName}]" +
                                $"\nfleetName: [{fleetName}]");
                    string gameStart = JsonConvert.SerializeObject(gameStartComms);
                    client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(gameStart));
                    respondToGameComms = true;
                }
            }
            else
            {
                // if game is not running
                    // start the game
                    // game is running to true
                // if game is running
                    // if message contains x or y
                        // split message at y
                        // talk to game w/ x y coords
            }

            if (playersInGame.Count == 2)
            {
                if (!respondToGameComms)
                {
                    Message gameStartComms = new Message(fleetName, $"command: GameStartComms," +
                                $"\nregionName: [{regionName}]");
                    string gameStart = JsonConvert.SerializeObject(gameStartComms);
                    client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(gameStart));
                    respondToGameComms = true;
                }
                gameIsStarting = true;
            }
                //if (playersInGame != null)
                //{
                //    for (int i = 0; i < playersInGame.Count; i++)
                //    {
                //        if (playersInGame[i] != recievedData._name)
                //        {
                //            Message messageData = new Message(fleetName, $"[{fleetName}]");
                //            Message responseMessage = new Message(fleetName, $"responseTo: {recievedData._msg}");
                //            string smessage = JsonConvert.SerializeObject(messageData);
                //            string smessage2 = JsonConvert.SerializeObject(responseMessage);
                //            //Send Json item to people subscribed to "ctrl1"
                //            client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(smessage));
                //            client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(smessage2));
                //            playersInGame.Add(recievedData._name);
                //        }
                //    }


            //add output to total chat log
            chatlog += output + "\r\n";
            //update new chat
            SetText(chatlog);
        }

        private void MessageBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string subscribedTo = $"battleship/{regionName}/{fleetName}";
                string regionSubscribe = $"battleship/{regionName}";
                string[] subscriptionList = { subscribedTo, regionSubscribe };
                Console.WriteLine(subscribedTo);
                //connect (get result bool)
                byte output = client.Connect(clientID, user, pass);
                //Recieve messages from anyone publishing to "ctrl2" 
                //client.Subscribe(subscriptionList, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                ushort returnmessage = client.Subscribe(new string[] { regionSubscribe }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                ushort returnMessage2 = client.Subscribe(new string[] { subscribedTo }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });


                Message messageData = new Message(fleetName, $"command:PingStartComms,\nfleetName: [{fleetName}]");
                string s = JsonConvert.SerializeObject(messageData);
                //Send Json item to people subscribed to "ctrl1"
                client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(s));
                SetText(chatlog);

                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                client.MqttMsgPublished += client_MqttMsgPublished;
            }
            catch (Exception ex)
            {

            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string enemyGame = $"{gameName}/{regionName}/{enemyName}";
            Console.WriteLine(enemyGame);
            //generate new message object (Pull message, add username)
            Message messageData = new Message(fleetName, MessageOutputBox.Text);
            //add message to chatlog
            //chatlog += messageData._name +":" + MessageOutputBox.Text + "\r\n"; 
            //convert message object to Json string
            string s = JsonConvert.SerializeObject(messageData);
            //Send Json item to people subscribed to "ctrl1"
            client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(s));
            SetText(chatlog);
        }

        //During hackathon1, we had issues with setting a text box ""safely"" so this is how i guess

        // View for more details https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-make-thread-safe-calls-to-windows-forms-controls

        //something needed for changing textboxes
        delegate void SetTextCallback(string text);

        //Specialized for setting text box
        private void SetText(string text)
        {

            if (this.DataBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.DataBox.Text = text;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DataBox_TextChanged(object sender, EventArgs e)
        {

        }

        public static class Prompt
        {
            public static string RequestGameName(string name, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = name };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
            public static string RequestFleetName(string name, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = name };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }

            public static string RequestRegionName(string region, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = region };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }

            public static string RequestEnemyName(string name, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = name };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void sendFleetButton_Click(object sender, EventArgs e)
        {
            Message messageData = new Message(fleetName, $"[{fleetName}]");
            string s = JsonConvert.SerializeObject(messageData);
            //Send Json item to people subscribed to "ctrl1"
            client.Publish($"battleship/{regionName}", Encoding.UTF8.GetBytes(s));
            SetText(chatlog);
        }
    }
}
