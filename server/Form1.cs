using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace step1_server
{
    public partial class Form1 : Form
    {
        
        struct clientInformation                //stores username and socket of the client together
        {
            public string userName;
            public Socket clientSocket;
            public clientInformation(string name, Socket socket) { userName = name; clientSocket = socket; }
        }

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<clientInformation> clientSockets = new List<clientInformation>();         //store info about connected clients in a list

        bool terminating = false;
        bool listening = false;
        int sweet_id = 0;                   //unique ID for received sweet posts


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }
        private int get_sweet_id()          //get sweet id for the next sweet
        {
            string filename = @"sweets.txt";
            if (System.IO.File.Exists(filename))
            {
                if (new FileInfo(filename).Length == 0) //no sweets posted yet
                {
                    return 0;
                }
                else
                {
                    var lines = File.ReadAllLines(filename).Reverse();  //read the file in reverse to find the last id
                    int count = 0;
                    int lastID = 0;
                    foreach (string line in lines)
                    {
                        if (count == 4)
                        {
                            lastID = Int32.Parse(line);
                            break;
                        }
                        count++;
                    }
                    return lastID + 1;      //next id = last id + 1
                }
            }
            else
            {
                return 0;   //no sweets posted yet
            }
        }


        private void buttonListen_Click(object sender, EventArgs e)
        {
            
            sweet_id = get_sweet_id();
            int serverPort;

            if (Int32.TryParse(textBoxPort.Text, out serverPort))   //port number needs to be an integer
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                textBoxPort.Enabled = false;
                listening = true;
                buttonListen.Enabled = false;

                Thread acceptThread = new Thread(Accept);           //start accepting clients
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    checkUserName(newClient);                   //after accepting a client; check user name, if user name is 
                                                                //unaccaptable, disconnect the client
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void sendBoolean(bool value, Socket clientSocket) //send boolean value to the client
        {
            Byte[] buffer = BitConverter.GetBytes(value);
            clientSocket.Send(buffer);
        }

        private void checkUserName(Socket newClient)
        {
            Byte[] buffer = new Byte[64];
            newClient.Receive(buffer);                              //receive user name from client
            string user_name = Encoding.Default.GetString(buffer);
            user_name = user_name.Substring(0, user_name.IndexOf("\0"));

            if (clientSockets.Any(client => client.userName == user_name))      //a client with this user name has already connected
            {
                sendBoolean(false, newClient);  //give feedback to the client, connection is disabled

                logs.AppendText("Another client with the same username is already connected, client is not accepted.\n");
                newClient.Close();
            }
            else
            {
                //string fileName = "../../user-db.txt";
                string fileName = @"C:\Users\hp\Documents\Visual Studio 2019\Projects\cs408\step1_server\user-db.txt";
                string[] lines = File.ReadAllLines(fileName);

                if (!lines.Contains(user_name))     //this user name does not exist in the user database
                {
                    sendBoolean(false, newClient);      //give feedback to the client, connection is disabled

                    logs.AppendText("The client's username does not exists in the database, client is not accepted.\n");
                    newClient.Close();
                }
                else
                {
                    sendBoolean(true, newClient);       //give feedback to the client, connection is succeeded

                    clientInformation accepted_client = new clientInformation(user_name, newClient);
                    clientSockets.Add(accepted_client);
                    logs.AppendText("Client " + user_name + " is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(accepted_client));  //connection is successfull, start receiving messages
                    receiveThread.Start();
                }
            }
        }

        private void Receive(clientInformation thisClient)  //receive messages from cients
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];               //receive message from client                    
                    thisClient.clientSocket.Receive(buffer);
                    Console.WriteLine(Encoding.Default.GetString(buffer));

                    string operation = Encoding.Default.GetString(buffer);          //the client sends the name of the operation
                    operation = operation.Substring(0, operation.IndexOf("\0"));    //to be completed before sending actual data

                    
                    if (operation == "request")
                    {
                        sendRequested(thisClient);
                    }
                    else if (operation == "post")
                    {
                        postMessage(thisClient);
                    }



                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("Client " + thisClient.userName + " has disconnected\n");
                    }
                    thisClient.clientSocket.Close();
                    clientSockets.Remove(thisClient);       //remove the client from the client list
                    connected = false;
                }
            }
        }

        private void sendRequested(clientInformation thisClient)    //send all sweets that was not posted by that user
        {
            logs.AppendText("\nThe following sweets are sent to user " + thisClient.userName + ":\n");

            string fileName = @"C:\Users\hp\Documents\Visual Studio 2019\Projects\cs408\step1_server\posts.txt";
            if (System.IO.File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)        //we have at least 1 sweet already posted
                {
                    string[] lines = File.ReadAllLines(fileName);

                    string sweet = "";
                    int i = 1;
                    bool willBeSent = false;

                    foreach (string line in lines)      //iterate through the sweet file
                    {
                        if (line != "")             //skip the empty lines
                        {
                            if (i % 4 == 2)         //sweets are written in 4 lines, that's why modula 4 is used
                            {
                                if (line != thisClient.userName)        //sweet's user name must not match with the user name of the client
                                {
                                    willBeSent = true;      //this sweet will be sent to the client
                                }
                            }
                            sweet += line;
                            sweet += "\n";

                            if (i % 4 == 0)
                            {
                                if (willBeSent)
                                {
                                    logs.AppendText(sweet + "\n");
                                    Byte[] buffer = Encoding.Default.GetBytes(sweet);
                                    thisClient.clientSocket.Send(buffer);                      //send sweets to client one by one
                                    Thread.Sleep(3);    //wait for a short time to give time for client to receive, before sending next data
                                }
                                willBeSent = false;
                                sweet = "";
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void postMessage(clientInformation thisClient)
        {
            Byte[] buffer_3 = new Byte[64];               //receive sweet from client
            thisClient.clientSocket.Receive(buffer_3);

            string time_now = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            string incomingMessage = Encoding.Default.GetString(buffer_3);
            incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

            File.AppendAllText(@"C:\Users\hp\Documents\Visual Studio 2019\Projects\cs408\step1_server\posts.txt", sweet_id + "\n" + thisClient.userName
                + "\n" + incomingMessage + "\n" + time_now
                + "\n\n");                                       //add sweet to the sweet file

            logs.AppendText("\nThe following sweet is posted\n");
            logs.AppendText("Sweet ID: " + sweet_id + "\n");
            logs.AppendText("Username: " + thisClient.userName + "\n");
            logs.AppendText("Message: " + incomingMessage + "\n");
            logs.AppendText("Time stamp: " + time_now + "\n\n");
            sweet_id++;
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
      
    }
}
