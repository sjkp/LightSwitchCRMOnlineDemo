using System.Data.Services.Client;
using System.Web;
using LightSwitchApplication;
using Microsoft.Office365.OAuth;

namespace sjkpdev07ContextData.sjkpdev07ContextDataService
{
    public partial class sjkpdev07Context : global::System.Data.Services.Client.DataServiceContext
    {
        partial void OnContextCreated()
        {
            this.SendingRequest2 += OnSendingRequest2;
        }

        private void OnSendingRequest2(object sender, SendingRequest2EventArgs sendingRequest2EventArgs)
        {
            var token = new SessionCache().Read("AccessToken#Microsoft.CRM");
            sendingRequest2EventArgs.RequestMessage.SetHeader("Authorization", "Bearer " + token);

        }
    }
}