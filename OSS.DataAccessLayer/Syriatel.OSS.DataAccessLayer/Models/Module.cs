using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.DataAccess.Models
{
    public class Module
    {
        
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string RootName { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public virtual ModuleBar ModuleBar { get; set; }
    }
}