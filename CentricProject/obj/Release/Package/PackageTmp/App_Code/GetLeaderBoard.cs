using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CentricProject.App_Code
{
    public class GetLeaderBoard
    {
        public List<int> getLeaders()
        {
            List<int> topLeaders = new List<int>();
            string dbConnection = "DefaultConnection";
            string query = "SELECT TOP (3) recognizedId, COUNT(DISTINCT recognitionId) AS NumOfRecs FROM [RecognitionModels] GROUP BY recognizedId ORDER BY NumOfRecs DESC";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    topLeaders.Add(Convert.ToInt32(dbReader["recognizedId"]));
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {
                // Do nothing
            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return topLeaders;
        }

        public List<int> getFullLeaderList()
        {
            List<int> topLeaders = new List<int>();
            string dbConnection = "DefaultConnection";
            string query = "SELECT recognizedId, COUNT(DISTINCT recognitionId) AS NumOfRecs FROM [RecognitionModels] GROUP BY recognizedId ORDER BY NumOfRecs DESC";
            int rank = 1;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    topLeaders.Add(Convert.ToInt32(dbReader["recognizedId"]));
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {
                // Do nothing
            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return topLeaders;
        }

        public string getName(int id)
        {
            string strReturn = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT firstName, lastName FROM [ProfileDetails] WHERE id=@id";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            queryCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    strReturn = dbReader["firstName"].ToString() + " " + dbReader["lastName"].ToString();
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {
                // Do nothing.
            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return strReturn;
        }

        public int getRecCount(int recognizedId)
        {
            int count = 0;
            string dbConnection = "DefaultConnection";
            string query = "SELECT COUNT(DISTINCT recognitionId) as NumOfRecs FROM [RecognitionModels] WHERE recognizedId=@recognizedId";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            queryCommand.Parameters.Add("@recognizedId", SqlDbType.Int).Value = recognizedId;
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    count = Convert.ToInt32(dbReader["NumOfRecs"]);
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {
                // Do nothing.
            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return count;
        }
    }
}