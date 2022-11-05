using System.Net;
using MiniBillingServer.Http;
using MiniBillingServer.Http;
using MiniBillingServer.Model;

namespace MiniBillingServer.Handlers
{
    class EmailHandlers : FilteredHttpHandler
    {
        public override bool Handle(HttpListenerContext context)
        {
            // Validate Handler
            if (context.Request.Url.LocalPath != "/cgi/Email_Certification.asp")
            {
                return false;
            }

            var Allstring = context.Request.QueryString["values"].Split('|');

            // Security check
            base.Handle(context);

            SendResult(context.Response, BillingDB.Instance.UpdateLockPw(Allstring[0],Allstring[2],Allstring[1]));
            return true;
        }
    }
}