using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ResponseTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.NeTypes
{
    public class NeTypesData
    {
        private List<NeTypeModelView> tables { get; set; }

        public TypeTree Data { get; set; }
        public TreeJason returnTree { get; set; }

        //public NeTypesData(int CategoryId)
        //{
        //    int id = 2;
        //    Init();
        //    tables = OracleHelper.GetNeTables(CategoryId);

        //    foreach(NeTypeModelView table in tables)
        //    {
        //        if (this.Data.children == null)
        //            this.Data.children = new List<TypeTree>();
                
        //        TypeTree typeTree = new TypeTree();
        //        typeTree.id = id++;
        //        typeTree.name = table.DisplayName;
                
        //        List<string> children = OracleHelper.GetNeTypeValues(table.TableName, table.ColumnName);

        //        foreach(string child in children)
        //        {
        //            if (typeTree.children == null)
        //                typeTree.children = new List<TypeTree>();

        //            TypeTree tr = new TypeTree();
        //            tr.id = id++;
        //            tr.tableName = table.TableName;
        //            tr.columnName = table.ColumnName;
        //            tr.name = child;
        //            tr.children = null;

        //            typeTree.children.Add(tr);
        //        }

                
        //        this.Data.children.Add(typeTree);
        //    }

            
        //}
        public NeTypesData(int CategoryId)
        {
            //int id = 2;
            Init();
            tables = OracleHelper.GetNeTables(CategoryId);

            foreach(NeTypeModelView table in tables)
            {
                if (this.returnTree.children == null)
                    this.returnTree.children = new List<TreeJason>();
                
                //TypeTree typeTree = new TypeTree();
             

                TreeJason typeTree2 = new TreeJason();
                typeTree2.label = table.DisplayName;
                typeTree2.selectable = true;

                List<string> children = OracleHelper.GetNeTypeValues(table.TableName, table.ColumnName);

                foreach(string child in children)
                {
                    if (typeTree2.children == null)
                        typeTree2.children = new List<TreeJason>();

                    TypeTree tr = new TypeTree();
                   
                    tr.tableName = table.TableName;
                    tr.columnName = table.ColumnName;
                    
                   

                    TreeJason tr2 = new TreeJason();
                    tr2.label = child;
                    tr2.children = null;
                    tr2.data = tr;
                    tr2.leaf = true;
                    tr2.selectable = true;
                    tr2.parent = new TreeJason();
                    tr2.parent.label = typeTree2.label;
                    tr2.parent.data = typeTree2.data;
                    tr2.parent.children = null;

                    typeTree2.children.Add(tr2);
                }

                typeTree2.parent = new TreeJason();
                typeTree2.parent.label = this.returnTree.label;
                typeTree2.parent.data = this.returnTree.data;
                typeTree2.parent.children = null;

                this.returnTree.children.Add(typeTree2);
            }

            
        }

        public NeTypesData(string ModelName)
        {
            //int id = 2;
            Init();
            List<int> categoriesIds = OracleHelper.GetCategoryIds(ModelName);

            foreach (int CategoryId in categoriesIds)
            {

                tables = OracleHelper.GetNeTables(CategoryId);

                if (tables != null)
                {
                    foreach (NeTypeModelView table in tables)
                    {
                        if (this.returnTree.children == null)
                            this.returnTree.children = new List<TreeJason>();

                        //TypeTree typeTree = new TypeTree();
                        //typeTree.id = id++;
                        //typeTree.name = table.DisplayName;

                        TreeJason typeTree1 = new TreeJason();
                        typeTree1.label = table.DisplayName;
                        typeTree1.selectable = true;

                        List<string> children = OracleHelper.GetNeTypeValues(table.TableName, table.ColumnName);

                        foreach (string child in children)
                        {
                            if (typeTree1.children == null)
                                typeTree1.children = new List<TreeJason>();

                            TypeTree tr = new TypeTree();
                            //tr.id = id++;
                            tr.tableName = table.TableName;
                            tr.columnName = table.ColumnName;
                            
                            //tr.name = child;
                            //tr.children = null;

                            TreeJason tr2 = new TreeJason();
                            tr2.label = child;
                            tr2.data = tr;
                            tr2.children = null;
                            tr2.leaf = true;
                            tr2.selectable = true;
                            tr2.parent = new TreeJason();
                            tr2.parent.label = typeTree1.label;
                            tr2.parent.data = typeTree1.data;
                            tr2.parent.children = null;

                            typeTree1.children.Add(tr2);
                        }

                        typeTree1.parent = new TreeJason();
                        typeTree1.parent.label = this.returnTree.label;
                        typeTree1.parent.data = this.returnTree.data;
                        typeTree1.parent.children = null;

                        this.returnTree.children.Add(typeTree1);
                    }
                }


            }


        }

        public void Init()
        {
            TypeTree data=new TypeTree();
            data.columnName = null;
            data.tableName = null;
            
            //this.Data = new TypeTree();
            //this.Data.name = "Types";
            //this.Data.id = 1;
            //this.Data.columnName = null;
            //this.Data.tableName = null;
            //this.Data.children = null;

            this.returnTree = new TreeJason();
            this.returnTree.label = "Types";
            this.returnTree.data = data;
            this.returnTree.selectable = true;
        }
    }
}