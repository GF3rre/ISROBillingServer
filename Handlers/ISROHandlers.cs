using MiniBillingServer.Http;
using MiniBillingServer.Model;
using System;
using System.Globalization;
using System.Net;

namespace MiniBillingServer.Handlers
{
    class ISROHandler : FilteredHttpHandler
    {
        public override bool Handle(HttpListenerContext context)
        {
            // Validate Handler
            if (context.Request.Url.LocalPath != "/Property/Silkroad-r/checkuser.aspx")
            {
                return false;
            }

            // Security check
            base.Handle(context);

            var Allstring = context.Request.QueryString["values"].Split('|');

            BillingData data = BillingDB.Instance.GetBillingData(Allstring[0], Allstring[1], Allstring[2]);


            if (data.RetnCode == 0)
            {
                SendResult(context.Response, $"{data.RetnCode}|{data.PortalJID}|{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}|Y|Y|{data.Mail}|{data.VipLevel}|{data.VipEnd.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)}|{data.VipType}");

            }
            else
            {
                SendResult(context.Response, data.RetnCode.ToString());
            }
            return true;
        }
    }
}