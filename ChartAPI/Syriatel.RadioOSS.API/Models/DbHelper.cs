using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Syriatel.RadioOSS.API.Models
{
    public class DbHelper
    {
        OracleConnection _conn;

        private const string _connectionString = @"Data Source=10.253.23.164/testDB; User Id=oss_test;Password=oss_test_123;";
        public DbHelper()
        {
            _conn = new OracleConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if (_conn.State == ConnectionState.Closed || _conn.State == ConnectionState.Broken)
                _conn.Open();
        }
        public void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }

        public DataTable ExcuteQuery(string Query)
        {
            DataTable dt = new DataTable();
            if (Query == string.Empty)
                throw new NullReferenceException("Query Not faild for excute in database ");
            else
            {

                OracleDataAdapter dpt = new OracleDataAdapter(Query, _conn);
                try
                {
                    OpenConnection();
                    dpt.Fill(dt);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConnection();
                }

            }
            return dt;

        }

        

    }
}