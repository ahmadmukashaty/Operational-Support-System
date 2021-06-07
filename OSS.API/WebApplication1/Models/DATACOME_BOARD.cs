using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_BOARD")]
    public class DATACOME_BOARD
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("TYPE_ID")]
        public int typeId { get; set; }

        [Column("SUBRACK_ID")]
        public int subRackId { get; set; }

        [Column("SLOT_ID")]
        public int slotId { get; set; }

        [Column("HARDWARE_VERSION")]
        public string hardwareVersion { get; set; }

        [Column("SOFTWARE_VERSION")]
        public string softwareVersion { get; set; }

        [Column("SERIAL_NUMBER")]
        public string serialNumber { get; set; }

        [Column("REMARKS")]
        public string Remarks { get; set; }

        [Column("BARCODE")]
        public string Barcode { get; set; }

        [Column("BIOS_VERSION")]
        public string biosVersion { get; set; }

        [Column("FPGA_VERSION")]
        public string FPGAVersion { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("BOM_ITEM")]
        public string BOMItem { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("MANUFACTURE_DATE")]
        public Nullable<System.DateTime> manufactureDate { get; set; }
        [Column("DEPLOYMENT_STATUS")]
        public Nullable<decimal> deploymentStatus { get; set; }
        [Column("FREE_SUBBOARD")]
        public Nullable<int> freeSubBoard { get; set; }

        public virtual DATACOM_BOARD_TYPE DATACOM_BOARD_TYPE { get; set; }
        public virtual ICollection<DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        public virtual ICollection<DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
    }
}