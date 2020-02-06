using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.DbLayer
{
    public class Helper
    {
        private const string _connectionString = @"DATA SOURCE=10.253.23.164:1521/testDB;PASSWORD=oss_test_123;PERSIST SECURITY INFO=True;USER ID=OSS_TEST";
        private OracleConnection _oracleConnection;

        public Helper()
        {
            _oracleConnection = new OracleConnection(_connectionString);
        }

        public DataTable ExcuteQuery(string sql)
        {
            if (sql == string.Empty)
                throw new NullReferenceException("this query not supported in oracle database !!");
            DataTable dt = new DataTable();
            OracleDataAdapter _adapter = new OracleDataAdapter(sql, _oracleConnection);
            try
            {
                _adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}