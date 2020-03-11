using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;
using Newtonsoft.Json;

namespace BookTcpServer
{
    public class Server
    {
        private static readonly List<Book> Books = new List<Book>()
        {
            new Book("Bog 1", "Fortatter 99", "1234567890123", 200),
            new Book("Bog 2", "Fortatter 60", "0987654321234", 594),
            new Book("Bog 3", "Fortatter 100", "6758493021234", 125)

        };

        public void Start()
        {
            //IP config
            IPAddress ipa = IPAddress.Parse("127.0.0.1");
            ipa = IPAddress.Parse("192.168.1.132");
            TcpListener tcp = new TcpListener(ipa, 4646);

            //Starts server
            tcp.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                Task.Run((() =>
                {
                    TcpClient tempSocket = tcp.AcceptTcpClient();
                    string socketIp = tempSocket.Client.RemoteEndPoint.ToString();
                    Console.WriteLine($"Client {socketIp} Connected");
                    //Connect client
                    DoClient(tempSocket);

                    if (!tempSocket.Connected) Console.WriteLine($"Client {socketIp} Disconeted");
                    tempSocket.Close();

                }));
            }


            //Stop server
            tcp.Stop();
        }

        public void DoClient(TcpClient socket)
        {


            NetworkStream ns = socket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string msg = "1";

            while (true)
            {
                try
                {
                    string[] messageArray = msg.Split(' ');
                    string param = msg.Substring(msg.IndexOf(' ') + 1);
                    string command = messageArray[0];
                    switch (command)
                    {
                        case "GetAll":
                            sw.WriteLine("Getting all books");
                            sw.WriteLine(JsonConvert.SerializeObject(Books));
                            break;

                        case "Get":
                            sw.WriteLine($"Getting book with ISBN: {param}");
                            sw.WriteLine(JsonConvert.SerializeObject(Books.Find(book => book.Isbn13 == param)));
                            break;

                        case "Save":
                            sw.WriteLine("Saving book");
                            try
                            {
                                var bookToSave = JsonConvert.DeserializeObject<Book>(param);
                                Books.Add(bookToSave);
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }

                    }
                    msg = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(msg)) break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }

            }

            ns.Close();
        }
    }
}
