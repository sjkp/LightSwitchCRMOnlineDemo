using System;
using System.Threading.Tasks;

namespace LightSwitchApplication
{
    /// <summary>
    /// Class mimicing the other Clients of the Office 365 API tools set for accessing CRM online data.
    /// </summary>
    class CRMClient
    {

        private Func<Task<string>> _accessTokenGetter;
        private object _syncLock = new object();

        private string _accessToken;
        private Uri _serviceRoot;

        public CRMClient(Uri serviceRoot, System.Func<Task<string>> accessTokenGetter)
        {
            _accessTokenGetter = accessTokenGetter;
            _serviceRoot = serviceRoot;

        }

        private async Task SetToken()
        {
            string token = await this._accessTokenGetter();
            lock (this._syncLock)
                this._accessToken = token;
        }


        public async Task<string> GetAccessToken()
        {
            await SetToken();
            return _accessToken;
        }
    }
}