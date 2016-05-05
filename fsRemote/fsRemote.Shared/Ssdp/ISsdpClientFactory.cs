using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace fsRemote.Shared.Ssdp
{
    public interface ISsdpClientFactory
    {
        ISsdpClient Init(IPAddress iPAddress);
    }
}
