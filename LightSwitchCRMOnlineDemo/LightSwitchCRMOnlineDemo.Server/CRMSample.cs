using System;
using System.Threading.Tasks;
using Microsoft.Office365.OAuth;

namespace LightSwitchApplication
{
    /// <summary>
    /// CRMSample class showing how to connect to CRM online by using the Office 365 API tools to get access token. 
    /// </summary>
    public class CRMSample
    {
        DiscoveryContext _discoveryContext;

        private static Uri ServiceEndpointUri = new Uri(
            "https://sjkpdev07.crm4.dynamics.com/XRMServices/2011/OrganizationData.svc/");

        private static string ServiceResourceId = "Microsoft.CRM";

        public async Task<string> GetAccessToken()
        {
            
            var client = await EnsureClientCreated();
            return await client.GetAccessToken();
        }


        private async Task<CRMClient> EnsureClientCreated()
        {
            if (_discoveryContext == null)
            {
                _discoveryContext = await DiscoveryContext.CreateAsync();
            }

            var dcr = await _discoveryContext.DiscoverResourceAsync(ServiceResourceId);

            var user = dcr.UserId;
            return new CRMClient(ServiceEndpointUri, async () =>
            {
                var accesToken = (await _discoveryContext.AuthenticationContext.AcquireTokenByRefreshTokenAsync(new SessionCache().Read("RefreshToken"), new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(_discoveryContext.AppIdentity.ClientId, _discoveryContext.AppIdentity.ClientSecret), ServiceResourceId)).AccessToken;

                return accesToken;
            });
        }
    }
}