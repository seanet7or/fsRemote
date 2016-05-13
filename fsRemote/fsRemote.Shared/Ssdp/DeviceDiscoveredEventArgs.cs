using System.Net;

namespace fsRemote.Shared.Ssdp
{
    public class DeviceDiscoveredEventArgs
    {
        public MSearchResponse SearchResponse
        {
            get;
            set;
        }

        public IPEndPoint EndPoint
        {
            get;
            set;
        }

        public DeviceDiscoveredEventArgs(MSearchResponse searchResponse, IPEndPoint endPoint)
        {
            SearchResponse = searchResponse;
            EndPoint = endPoint;
        }
    }
}
