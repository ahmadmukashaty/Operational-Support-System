using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models
{
    [Table("CATEGORY")]
    public partial class CATEGORY
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Order")]
        public Nullable<int> Order { get; set; }
        [Column("IsOpen")]
        public Nullable<short> IsOpen { get; set; }
        [Column("ModuleBarId")]
        public Nullable<int> ModuleBarId { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("NAME")]
        public string NAME { get; set; }
    }
}