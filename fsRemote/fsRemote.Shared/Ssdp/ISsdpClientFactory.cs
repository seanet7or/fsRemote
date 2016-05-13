using System.Net;

namespace fsRemote.Shared.Ssdp
{
    public interface ISsdpClientFactory
    {
        ISsdpClient Init(IPAddress iPAddress);
    }
}
