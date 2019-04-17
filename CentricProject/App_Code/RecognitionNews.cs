using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CentricProject.Models;

namespace CentricProject.App_Code
{
    public class RecognitionNews
    {
        public List<int> getRecentRecs()
        {
            List<int> recentRecs = new List<int>();
            string dbConnection = "DefaultConnection";
            string query = "SELECT TOP (3) recognitionId FROM [RecognitionModels] ORDER BY createDate DESC";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    recentRecs.Add(Convert.ToInt32(dbReader["recognitionId"]));
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
            return recentRecs;
        }

        public string recDetailsRecognizer(int recognitionId)
        {
            string strReturn = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT recognizerId FROM [RecognitionModels] WHERE recognitionId=@recognitionId";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            queryCommand.Parameters.Add("@recognitionId", SqlDbType.Int).Value = recognitionId;
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    strReturn = getName(Convert.ToInt32(dbReader["recognizerId"]));
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

        public string recDetailsRecognized(int recognitionId)
        {
            string strReturn = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT recognizedId FROM [RecognitionModels] WHERE recognitionId=@recognitionId";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            queryCommand.Parameters.Add("@recognitionId", SqlDbType.Int).Value = recognitionId;
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    strReturn = getName(Convert.ToInt32(dbReader["recognizedId"]));
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

        public string recDetailsCoreValue(int recognitionId)
        {
            string strReturn = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT coreValue FROM [RecognitionModels] WHERE recognitionId=@recognitionId";
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.
            ConnectionStrings[dbConnection].ToString());
            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            queryCommand.Parameters.Add("@recognitionId", SqlDbType.Int).Value = recognitionId;
            try
            {
                sqlConnection.Open();
                SqlDataReader dbReader = queryCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    CoreValues coreValue = (CoreValues)(Convert.ToInt32(dbReader["coreValue"]));
                    strReturn = coreValue.ToString();
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
        private string getName(int id)
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


    }
}