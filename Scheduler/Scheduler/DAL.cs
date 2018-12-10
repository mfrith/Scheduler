using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
  public class DAL
  {
    public static int ExecuteQuery(string query)
    {
      int rowCount = 0;
      SqlConnection conn = new SqlConnection();
      SqlCommand sqlcommand = new SqlCommand();
      conn.ConnectionString = @"Server=.\SQLEXPRESS;Database=Scheduler;Integrated Security=true;";
      try
      {
        sqlcommand.CommandText = query;
        sqlcommand.CommandType = System.Data.CommandType.Text;
        sqlcommand.Connection = conn;

        conn.Open();

        rowCount = sqlcommand.ExecuteNonQuery();

        conn.Close();
      }
      catch (Exception ex)
      {
        conn.Close();
        throw ex;
      }
      return rowCount;
    }
  }
}
