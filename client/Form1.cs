using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;


//ip:10.50.46.127 su
//ip:192.168.1.114 home
namespace step1_client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;         //connected to the server
        bool disconnected = false;      //only becomes true when disconnect button is clicked, when reconnected becomes false
        Socket clientSocket;
        string username;                //user name of the client
        string currentTextBoxInUse = "none";    //to switch between output text boxes
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBoxIp.Text;
            username = textBoxUsn.Text;

            int portNum;
            if (!Int32.TryParse(textBoxPort.Text, out portNum))         //check inputs
            {
                logs.AppendText("Please check port number \n");
            }
            else if (IP == "")
            {
                logs.AppendText("Ip address slot can not be empty\n");
            }
            else if (username == "" || username.Length > 64)
            {
                logs.AppendText("User name is not in a valid length\n");
            }
            else                                            //inputs are valid
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    checkConnection();                      //check whether server accepts this client
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
        }

        private void checkConnection()  //send the user name and receive feedback from server, if connection is successful start receiving
        {
            try
            {
                Byte[] buffer = Encoding.Default.GetBytes(username);    //send username to server
                clientSocket.Send(buffer);

                Byte[] buffer_2 = new Byte[1];                          //receive feedback from the server
                clientSocket.Receive(buffer_2);                         //true or false

                if (buffer_2[0] > 0)    //if true
                {
                    buttonConnect.Enabled = false;
                    buttonDisc.Enabled = true;
                    textBoxIp.Enabled = false;
                    textBoxPort.Enabled = false;
                    textBoxUsn.Enabled = false;

                    textBoxPost.Enabled = true;
                    buttonPost.Enabled = true;
                    buttonGetPosts.Enabled = true;


                    connected = true;
                    disconnected = false;
                    logs.AppendText("Connected to the server!\n");

                    Thread receiveThread = new Thread(Receive);         //we are connected to the server, we can start receiving messages
                    receiveThread.Start();
                }
                else    //if false
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server did not accept the user name!\n");
                        buttonConnect.Enabled = true;
                        textBoxPost.Enabled = false;
                        buttonPost.Enabled = false;
                    }
                    clientSocket.Close();
                    connected = false;
                }

            }
            catch
            {
                logs.AppendText("The server did not reply back about connection!\n");
            }
        }

        private void Receive()
        {
            while (connected)    //continuously check for messages sent from server
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    
                        logs.AppendText(incomingMessage + "\n");
                    
                }
                catch
                {
                    if (!terminating && !disconnected)  //lost connection, but disconnect button has not been clicked
                    {
                        logs.AppendText("The server has disconnected\n");
                        
                        buttonDisc.Enabled = false;
                        buttonGetPosts.Enabled = false;
                        buttonPost.Enabled = false;


                        buttonConnect.Enabled = true;
                        textBoxIp.Enabled = true;
                        textBoxPort.Enabled = true;
                        textBoxUsn.Enabled = true;

                        clientSocket.Close();           //close connection to the socket
                        connected = false;
                    }
                }
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            currentTextBoxInUse = "default";

            string operation = "post";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);

            string message = textBoxPost.Text;

            if (message.Length <= 64)
            {
                Byte[] buffer2 = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer2);                              

                logs.AppendText("\nThe following sweet is posted\n");
                logs.AppendText("Username: " + username + "\n");
                logs.AppendText("Message: " + message + "\n");
                logs.AppendText("Time stamp: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "\n\n");
            }
        }

        private void buttonDisc_Click(object sender, EventArgs e)
        {
            username = "";
            clientSocket.Close();
            connected = false;
            terminating = true;
            buttonConnect.Enabled = true;
            buttonDisc.Enabled = false;

            textBoxUsn.Enabled = true;
            textBoxIp.Enabled = true;
            textBoxPort.Enabled = true;

            buttonPost.Enabled = false;
            textBoxPost.Enabled = false;
            buttonGetPosts.Enabled = false;

            logs.AppendText("Disconnected from the server\n");
        }


        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void buttonGetPosts_Click(object sender, EventArgs e)
        {
            

            string operation = "request";
            Byte[] buffer = Encoding.Default.GetBytes(operation);
            clientSocket.Send(buffer);                              //get posts from server

            logs.AppendText("\nRequested posts: \n");
        }

        
    }
}
