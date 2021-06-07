using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.DynamicReport;
using Syriatel.OSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Syriatel.OSS.API.Models.DynamicJoinService
{
    public class DynamicJoinCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        private List<ReportLevelsModelView> Levels { get; set; }

        private GraphTraversal graphTraversal { get; set; }

        public DynamicJoinReturnedData Data { get; set; }

        public DynamicJoinCreation(DynamicJoinData response)
        {
            int classificationId = OracleHelper.GetClassificationID(response.ClassificationName.ToLower());

            if (classificationId != 0)
            {
                this.Levels = OracleHelper.GetCategoryLevelsAllData(classificationId);
            }

            if (this.Levels != null && response.EfectedTabels != null)
            {

                string JoinType = GetJoinType(response.JoinType);

                if (JoinType != null)
                {
                    this.graphTraversal = new GraphTraversal(this.Levels, JoinType);
                    this.graphTraversal.AddTablesToGraph(response.EfectedTabels);
                    string join = "";
                    List<string> junctionTables = this.graphTraversal.GenerateJoinClause(ref join);

                    this.Data = new DynamicJoinReturnedData();
                    this.Data.JoinClause = join;
                    this.Data.WhereClause = GenerateWhereClause(junctionTables);
                }
            }
        }

        private string GenerateWhereClause(List<string> junctionTables)
        {
            string whereClause = "";
            bool firstTime = true;

            if(junctionTables != null && junctionTables.Count > 0)
            {
                foreach(string table in junctionTables)
                {
                    if(firstTime)
                    {
                        whereClause += (" " + table + ".RETIRE_DATE IS NULL ");
                        firstTime = false;
                    }
                    else
                        whereClause += (" AND " + table + ".RETIRE_DATE IS NULL ");
                }
            }

            return whereClause;
        }

        private string GetJoinType(string join)
        {
            if (join == null)
                return null;

            string InnerJoin = Constants.JoinType.INNER_JOIN;
            string LeftOuterJoin = Constants.JoinType.LEFT_OUTER_JOIN;

            if (InnerJoin.Contains(join.ToUpper()) || Regex.Replace(InnerJoin, @"\s+", "").Contains(join.ToUpper()))
            {
                return Constants.JoinType.INNER_JOIN;
            }
            else if (LeftOuterJoin.Contains(join.ToUpper()) || Regex.Replace(LeftOuterJoin, @"\s+", "").Contains(join.ToUpper()))
            {
                return Constants.JoinType.LEFT_OUTER_JOIN;
            }

            return null;
        }

    }
}