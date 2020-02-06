using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace Syriatel.OSS.API.Models
{
    public class FIREWALL_PORT
    {
        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public int PORT_ID { get; set; }
        public string PORT_NAME { get; set; }
        public string RATE_BPS { get; set; }
        public string PORT_LEVEL { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        public int PARENT_ID { get; set; }

        public virtual FIREWALL_PORT_TYPE FIREWALL_PORT_TYPE { get; set; }
        public virtual FIREWALL_SUBBOARD FIREWALL_SUBBOARD { get; set; }
    }
}