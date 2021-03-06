﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace fsRemote.Shared.Ssdp
{
	class SearchResponse
	{
		internal Guid Id
		{
			get;
			private set;
		}

		internal bool IsValid
		{
			get
			{
				return validResponseLine
				&& (secondsToCache >= 0)
				&& (!string.IsNullOrEmpty(UpnpDescriptionLink))
				&& (!string.IsNullOrEmpty(server))
				&& (!string.IsNullOrEmpty(searchTarget))
				&& (!string.IsNullOrEmpty(usn))
				&& (!Id.Equals(Guid.Empty));
			}
		}

		internal IPEndPoint EndPoint
		{
			get;
			private set;
		}

		internal string UpnpDescriptionLink
		{
			get;
			private set;
		}

		internal SearchResponse(string receivedHeader, IPEndPoint endPoint)
		{
			try
			{
				foreach (var line in receivedHeader.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (line == "HTTP/1.1 200 OK")
					{
						validResponseLine = true;
					}
					else if (line.ToUpper().StartsWith("CACHE-CONTROL:"))
					{
						var maxAge = line.Substring(14).Trim();
						var seconds = maxAge.Split('=').ElementAt(1).Trim();
						secondsToCache = Int32.Parse(seconds);
					}
					else if (line.ToUpper().StartsWith("LOCATION:"))
					{
						UpnpDescriptionLink = line.Substring(9).Trim();
					}
					else if (line.ToUpper().StartsWith("SERVER:"))
					{
						server = line.Substring(7).Trim();
					}
					else if (line.ToUpper().StartsWith("ST:"))
					{
						searchTarget = line.Substring(3).Trim();
					}
					else if (line.ToUpper().StartsWith("USN:"))
					{
						usn = line.Substring(4).Trim();
						var uuid = usn.Split(':').ElementAt(1);
						Id = Guid.Parse(uuid);
					}
				}

				received = DateTime.Now;
				EndPoint = endPoint;
			}
			catch (Exception e)
			{
                Debug.WriteLine("Error parsing search response: " + e);
			}
		}

		int secondsToCache = -1;
		readonly bool validResponseLine;
		readonly DateTime received;
		readonly string server = null;
		readonly string searchTarget = null;
		readonly string usn = null;
	}
}
