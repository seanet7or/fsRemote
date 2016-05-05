using fsRemote.Shared.Ssdp;
using System.Net;
using System.Net.Sockets;
using System;

namespace fsRemote.Droid
{
    internal class SsdpClient : UdpClient, ISsdpClient
    {
        public SsdpClient(IPAddress iPAddress)
        {
           Client = new SsdpSocket(iPAddress);
        }

        public IAsyncResult BeginReceive(Action<IAsyncResult> receiveCallback, ISsdpClient client)
        {
            return base.BeginReceive((ar) => receiveCallback(ar), client);
        }

        public bool Connected
        {
            get
            {
                return Client != null;
            }
        }
    }    
}