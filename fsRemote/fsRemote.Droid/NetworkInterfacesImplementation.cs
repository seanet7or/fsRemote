using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using fsRemote.Shared.Dependencies;
using Java.Net;

[assembly: Xamarin.Forms.Dependency(typeof(fsRemote.Droid.NetworkInterfacesImplementation))]
namespace fsRemote.Droid
{
    public class NetworkInterfacesImplementation : INetworkInterfaces
    {
        public IEnumerable<IPAddress> GetConnectedIPAddresses()
        {
           var addresses = new List<IPAddress>();

            Java.Util.IEnumeration networkInterfaces = NetworkInterface.NetworkInterfaces;
            while (networkInterfaces.HasMoreElements)
            {
                var netInterface = (NetworkInterface)networkInterfaces.NextElement();
                foreach (var interfaceAddress in netInterface.InterfaceAddresses)
                {
                    IPAddress ipv4;
                    if (IPAddress.TryParse(interfaceAddress.Address.HostAddress, out ipv4))
                    {
                        if (ipv4.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            addresses.Add(ipv4);
                        }
                    }
                }
            }
            return addresses;
        }
    
    }
}