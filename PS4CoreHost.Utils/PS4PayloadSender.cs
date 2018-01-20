using socket.core.Client;
using SocketLibrary;
using System;
using System.Net;
using System.Net.Sockets;

namespace PS4CoreHost.Utils
{
    public class PS4PayloadSender : IPayloadSender, IDisposable
    {
        private readonly TcpPushClient _client;
        private byte[] _data;

        public PS4PayloadSender()
        {
            _client = new TcpPushClient(1024);
            _client.OnConnect += _client_OnConnect;
            //_client.OnClose += _client_OnClose;
            _client.OnSend += _client_OnSend;
        }

        private void _client_OnSend(int obj)
        {
            Console.WriteLine($"!!!!!!!!!!!!!!{obj}");
        }

        //private void _client_OnClose()
        //{
        //    Console.WriteLine("-----------CLOSED !!!!!!!!!!!!!!");
        //}

        public void Send(string host, byte[] data, int port = 9020)
        {
            //_client.Connect(host, port);
            //_data = data;

            //using (var socket = new ConnectedSocket(host, port)) // Connects to 127.0.0.1 on port 1337
            //{
            //    socket.Send("Hello world!"); // Sends some data
            //    var data1 = socket.Receive(); // Receives some data back (blocks execution)
            //}

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(host, port);
            socket.Send(data);

        }

        private void _client_OnConnect(bool connected)
        {
            if (connected)
            {
                _client.Send(_data, 0, _data.Length);
            }
        }

        public void Dispose()
        {
            //_client.Close();
        }
    }
}
