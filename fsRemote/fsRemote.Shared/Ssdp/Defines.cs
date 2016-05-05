using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fsRemote.Shared.Ssdp
{
    class Defines
    {
        internal const string MulticastIpv4Address = "239.255.255.250";

        internal const int SsdpPort = 1900;

        internal static IPEndPoint SsdpMulticastEndpoint = new IPEndPoint(IPAddress.Parse(MulticastIpv4Address), SsdpPort);
    }
}
