using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_CATEGORY")]
    public class RIM_CATEGORY
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }


        [Column("MODULE_NAME")]
        public string moduleName { get; set; }


        [Column("ORDER_NUM")]
        public Nullable<int> Order { get; set; }


        [Column("Structure")]
        public string Structure { get; set; }
        public virtual ICollection<RIM_SUBCATEGORY> RIM_SUBCATEGORY { get; set; }
    }
}