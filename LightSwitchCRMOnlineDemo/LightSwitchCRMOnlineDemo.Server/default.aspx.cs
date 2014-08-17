using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.LightSwitch.Utilities.Server.Internal;
using Microsoft.Office365.OAuth;

namespace LightSwitchApplication
{
    public partial class _Default : Microsoft.LightSwitch.Framework.Server.DefaultPageBase
    {
        protected override async void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var task = await new CRMSample().GetAccessToken();                
                basePageLoad();
            }
            catch (RedirectRequiredException ex)
            {
                Response.Redirect(ex.RedirectUri.ToString(), false);
            }
        }

        private void basePageLoad()
        {
            string url;
            string defaultClientName = ConfigurationManager.AppSettings["Microsoft.LightSwitch.DefaultClientName"];
            if (defaultClientName != null)
            {
                url = string.Format("./{0}/{1}{2}", (object)defaultClientName,
                    (object)LightSwitchAuthenticationConfiguration.ClientLaunchPageName,
                    (object)this.Request.Url.Query);
            }
            else
            {
                url = (string)null;
                this.Response.StatusCode = 500;
                this.Response.End();
            }

            this.Response.Redirect(url, false);
        }
    }
}
