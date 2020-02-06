using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_BOARD")]
    public class DATACOME_BOARD
    {
        [Column("ID")]

        public int ID { get; set; }
 
        [Column("TYPE_ID")]
        public int TYPE_ID { get; set; }
        //[DisplayName("SUBRACK ID")]
     
        [Column("SUBRACK_ID") ]
        public int SUBRACK_ID { get; set; }

        [Column("SLOT_ID")]
        public int SLOT_ID { get; set; }

        [Column("HARDWARE_VERSION")]
        public string HARDWARE_VERSION { get; set; }

        [Column("SOFTWARE_VERSION")]
        public string SOFTWARE_VERSION { get; set; }

        [Column("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }

        [Column("REMARKS")]
        public string REMARKS { get; set; }

        [Column("BARCODE")]
        public string BARCODE { get; set; }

        [Column("BIOS_VERSION")]
        public string BIOS_VERSION { get; set; }

        [Column("FPGA_VERSION")]
        public string FPGA_VERSION { get; set; }

        [Column("STATUS")]
        public string STATUS { get; set; }

        [Column("BOM_ITEM")]
        public string BOM_ITEM { get; set; }

        [Column("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [Column("MANUFACTURE_DATE")]
        public Nullable<System.DateTime> MANUFACTURE_DATE { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> DEPLOYMENT_STATUS { get; set; }
        [Column("FREE_SUBBOARD")]
        public Nullable<int> FREE_SUBBOARD { get; set; }

        public virtual DATACOM_BOARD_TYPE DATACOM_BOARD_TYPE { get; set; }
        public virtual ICollection<DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        public virtual ICollection<DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
    }
}