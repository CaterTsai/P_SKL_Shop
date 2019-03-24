using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// dbMgr 的摘要描述
/// </summary>
public class dbMgrWall
{
    private SqlConnection _sqlConn = null;
    public dbMgrWall()
    {
    }

    public void connDB()
    {
        var host = System.Web.Configuration.WebConfigurationManager.AppSettings["dbHost"];
        var db = System.Web.Configuration.WebConfigurationManager.AppSettings["dbDatabase"];
        var user = System.Web.Configuration.WebConfigurationManager.AppSettings["dbUser"];
        var pw = System.Web.Configuration.WebConfigurationManager.AppSettings["dbPW"];
        string strConn = "server=" + host + ";database=" + db + ";uid=" + user + ";pwd=" + pw;

        _sqlConn = new SqlConnection(strConn);
    }

    public void addWallLog(int type)
    {
        using (SqlCommand cmd = new SqlCommand("addWallData", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = type;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            finally
            {
                _sqlConn.Close();
                cmd.Dispose();
            }
        }
    }

    public void addWallWebLog(int type)
    {
        using (SqlCommand cmd = new SqlCommand("addWallWebData", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@type", SqlDbType.TinyInt).Value = type;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            finally
            {
                _sqlConn.Close();
                cmd.Dispose();
            }
        }
    }
}