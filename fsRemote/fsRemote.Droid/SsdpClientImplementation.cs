using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using fsRemote.Shared.Ssdp;
using System.Net.Sockets;
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