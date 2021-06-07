using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("RIM_SUBCATEGORY")]
    public class RIM_SUBCATEGORY
    {
        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [Column("NAME")]
        public string Name  { get; set; }

        [Column("ORDER")]
        public Nullable<int> Order { get; set; }

        [Column("PARENT_ID")]
        public Nullable<int> parentId { get; set; }

        [Column("CATEGORY_ID")]
        public Nullable<int> categoryId { get; set; } 
        public virtual ICollection<DATACOM_NE> DATACOM_NE { get; set; }
        public virtual RIM_CATEGORY RIM_CATEGORY { get; set; }

    }
}