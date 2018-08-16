﻿using System;
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
                            c.nickName = data["nickName"].ToString();
                            c.nickName = c.nickName.Replace(" ", string.Empty);
                            c.ans1 = data["ans1"].ToString();
                            c.ans1 = c.ans1.Replace(" ", string.Empty);
                            c.ans2 = Convert.ToInt32(data["ans2"]);
                            c.ans3 = Convert.ToInt32(data["ans3"]);
                            c.ans4 = Convert.ToInt32(data["ans4"]);
                            c.ans5 = Convert.ToInt32(data["ans5"]);
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
                        bData.nickName = data["nickName"].ToString();
                        bData.nickName = bData.nickName.Replace(" ", string.Empty);
                        bData.ans1 = data["ans1"].ToString();
                        bData.ans1 = bData.ans1.Replace(" ", string.Empty);
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
        }
    }
    
    #endregion
    
}