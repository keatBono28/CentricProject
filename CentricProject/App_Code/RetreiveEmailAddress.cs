using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CentricProject.App_Code
{
    public class RetreiveEmailAddress
    {
        public string GetEmailAddress(int id)
        {
            string email = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT Email FROM [AspNetUsers] WHERE ProfileDetails_id = @id";
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
                    email = dbReader["Email"].ToString();
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {
                
            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return email;
        }
        public string GetPosition(int id)
        {
            string position = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT position FROM [ProfileDetails] WHERE id = @id";
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
                    position = dbReader["position"].ToString();
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {

            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return position;
        }

        public string GetUserId(int id)
        {
            string userId = "";
            string dbConnection = "DefaultConnection";
            string query = "SELECT Id FROM [AspNetUsers] WHERE ProfileDetails_id = @id";
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
                    userId = dbReader["Id"].ToString();
                }
                dbReader.Close();
            }
            catch (Exception exeception)
            {

            }
            finally
            {
                queryCommand.Dispose();
                sqlConnection.Close();
            }
            return userId;
        }

    }
}