using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// dbMgr 的摘要描述
/// </summary>
public class dbMgrLab
{
    private SqlConnection _sqlConn = null;
    private List<int> _runDefaultRank = new List<int>(new int[] {
        1000
        ,900
        ,800
        ,700
        ,600
        ,500
        ,400
        ,300
        ,200
        ,100
        }
    );
    public dbMgrLab()
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
    public void addRunData(string guid)
    {
        using (SqlCommand cmd = new SqlCommand("addRunData", _sqlConn))
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

    public cityDisplayData addCityData(string guid, string nick, int rank)
    {
        cityDisplayData c = new cityDisplayData();
        using (SqlCommand cmd = new SqlCommand("addCityData", _sqlConn))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@nick", SqlDbType.NChar).Value = nick;
            cmd.Parameters.Add("@rank", SqlDbType.Int).Value = rank;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    data.Read();

                    c.nickName = nick;
                    c.rank = Convert.ToInt32(data["r"]);
                    c.gender = Convert.ToInt32(data["gender"]);
                    c.group = Convert.ToInt32(data["cGroup"]);

                }
            }
            catch (Exception ex)
            {
                c = null;
                //throw ex.GetBaseException();
            }
            finally
            {
                _sqlConn.Close();
                cmd.Dispose();

            }
            return c;
        }
    }

    public bool addMobileData(string guid)
    {
        int result = 0;
        using (SqlCommand cmd = new SqlCommand("addMobileData", _sqlConn))
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
        return result == 1;
    }

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
    #endregion

    #region Update
    public void updateRunData(string guid, runData data)
    {
        using (SqlCommand cmd = new SqlCommand("updateRunData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@gender", SqlDbType.TinyInt).Value = data.gender;
            cmd.Parameters.Add("@group", SqlDbType.TinyInt).Value = data.group;
            cmd.Parameters.Add("@carType", SqlDbType.TinyInt).Value = data.carType;
            cmd.Parameters.Add("@area", SqlDbType.TinyInt).Value = data.area;
            cmd.Parameters.Add("@coin", SqlDbType.SmallInt).Value = data.coin;
            cmd.Parameters.Add("@dist", SqlDbType.SmallInt).Value = data.dist;
            cmd.Parameters.Add("@runTime", SqlDbType.Float).Value = data.runTime;
            cmd.Parameters.Add("@score", SqlDbType.SmallInt).Value = data.score;
            cmd.Parameters.Add("@umbrella", SqlDbType.TinyInt).Value = data.umbrella;
            cmd.Parameters.Add("@sItem", SqlDbType.TinyInt).Value = data.specialItem;

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

    public void updateMobileData(string guid, mobileData data)
    {
        using (SqlCommand cmd = new SqlCommand("updateMobileData", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@nick", SqlDbType.NChar).Value = data.nick;
            cmd.Parameters.Add("@gender", SqlDbType.TinyInt).Value = data.gender;
            cmd.Parameters.Add("@careerType", SqlDbType.TinyInt).Value = data.careerType;
            cmd.Parameters.Add("@headType", SqlDbType.TinyInt).Value = data.headType;
            cmd.Parameters.Add("@footType", SqlDbType.TinyInt).Value = data.footType;

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

    public void updateShare(string guid, int isFB, int isLine)
    {
        using (SqlCommand cmd = new SqlCommand("setShare", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uKey", SqlDbType.NChar).Value = guid;
            cmd.Parameters.Add("@isFB", SqlDbType.Bit).Value = isFB;
            cmd.Parameters.Add("@isLine", SqlDbType.Bit).Value = isLine;

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
    public void getRunRank(ref List<runRankData> rankData)
    {
        using (SqlCommand cmd = new SqlCommand("getRunRank", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {

                    rankData.Clear();
                    while (data.Read())
                    {
                        if (data[0].Equals(DBNull.Value))
                        {
                            break;
                        }

                        runRankData r = new runRankData();
                        r.guid = data["uKey"].ToString();
                        r.guid = r.guid.Replace(" ", string.Empty);
                        r.score = Convert.ToInt32(data["score"]);
                        r.carType = Convert.ToInt32(data["carType"]);
                        r.group = Convert.ToInt32(data["cGroup"]);
                        r.gender = Convert.ToInt32(data["gender"]);
                        r.rank = getRank(r.score);

                        if (r.rank != -1)
                        {
                            rankData.Add(r);
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

    public void getUserRank(string guid, ref int rank, ref int score)
    {
        if (guid == "")
        {
            return;
        }
        rank = -1;
        score = 0;
        using (SqlCommand cmd = new SqlCommand("getUserRank", _sqlConn))
        {
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
                        if (!data[0].Equals(DBNull.Value))
                        {
                            rank = Convert.ToInt32(data["rank"]);
                            score = Convert.ToInt32(data["score"]);

                            int rankInDefault = getRank(score);

                            if (rankInDefault == -1)
                            {
                                rank += 10;
                            }
                            else
                            {
                                rank += (rankInDefault - 1);
                            }
                        }
                    }
                }
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

    public void getCityDisplay(ref List<cityDisplayData> cityDataList)
    {
        using (SqlCommand cmd = new SqlCommand("getCityDisplay", _sqlConn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@store", SqlDbType.TinyInt).Value = 1;
            try
            {
                _sqlConn.Open();
                using (var data = cmd.ExecuteReader())
                {
                    cityDataList.Clear();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            if (data[0].Equals(DBNull.Value))
                            {
                                break;
                            }

                            cityDisplayData c = new cityDisplayData();
                            c.nickName = data["nick"].ToString();
                            c.nickName = c.nickName.Replace(" ", string.Empty);
                            c.rank = Convert.ToInt32(data["rank"]);
                            c.gender = Convert.ToInt32(data["gender"]);
                            c.group = Convert.ToInt32(data["cGroup"]);
                            c.score = Convert.ToInt32(data["score"]);
                            cityDataList.Add(c);
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

    public void getNewCityUser(ref cityDisplayData cityData)
    {
        using (SqlCommand cmd = new SqlCommand("getNewCityUser", _sqlConn))
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
                        cityData.nickName = data["nick"].ToString();
                        cityData.nickName = cityData.nickName.Replace(" ", string.Empty);
                        cityData.rank = Convert.ToInt32(data["rank"]);
                        cityData.gender = Convert.ToInt32(data["gender"]);
                        cityData.group = Convert.ToInt32(data["cGroup"]);
                        cityData.score = Convert.ToInt32(data["score"]);
                    }
                    else
                    {
                        cityData = null;
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

    public runData getUserRunData(string guid)
    {
        if (guid == "")
        {
            return null;
        }

        runData rData = new runData();
        using (SqlCommand cmd = new SqlCommand("getUserRunData", _sqlConn))
        {
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
                        rData.coin = Convert.ToInt32(data["coin"]);
                        rData.dist = Convert.ToInt32(data["dist"]);
                        rData.specialItem = Convert.ToInt32(data["specialItem"]);
                        rData.umbrella = Convert.ToInt32(data["umbrella"]);
                        rData.runTime = (float)Convert.ToDouble(data["runTime"]);
                        rData.carType = Convert.ToInt32(data["carType"]); //TODO;

                        rData.score = rData.coin * 10 + rData.dist; //TODO
                    }
                    else
                    {
                        rData = null;
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
        return rData;
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
    
    #region Private Method
    private int getRank(int score)
    {
        int rank = -1;
        for (int i = 0; i < _runDefaultRank.Count(); i++)
        {
            if (_runDefaultRank[i] < score)
            {
                _runDefaultRank[i] = score;
                rank = i + 1;
                break;
            }
        }
        return rank;
    }
    #endregion
}