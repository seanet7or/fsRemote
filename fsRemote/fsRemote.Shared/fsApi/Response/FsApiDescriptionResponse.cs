using System.Xml.Serialization;

namespace fsRemote.Shared.fsApi.Response
{
	[XmlRoot(ElementName = "netRemote")]
	public class FsApiDescriptionResponse
	{
		[XmlElement("friendlyName")]
		public string FriendlyName { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("webfsapi")]
		public string WebFsApi { get; set; }
	}
}

