﻿using fsRemote.Shared.fsApi.Response;
using Xamarin.Forms;

namespace fsRemote.Shared
{
    class DeviceSearchPageDeviceCell : TextCell
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var fsApiDesc = BindingContext as FsApiDescriptionResponse;
            if (fsApiDesc != null)
            {
                Text = fsApiDesc.FriendlyName;
                Detail = fsApiDesc.WebFsApi;
            }
        }
    }
}
