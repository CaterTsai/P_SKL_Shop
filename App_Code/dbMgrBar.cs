using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// dbMgr 的摘要描述
/// </summary>
public class dbMgrBar
{
    private SqlConnection _sqlConn = null;

    public dbMgrBar()
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
    public void addBarData(string guid)
    {
        using (SqlCommand cmd = new SqlCommand("addBarData", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
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
    
    public void addBarMobile(string guid, string name, string phone)
    {
        using (SqlCommand cmd = new SqlCommand("addBarMobileData", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            cmd.Parameters.Add("@name", SqlDbType.NChar).Value = name;
            cmd.Parameters.Add("@phone", SqlDbType.NChar).Value = phone;
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
    #endregion

    #region Update
    public void updateBarData(string guid, barData data)
    {   
        using (SqlCommand cmd = new SqlCommand("updateBarData", _sqlConn))
        {
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.ParseExact(data.ans1, format, provider);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@ans1", SqlDbType.Date).Value = date;
            cmd.Parameters.Add("@ans2", SqlDbType.TinyInt).Value = data.ans2;
            cmd.Parameters.Add("@ans3", SqlDbType.TinyInt).Value = data.ans3;
            cmd.Parameters.Add("@ans4", SqlDbType.TinyInt).Value = data.ans4;
            cmd.Parameters.Add("@ans5", SqlDbType.TinyInt).Value = data.ans5;

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
    
    public void setBarLiquorNickname(string guid, string nickname)
    {
        using (SqlCommand cmd = new SqlCommand("setBarLiquorNickname", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            cmd.Parameters.Add("@nickName", SqlDbType.NChar).Value = nickname;
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

    public int setBarLiquorLike(string guid)
    {
        int result = 0;
        using (SqlCommand cmd = new SqlCommand("addBarLike", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
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
        return result;
    }
    #endregion

    #region Get
    public void getBarLiquorDisplay(ref List<barData> barDataList)
    {
        using (SqlCommand cmd = new SqlCommand("getBarLiquorDisplay", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    barDataList.Clear();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            if (data[0].Equals(DBNull.Value))
                            {
                                break;
                            }

                            
                            barData c = new barData();
                            c.guid = data["uKey"].ToString();
                            c.guid = c.guid.Replace(" ", string.Empty);
                            c.nickName = data["nickName"].ToString();
                            c.nickName = c.nickName.Replace(" ", string.Empty);

                            DateTime date = DateTime.Parse(data["ans1"].ToString());
                            c.ans1 = date.ToString("yyyy-MM-dd");
                            c.ans2 = Convert.ToInt32(data["ans2"]);
                            c.ans3 = Convert.ToInt32(data["ans3"]);
                            c.ans4 = Convert.ToInt32(data["ans4"]);
                            c.ans5 = Convert.ToInt32(data["ans5"]);
                            
                            if(!data.IsDBNull(7))
                            {
                                c.likeCount = Convert.ToInt32(data["likeCount"]);
                            }
                            else
                            {
                                c.likeCount = 0;
                            }
                            
                            barDataList.Add(c);
                        }
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
    }

    public void getNewBarLiquor(ref barData bData)
    {
        using (SqlCommand cmd = new SqlCommand("getNewBarLiquor", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        data.Read();

                        bData.guid = data["uKey"].ToString();
                        bData.guid = bData.guid.Replace(" ", string.Empty);
                        bData.nickName = data["nickName"].ToString();
                        bData.nickName = bData.nickName.Replace(" ", string.Empty);

                        DateTime date = DateTime.Parse(data["ans1"].ToString());
                        bData.ans1 = date.ToString("yyyy-MM-dd");
                        bData.ans2 = Convert.ToInt32(data["ans2"]);
                        bData.ans3 = Convert.ToInt32(data["ans3"]);
                        bData.ans4 = Convert.ToInt32(data["ans4"]);
                        bData.ans5 = Convert.ToInt32(data["ans5"]);
                        bData.likeCount = Convert.ToInt32(data["likeCount"]);
                    }
                    else
                    {
                        bData = null;
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
    }

    public barData getBarLiquorData(string guid)
    {
        using (SqlCommand cmd = new SqlCommand("getBarLiquorData", _sqlConn))
        {
            barData bData = new barData() ;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        data.Read();
                        bData.nickName = data["nickName"].ToString();
                        bData.nickName = bData.nickName.Replace(" ", string.Empty);
                        bData.ans1 = data["ans1"].ToString();
                        bData.ans2 = Convert.ToInt32(data["ans2"]);
                        bData.ans3 = Convert.ToInt32(data["ans3"]);
                        bData.ans4 = Convert.ToInt32(data["ans4"]);
                        bData.ans5 = Convert.ToInt32(data["ans5"]);
                    }
                    else
                    {
                        bData = null;
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

            return bData;
        }
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

    
    #endregion

}