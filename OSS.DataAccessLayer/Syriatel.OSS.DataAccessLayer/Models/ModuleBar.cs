using Syriatel.OSS.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.DataAccess.Models
{
    public class ModuleBar
    {
        [Key]
        public int Id { get; set; }

        public int SearchFlag { get; set; }

        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}