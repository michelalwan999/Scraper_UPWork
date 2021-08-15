using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net

namespace WebRequests
{
   public class WebClientWithCookies : WebClient
    {
        private CookieContainer _container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;

            if (request != null)
            {
                request.Method = "Post";
                request.CookieContainer = _container;
            }

            return request;
        }
    }
}
