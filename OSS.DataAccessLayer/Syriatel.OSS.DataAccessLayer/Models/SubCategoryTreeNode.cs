using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.DataAccess.Models
{
    public class SubCategoryTreeNode
    {
        [Key]
        public int Id { get; set; }

        public int Level { get; set; }

        public int Order { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public int ParentId { get; set; }

        public int SubCategoryTreeRootId { get; set; }

        public SubCategoryTreeRoot SubCategoryTreeRoot { get; set; }

        public virtual DataField DataField { get; set; }
    }
}
