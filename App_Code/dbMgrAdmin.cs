using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// dbMgr 的摘要描述
/// </summary>
public class dbMgrAdmin
{
    private SqlConnection _sqlConn = null;
    public dbMgrAdmin()
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

    #region Add
    public void addConfigData(configData data)
    {
        using (SqlCommand cmd = new SqlCommand("addConfigData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.TinyInt).Value = data.id;
            cmd.Parameters.Add("@value_1", SqlDbType.Int).Value = data.value_1;
            cmd.Parameters.Add("@value_2", SqlDbType.Float).Value = data.value_2;
            cmd.Parameters.Add("@value_3", SqlDbType.NChar).Value = data.value_3;

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

    public void addAdmin(string uuid)
    {
        using (SqlCommand cmd = new SqlCommand("addAdmin", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uuid", SqlDbType.NChar).Value = uuid;
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
    #endregion

    #region Update
    public void updateConfigData(configData data)
    {
        using (SqlCommand cmd = new SqlCommand("updateConfigData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.TinyInt).Value = data.id;
            cmd.Parameters.Add("@value_1", SqlDbType.Int).Value = data.value_1;
            cmd.Parameters.Add("@value_2", SqlDbType.Float).Value = data.value_2;
            cmd.Parameters.Add("@value_3", SqlDbType.NChar).Value = data.value_3;

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
    #endregion

    #region Get
    public configData getConfigData(int id)
    {

        configData config = new configData();
        using (SqlCommand cmd = new SqlCommand("getConfig", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.TinyInt).Value = id;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        data.Read();
                        if (!data.IsDBNull(data.GetOrdinal("value_1")))
                        {
                            config.value_1 = Convert.ToInt32(data["value_1"]);
                        }

                        if (!data.IsDBNull(data.GetOrdinal("value_2")))
                        {
                            config.value_2 = (float)Convert.ToDouble(data["value_2"]);
                        }

                        if (!data.IsDBNull(data.GetOrdinal("value_3")))
                        {
                            config.value_3 = Convert.ToString(data["value_3"]);
                            config.value_3 = config.value_3.Replace(" ", string.Empty);
                        }
                    }
                    else
                    {
                        config = null;
                    }
                }
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
        return config;
    }

    public bool checkAdmin(string uuid)
    {
        int result = 0;
        using (SqlCommand cmd = new SqlCommand("checkAdmin", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uuid", SqlDbType.NChar).Value = uuid;
            SqlParameter ret = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                _sqlConn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(ret.Value);
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
        return result == 1;
    }
    #endregion

    #region Lab
    public void clearRun()
    {
        using (SqlCommand cmd = new SqlCommand("clearRun", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
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

    public void clearCity()
    {
        using (SqlCommand cmd = new SqlCommand("clearCity", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
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
    #endregion

}