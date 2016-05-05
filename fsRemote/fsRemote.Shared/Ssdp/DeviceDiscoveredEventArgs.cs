using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

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
