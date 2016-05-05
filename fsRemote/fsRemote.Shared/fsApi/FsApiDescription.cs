using System.Net;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using fsRemote.Shared.fsApi.Response;

namespace fsRemote.Shared.fsApi
{
	public static class FsApiDescription
	{
		public static async Task<FsApiDescriptionResponse> ReadDeviceDescriptionAsync(string descriptionUrl)
		{
			try
			{
				using (var webClient = new WebClient())
				{
					using (var responseStream = await webClient.OpenReadTaskAsync(new Uri(descriptionUrl)))
					{
						var responseDto = (FsApiDescriptionResponse)new XmlSerializer(
							                  typeof(FsApiDescriptionResponse)).Deserialize(responseStream);
						return responseDto;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error downloading FsApi device description: " + e.Message);
			}
			return null;
		}
	}
}

