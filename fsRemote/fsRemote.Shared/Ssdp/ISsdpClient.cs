using System;
using System.Net;

namespace fsRemote.Shared.Ssdp
{
    public interface ISsdpClient
    {
        bool Connected { get; }

        int Send(byte[] searchRequestData, int length, IPEndPoint ssdpMulticastEndpoint);
        IAsyncResult BeginReceive(Action<IAsyncResult> receiveCallback, ISsdpClient client);
        byte[] EndReceive(IAsyncResult ar, ref IPEndPoint remoteEndpoint);
        void Close();
    }
}