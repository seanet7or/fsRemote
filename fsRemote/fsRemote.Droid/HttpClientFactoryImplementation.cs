using fsRemote.Shared;

[assembly: Xamarin.Forms.Dependency(typeof(fsRemote.Droid.HttpClientFactoryImplementation))]
namespace fsRemote.Droid
{
    class HttpClientFactoryImplementation : IHttpClientFactory
    {
        public IHttpClient Create()
        {
            return new HttpClientImplementation();
        }
    }
}