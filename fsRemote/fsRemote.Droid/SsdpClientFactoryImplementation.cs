using fsRemote.Shared.Ssdp;
using System.Net;

[assembly: Xamarin.Forms.Dependency(typeof(fsRemote.Droid.SsdpClientFactoryImplementation))]
namespace fsRemote.Droid
{
    public class SsdpClientFactoryImplementation : ISsdpClientFactory
    {
        public ISsdpClient Init(IPAddress iPAddress)
        {
            return new SsdpClient(iPAddress);
        }
    }
}