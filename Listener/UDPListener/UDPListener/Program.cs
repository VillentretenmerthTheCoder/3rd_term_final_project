using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UDPListener
{
    class Program
    {

        static async Task Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient udpServer = new UdpClient(9999);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 9999);
            //IPEndPoint object will allow us to read datagrams sent from another source.

            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is blocked");

                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    if (receivedData == "Stop")
                    {
                        Console.WriteLine("The broadcast has been stopped!");
                        break;
                    }
                    else
                    {

                        string[] data = receivedData.Split("QWERTY");
                        Console.WriteLine(data.Length);
                        Console.WriteLine(data.ToString());

                        string DateTime = data[0];
                        string Text = data[1];
                       
                        Console.WriteLine(DateTime + Text);

                        Worker worker = new Worker();
                        //Posting data to Rest Service.
                        Conversation conversation = new Conversation(Text,DateTime);
                        string content = JsonConvert.SerializeObject(conversation);
                        await worker.PostDataAsync(content);



                       


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
