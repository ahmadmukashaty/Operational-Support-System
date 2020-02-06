using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.DataAccess.Models
{
    public class SubCategoryTreeRoot
    {
        [ForeignKey("SubCategory")]
        [Key]
        public int Id { get; set; }

        public int SearchFlag { get; set; }

        [MaxLength(250)]
        public string DesignFile { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public ICollection<SubCategoryTreeNode> SubCategoryTreeNodes { get; set; }
    }
}
