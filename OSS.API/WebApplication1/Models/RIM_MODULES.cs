﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_MODULES")]

    public class RIM_MODULES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIM_MODULES()
        {
            this.RIM_CATEGORIES = new HashSet<RIM_CATEGORIES>();
        }
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("NAME")]

        public string NAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RIM_CATEGORIES> RIM_CATEGORIES { get; set; }
    }
}