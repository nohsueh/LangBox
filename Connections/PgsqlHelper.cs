using System;
using System.Data;
using System.Diagnostics;
using Npgsql;


namespace LangBox.Connections
{
    internal class PgsqlHelper
    {
        private static string conn = @"Host=localhost;Port=5432;Username=postgres;Password=126652;Database=langbox_db;";

        #region 查询操作
        public static DataTable ExecuteQuery(string sql)
        {
            NpgsqlConnection sqlConn = new NpgsqlConnection(conn);
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter(sql, sqlConn))
                {
                    sqldap.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return dt;
        }

        #endregion
        #region 增删改操作
        public static int ExecuteNonQuery(string sql, params NpgsqlParameter[] npgsqlParameters)
        {
            NpgsqlConnection sqlConn = new NpgsqlConnection(conn);
            try
            {
                sqlConn.Open();
                using (NpgsqlCommand pgsqlCommand = new NpgsqlCommand(sql, sqlConn))
                {
                    foreach (NpgsqlParameter parm in npgsqlParameters)
                        pgsqlCommand.Parameters.Add(parm);
                    int r = pgsqlCommand.ExecuteNonQuery();  //执行查询并返回受影响的行数
                    sqlConn.Close();
                    return r; //r如果是>0操作成功！ 
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return -1;
            }

        }
        #endregion
    }
}