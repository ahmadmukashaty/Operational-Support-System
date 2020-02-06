using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.DataAccess.Models
{
    public class DataField
    {
         [ForeignKey("SubCategoryTreeNode")]
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string RelatedApi { get; set; }

        public int SubCategoryTreeNodeId { get; set; }

        public virtual SubCategoryTreeNode SubCategoryTreeNode { get; set; }
    }
}
