using System;               //For console
using System.Net;
using System.Net.Sockets;   //For TCPClient, NetworkStream, SocketException

namespace TCPEchoServer
{

    class TcpEchoServer
    {
        private const int BUFSIZE = 32; //Size of receive buffer

        static void Main(string[] args)
        {
            if (args.Length > 1)
                throw new ArgumentException("Parameters : [<Port>]");

            int servPort = (args.Length == 1) ? Int32.Parse(args[0]) : 7;

            TcpListener listener = null;

            try
            {
                var IpAdd = System.Net.IPAddress.Parse("127.0.0.1");
                //Create a TCPListener to accept client connections
                listener = new TcpListener(IpAdd, servPort);
                listener.Start();
                Console.WriteLine("Listener started on {0} ip address and {1} port number", IpAdd, servPort);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }

            byte[] rcvBuffer = new byte[BUFSIZE];   //Receive buffer
            int bytesRcvd;

            for (; ; )
            {
                TcpClient client = null;
                NetworkStream netStream = null;

                try
                {
                    client = listener.AcceptTcpClient();
                    netStream = client.GetStream();
                    Console.Write("Handling client - ");

                    //Receive until client closes connection , indicated by 0 return value
                    int totalBytesEchoed = 0;
                    while ((bytesRcvd = netStream.Read(rcvBuffer, 0, rcvBuffer.Length)) > 0)
                    {
                        netStream.Write(rcvBuffer, 0, bytesRcvd);
                        totalBytesEchoed += bytesRcvd; 
                    }
                    Console.WriteLine("echoed {0} bytes.", totalBytesEchoed);

                    //Close the stream and socket. We are done with this client
                    netStream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    netStream.Close();
                }
            }
        }
    }
}
