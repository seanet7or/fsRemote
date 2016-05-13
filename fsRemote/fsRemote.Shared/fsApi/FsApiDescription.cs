using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using fsRemote.Shared.fsApi.Response;
using System.Diagnostics;
using Xamarin.Forms;

namespace fsRemote.Shared.fsApi
{
    public static class FsApiDescription
	{
		public static async Task<FsApiDescriptionResponse> ReadDeviceDescriptionAsync(string descriptionUrl)
		{
			try
			{
                Debug.Assert(DependencyService.Get<IHttpClientFactory>() != null);
                using (var client = DependencyService.Get<IHttpClientFactory>().Create())
                {
                    Debug.Assert(client != null);
                    using (var responseStream = await client.GetStreamAsync(descriptionUrl))
                    {
                        var responseDto = (FsApiDescriptionResponse)new XmlSerializer(
                                              typeof(FsApiDescriptionResponse)).Deserialize(responseStream);
                        return responseDto;
                    }
                }
			}
			catch (Exception e)
			{
				Debug.WriteLine("Error downloading FsApi device description: " + e.Message);
			}
			return null;
		}
	}
}

