using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Helper
{
    public static class Extenstion
    {
        
        public static IEnumerable<T> SerializeType<T>(this DataTable datatable) where T: class, new()
        {
            List<T> list = new List<T>();

            PropertyInfo[] info = typeof(T).GetProperties();


            foreach (DataRow _row in datatable.Rows)
            {
                var t = new T();
                for (int i = 0; i < info.Length; i++)
                {
                    if (_row[i] == null || _row[i]== DBNull.Value)
                    {
                        info[i].SetValue(t, DBNull.Value.ToString());
                    }
                    else
                    info[i].SetValue(t, _row[i]);
                }
                list.Add(t);
            }
            return list;
        }
    }
}