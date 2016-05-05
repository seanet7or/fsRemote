using System.Threading.Tasks;

namespace fsRemote.Shared.Ssdp
{
	interface IDeviceSearchResponseListener
	{
		Task OnSearchResponseReceived(SearchResponse response);
	}
}

