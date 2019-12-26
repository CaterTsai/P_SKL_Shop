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
    public void addConfigData(configData data, int store)
    {
        using (SqlCommand cmd = new SqlCommand("addConfigData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.TinyInt).Value = data.id;
            cmd.Parameters.Add("@value_1", SqlDbType.Int).Value = data.value_1;
            cmd.Parameters.Add("@value_2", SqlDbType.Float).Value = data.value_2;
            cmd.Parameters.Add("@value_3", SqlDbType.NChar).Value = data.value_3;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;
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

    public void addBarQuestion(ref barQuestion qData)
    {
        using (SqlCommand cmd = new SqlCommand("addQuestion", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@question", SqlDbType.NVarChar).Value = qData.question;
            cmd.Parameters.Add("@opt1", SqlDbType.NVarChar).Value = qData.opt1;
            cmd.Parameters.Add("@opt2", SqlDbType.NVarChar).Value = qData.opt2;
            cmd.Parameters.Add("@opt3", SqlDbType.NVarChar).Value = qData.opt3;
            cmd.Parameters.Add("@opt4", SqlDbType.NVarChar).Value = qData.opt4;
            cmd.Parameters.Add("@opt5", SqlDbType.NVarChar).Value = qData.opt5;
            cmd.Parameters.Add("@opt6", SqlDbType.NVarChar).Value = qData.opt6;

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

    public void addStoreData(storeData storeData)
    {
        using (SqlCommand cmd = new SqlCommand("addStoreData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@storeNo", SqlDbType.NChar).Value = storeData.storeNo;
            cmd.Parameters.Add("@storeName", SqlDbType.NChar).Value = storeData.storeName;
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
    public void updateConfigData(configData data, int store)
    {
        using (SqlCommand cmd = new SqlCommand("updateConfigData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.TinyInt).Value = data.id;
            cmd.Parameters.Add("@value_1", SqlDbType.Int).Value = data.value_1;
            cmd.Parameters.Add("@value_2", SqlDbType.Float).Value = data.value_2;
            cmd.Parameters.Add("@value_3", SqlDbType.NChar).Value = data.value_3;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;

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

    public void updateQuestion(ref barQuestion qData)
    {
        using (SqlCommand cmd = new SqlCommand("updateBarQuestion", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@question", SqlDbType.NVarChar).Value = qData.question;
            cmd.Parameters.Add("@opt1", SqlDbType.NVarChar).Value = qData.opt1;
            cmd.Parameters.Add("@opt2", SqlDbType.NVarChar).Value = qData.opt2;
            cmd.Parameters.Add("@opt3", SqlDbType.NVarChar).Value = qData.opt3;
            cmd.Parameters.Add("@opt4", SqlDbType.NVarChar).Value = qData.opt4;
            cmd.Parameters.Add("@opt5", SqlDbType.NVarChar).Value = qData.opt5;
            cmd.Parameters.Add("@opt6", SqlDbType.NVarChar).Value = qData.opt6;

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

    public void updateStoreData(storeData storeData)
    {
        using (SqlCommand cmd = new SqlCommand("updateStoreData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@storeId", SqlDbType.Int).Value = storeData.storeId;
            cmd.Parameters.Add("@storeNo", SqlDbType.NChar).Value = storeData.storeNo;
            cmd.Parameters.Add("@storeName", SqlDbType.NChar).Value = storeData.storeName;
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
    public configData getConfigData(int id, int store)
    {

        configData config = new configData();
        using (SqlCommand cmd = new SqlCommand("getConfig", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cid", SqlDbType.TinyInt).Value = id;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;
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

    public barQuestion getBarQuestion()
    {
        using (SqlCommand cmd = new SqlCommand("getQuestion", _sqlConn))
        {
            barQuestion qData = new barQuestion();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        data.Read();
                        qData.question = data["question"].ToString();
                        qData.question = qData.question.Replace(" ", string.Empty);
                        qData.question = qData.question.Replace("\r\n", string.Empty);
                        qData.opt1 = data["opt1"].ToString();
                        qData.opt1 = qData.opt1.Replace(" ", string.Empty);
                        qData.opt2 = data["opt2"].ToString();
                        qData.opt2 = qData.opt2.Replace(" ", string.Empty);
                        qData.opt3 = data["opt3"].ToString();
                        qData.opt3 = qData.opt3.Replace(" ", string.Empty);
                        qData.opt4 = data["opt4"].ToString();
                        qData.opt4 = qData.opt4.Replace(" ", string.Empty);
                        qData.opt5 = data["opt5"].ToString();
                        qData.opt5 = qData.opt5.Replace(" ", string.Empty);
                        qData.opt6 = data["opt6"].ToString();
                        qData.opt6 = qData.opt6.Replace(" ", string.Empty);
                    }
                    else
                    {
                        qData = null;
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

            return qData;
        }
    }

    public List<storeData> getStoreData()
    {
        List<storeData> storeDataList = new List<storeData>();
        using (SqlCommand cmd = new SqlCommand("getStoreData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    while(data.Read())
                    {
                        if (data[0].Equals(DBNull.Value))
                        {
                            break;
                        }

                        storeData sd = new storeData();
                        sd.storeId = Convert.ToInt32(data["storeId"]);
                        sd.storeName = data["storeName"].ToString();
                        sd.storeName = sd.storeName.Replace(" ", string.Empty);
                        sd.storeNo = data["storeNo"].ToString();
                        sd.storeNo = sd.storeNo.Replace(" ", string.Empty);

                        storeDataList.Add(sd);
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

            return storeDataList;
        }
    }

    
    #endregion

    #region Lab
    public void clearRun(int store)
    {
        using (SqlCommand cmd = new SqlCommand("clearRun", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;
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

    public void clearCity(int store)
    {
        using (SqlCommand cmd = new SqlCommand("clearCity", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;
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

    #region Bar
    public void clearBar(int store)
    {
        using (SqlCommand cmd = new SqlCommand("clearBar", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.Int).Value = store;
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