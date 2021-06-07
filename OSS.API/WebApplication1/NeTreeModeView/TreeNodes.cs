using Syriatel.OSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.NeTreeModeView
{
    public class TreeNodes
    {
        private DataLookup data { get; set; }

        //public List<Category> Categories { get; set; }

        public List<Instance> SubCategories { get; set; }

        private List<Instance> NeInstances { get; set; }

        //public BoardInstances Board { get; set; }

        //public SubBoardInstances SubBoard { get; set; }

        //public PortInstances Port { get; set; }

        //public SFPInstances SFP { get; set; }

        public TreeNodes()
        {

            //this.Board = this.data.getAllBoards();
            //this.SubBoard = this.data.getAllSubBoards();
            //this.Port = this.data.getAllPorts();
            //this.SFP = this.data.getAllSFPs();
        }

        public TreeNodes(int subCategory)
        {
            this.data = new DataLookup();
            this.SubCategories = this.data.getAllSubCategories(subCategory);
            this.NeInstances = this.data.getAllNEs();
        }

        public List<Instance> GetSubCategoryNEs(int SubCategoryId)
        {
            List<Instance> SubCat_NE = null;
            foreach(Instance ins in NeInstances)
            {
                if (ins.ParentId == SubCategoryId)
                {
                    if(SubCat_NE == null)
                        SubCat_NE = new List<Instance>();

                    SubCat_NE.Add(ins);
                }
                    
            }

            return SubCat_NE;
        }
    }
}