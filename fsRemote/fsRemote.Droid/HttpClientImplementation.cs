using System;
using System.IO;
using System.Threading.Tasks;
using fsRemote.Shared;
using System.Net;


namespace fsRemote.Droid
{
    class HttpClientImplementation : WebClient, IHttpClient
    {        
        public async Task<Stream> GetStreamAsync(string url)
        {
            
            return await OpenReadTaskAsync(new Uri(url));
        }
    }
}