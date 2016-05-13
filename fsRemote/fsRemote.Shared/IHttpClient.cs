using System;
using System.IO;
using System.Threading.Tasks;

namespace fsRemote.Shared
{
    interface IHttpClient : IDisposable
    {
        Task<Stream> GetStreamAsync(string url);
    }
}
