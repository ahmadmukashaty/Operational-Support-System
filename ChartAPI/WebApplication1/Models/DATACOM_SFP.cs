using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("DATACOM_SFP")]
    public class DATACOM_SFP
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("TYPE")]
        public string TYPE { get; set; }
        [Column("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
        [Column("SM_MM")]
        public string SM_MM { get; set; }
        [Column("SPEED")]
        public Nullable<decimal> SPEED { get; set; }
        [Column("WAVELENGTH")]
        public Nullable<decimal> WAVELENGTH { get; set; }
        [Column("TRANSMISSION_DISTANCE")]
        public Nullable<decimal> TRANSMISSION_DISTANCE { get; set; }
        [Column("FIBER_CABLE_TYPE")]
        public string FIBER_CABLE_TYPE { get; set; }
        [Column("MANUFACTURE")]
        public string MANUFACTURE { get; set; }
        [Column("RECEIVE_POWER")]
        public Nullable<decimal> RECEIVE_POWER { get; set; }
        [Column("REFERENCE_RECEIVE_POWER")]
        public string REFERENCE_RECEIVE_POWER { get; set; }
        [Column("REFERENCE_RECEIVE_TIME")]
        public Nullable<decimal> REFERENCE_RECEIVE_TIME { get; set; }
        [Column("UPPER_RECEIVE_POWER")]
        public Nullable<decimal> UPPER_RECEIVE_POWER { get; set; }
        [Column("LOWER_RECEIVE_POWER")]
        public Nullable<decimal> LOWER_RECEIVE_POWER { get; set; }
        [Column("TRANSMIT_POWER")]
        public Nullable<decimal> TRANSMIT_POWER { get; set; }
        [Column("REFERENCE_TRANSMIT_POWER")]
        public string REFERENCE_TRANSMIT_POWER { get; set; }
        [Column("REFERENCE_TRANSMIT_TIME")]
        public Nullable<decimal> REFERENCE_TRANSMIT_TIME { get; set; }
        [Column("UPPER_TRANSMIT_POWER")]
        public Nullable<decimal> UPPER_TRANSMIT_POWER { get; set; }
        [Column("LOWER_TRANSMIT_POWER")]
        public Nullable<decimal> LOWER_TRANSMIT_POWER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
    }
}