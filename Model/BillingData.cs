using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBillingServer.Model
{
    internal class BillingData
    {
        public int RetnCode = 0;
        public int PortalJID = 0;
        public string Mail = "0";
        public int VipType = 2;
        public int VipLevel = 0;
        public DateTime VipEnd;

        public BillingData(int ChannelID, int JID, string Mail,int VipLevel,DateTime VipEnd)
        {
            this.RetnCode = ChannelID;
            this.PortalJID = JID;
            this.Mail = Mail;
            if (VipEnd > DateTime.Now)
            {
                this.VipLevel = VipLevel;
                this.VipEnd = VipEnd;
                this.VipType = 1;
            }
            else
            {
                this.VipLevel = 0;
                this.VipEnd = VipEnd;
                this.VipType = 2;
            }
        }
    }
}
