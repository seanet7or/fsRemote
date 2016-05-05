using fsRemote.Shared.Dependencies;
using fsRemote.Shared.fsApi;
using fsRemote.Shared.fsApi.Response;
using fsRemote.Shared.Ssdp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fsRemote.Shared
{
    class DeviceSearchPage : ContentPage
    {
        ListView deviceListView;
        ObservableCollection<FsApiDescriptionResponse> items = new ObservableCollection<FsApiDescriptionResponse>();

        internal DeviceSearchPage()
        {
            deviceListView = new ListView
            {
                ItemsSource = items,
                ItemTemplate = new DataTemplate(typeof(DeviceSearchPageDeviceCell)),
                IsPullToRefreshEnabled = true,
                RefreshCommand = new Command(RefreshAsync)
            };
            Content = deviceListView;
            System.Threading.ThreadPool.QueueUserWorkItem(RefreshAsync);
        }

        private async void RefreshAsync(object obj)
        {
            await SearchInBackground();
        }

        async Task SearchInBackground()
        {
            Device.BeginInvokeOnMainThread(() => deviceListView.IsRefreshing = true);

            var ipAddresses = DependencyService.Get<INetworkInterfaces>().GetConnectedIPAddresses();
            using (var deviceDiscovery = new DeviceDiscovery(ipAddresses))
            {
                deviceDiscovery.DeviceDiscovered += OnDeviceDiscovered;
                await deviceDiscovery.SearchAsync(5);
            }

            Device.BeginInvokeOnMainThread(() => deviceListView.IsRefreshing = false);
        }

        async void OnDeviceDiscovered(object sender, DeviceDiscoveredEventArgs e)
        {
            if (e.SearchResponse.Usn.Contains(":fsapi:")
                || e.SearchResponse.Usn.Contains(":schemas-frontier-silicon-com:"))
            {
                var fsApiDescription = await FsApiDescription.ReadDeviceDescriptionAsync(e.SearchResponse.Location);
                if (fsApiDescription != null)
                {
                    items.Add(fsApiDescription);
                }
            }
        }
    }
}

