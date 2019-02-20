using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //ip you are connecting to
        static string  IP = "134.41.140.214";
        //unique ID of you, must be different on other client
        string clientID = "GregDev";
        //extra step authentication
        string user = "dev";  
        string pass = "dev1234";
        //stored data
        string chatlog = "";

        //Connect to IP
        MqttClient client = new MqttClient(IP);

        //Connect button
        private void button1_Click(object sender, EventArgs e)
        {
 
            try
            {
                //connect (get result bool)
                byte output = client.Connect(clientID, user, pass);
                //Recieve messages from anyone publishing to "ctrl2" 
                ushort returnmessage = client.Subscribe(new string[]{ "ctrl2"},new byte[]{ MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE});

                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                client.MqttMsgPublished += client_MqttMsgPublished;
            }
            catch (Exception ex)
            {

            }
        }
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

            //add output to total chat log
            chatlog += output + "\r\n";
            //update new chat
            SetText(chatlog);
        }

        private void MessageBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //generate new message object (Pull message, add username)
            Message messageData = new Message("Greg", MessageOutputBox.Text);
            //add message to chatlog
            chatlog += messageData._name +":" + MessageOutputBox.Text + "\r\n"; 
            //convert message object to Json string
            string s = JsonConvert.SerializeObject(messageData);
            //Send Json item to people subscribed to "ctrl1"
            client.Publish("ctrl1", Encoding.UTF8.GetBytes(s));
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
    }
}
