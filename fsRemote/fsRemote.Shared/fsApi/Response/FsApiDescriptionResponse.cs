using System.Xml.Serialization;

namespace fsRemote.Shared.fsApi.Response
{
	[XmlRootAttribute(ElementName = "netRemote")]
	public class FsApiDescriptionResponse
	{
		[XmlElementAttribute("friendlyName")]
		public string FriendlyName { get; set; }

		[XmlElementAttribute("version")]
		public string Version { get; set; }

		[XmlElementAttribute("webfsapi")]
		public string WebFsApi { get; set; }
	}
}

