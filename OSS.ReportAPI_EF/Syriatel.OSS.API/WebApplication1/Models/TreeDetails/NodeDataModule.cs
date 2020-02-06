using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.TreeDetails
{
    public class NodeDataModule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public NodeDataModule(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}