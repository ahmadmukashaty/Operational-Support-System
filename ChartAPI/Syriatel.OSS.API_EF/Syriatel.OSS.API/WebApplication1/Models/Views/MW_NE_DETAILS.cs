using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Syriatel.OSS.API.Models.Views
{
    [Table("MW_NE_DETAILS")]
    public class MW_NE_DETAILS
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("CREATE DATE")]
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        [Column("DEPLOYMENT STATUS")]
        public Nullable<int> DEPLOYMENT_STATUS { get; set; }
        [Column("GATEWAY TYPE")]
        public string GATEWAY_TYPE { get; set; }
        [Column("IP")]
        public string IP { get; set; }
        [Column("LSR ID")]
        public string LSR_ID { get; set; }
        [Column("MAC ADDRESS")]
        public string MAC_ADDRESS { get; set; }
        [Column("NAME")]
        public string NAME { get; set; }
        [Column("NE ID")]
        public string NE_ID { get; set; }
        [Column("PATCH VERSION")]
        public string PATCH_VERSION { get; set; }
        [Column("REMARKS")]
        public string REMARKS { get; set; }
        [Column("SITE MODEL")]
        public string SITE_MODEL { get; set; }
        [Column("SOFTWARE VERSION")]
        public string SOFTWARE_VERSION { get; set; }
        [Column("SUBRACK TYPE")]
        public string SUBRACK_TYPE { get; set; }
        [Column("TERMINAL CLASS")]
        public string TERMINAL_CLASS { get; set; }
        [Column("U2000 REF ID")]
        public string U2000_REF_ID { get; set; }
        [Column("CLASS")]
        public string CLASS { get; set; }
        [Column("SERIES")]
        public string SERIES { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
        [Column("VENDOR")]
        public string VENDOR { get; set; }
        [Column("TOTAL CHILD NUMBER")]
        public int TOTAL_CHILD_NUMBER { get; set; }
        
        [Column("CODE")]
        public string CODE { get; set; }
        [Column("ENGLISH NAME")]
        public string ENGLISH_NAME { get; set; }
        [Column("REGION")]
        public string REGION { get; set; }
        [Column("AREA")]
        public string AREA { get; set; }
        [Column("ZONE")]
        public string ZONE { get; set; }

        [Column("SUBAREA")]
        //[DisplayName("hala hala")]
        //[Description("hallaaaa halala")]
        public string SUBAREA { get; set; }
       
       
    }

}