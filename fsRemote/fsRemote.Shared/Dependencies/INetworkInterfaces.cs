using System;
using System.Collections.Generic;
using System.Net;

namespace fsRemote.Shared.Dependencies
{
	public interface INetworkInterfaces
	{
		IEnumerable<IPAddress> GetConnectedIPAddresses();
	}
}

