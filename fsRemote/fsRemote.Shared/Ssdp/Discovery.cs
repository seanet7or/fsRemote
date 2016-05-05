using fsRemote.Shared.Dependencies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fsRemote.Shared.Ssdp
{
	class Discovery
	{
		readonly ConcurrentDictionary<Guid, SearchResponse> searchResponses = new ConcurrentDictionary<Guid, SearchResponse>();

		const int RequestsToSend = 2;
		const int SearchRequestsInterval = 10;

		List<ISsdpClient> clients = new List<ISsdpClient>();

		byte[] buffer = new byte[65536];


		byte[] searchRequestData;

		internal Discovery()
		{
			/*var ipAddresses = new List<IPAddress>();

			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (item.OperationalStatus == OperationalStatus.Up)
				{
					IPInterfaceProperties adapterProperties = item.GetIPProperties();

					foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							clients.Add(new SsdpClient(ip.Address));
						}
					}

				}
			}*/

			foreach (var address in DependencyService.Get<INetworkInterfaces>().GetConnectedIPAddresses())
			{
				clients.Add(DependencyService.Get<ISsdpClientFactory>().Init(address));
			}
		}

		IDeviceSearchResponseListener responseListener;

		internal async Task<SearchResponse[]> SearchAsync(int secondsToSearch, IDeviceSearchResponseListener listener)
		{
			responseListener = listener;
			var searchRequest = new MSearchRequest(SearchTargets.RootDevices, secondsToSearch);
			searchRequestData = searchRequest.GetHeaderData();

			foreach (var client in clients)
			{
				client.BeginReceive(ReceiveCallback, client);
				for (int i = 0; i < RequestsToSend; i++)
				{
					client.Send(searchRequestData, searchRequestData.Length, Defines.SsdpMulticastEndpoint);
					await Task.Delay(SearchRequestsInterval);
				}
			}
			await Task.Delay((secondsToSearch + 1) * 1000);
			return searchResponses.Values.ToArray();
		}

		async void ReceiveCallback(IAsyncResult ar)
		{
			var remoteEndpoint = new IPEndPoint(IPAddress.Any, Defines.SsdpPort);
			var client = (ISsdpClient)ar.AsyncState;
			var data = client.EndReceive(ar, ref remoteEndpoint);
			string receiveString = Encoding.ASCII.GetString(data, 0, data.Length);
			client.BeginReceive(ReceiveCallback, client);

			if (receiveString.StartsWith("HTTP/1.1 200 OK", StringComparison.Ordinal))
			{
				var searchResponse = new SearchResponse(receiveString, remoteEndpoint);
				if (searchResponse.IsValid)
				{
					bool firstResponse = !searchResponses.ContainsKey(searchResponse.Id);
					searchResponses[searchResponse.Id] = searchResponse;
					await responseListener.OnSearchResponseReceived(searchResponse);
				}
				else
				{
					Debug.WriteLine("Invalid response: " + receiveString);
				}
			}
			//Console.WriteLine("Received from {1}:\n{0}", receiveString, remoteEndpoint.ToString());
		}


	}
}
