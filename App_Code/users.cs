using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for users
/// </summary>
public class users
{
    public users()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string Userinfo(string Name, string Email, string Mobile, string Address)
    {
        #region InsertUser
        String Result = "";
        try
        {
            SqlParameter[] myparams = null;
            using (SqlConnection Connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                Connection.Open();
                myparams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_user");
                using (SqlTransaction Transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        myparams[0].Value = "1";
                        // myparams[1].Value = GUID;
                        myparams[2].Value = Name;
                        myparams[3].Value = Email;
                        myparams[4].Value = Mobile;
                        myparams[5].Value = Address;
                        // myparams[6].Value = IsActive;
                        // SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_user", myparams);
                        DataTable dtUserData = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_user", myparams).Tables[0];
                        string userexist = dtUserData.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;

                        if (userexist == "Exist")
                        {
                            Result = "User already Exist";
                        }
                        else
                        {
                            Result = "User Added";
                        }

                    }
                    catch (Exception ex)
                    {
                        Transaction.Rollback();
                    }
                    if (Connection.State != ConnectionState.Closed)
                    {
                        Connection.Close();
                    }
                }
            }


        }
        catch (Exception ex)
        {

        }

        #endregion
        return Result;


    }

    public static DataTable GetUserData(string Mobile)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] myParams = null;
            using (SqlConnection connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {

                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_user");
                        myParams[0].Value = 3;
                        myParams[4].Value = Mobile;

                        dt = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_user", myParams).Tables[0];
                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return dt;
                        throw ex;
                    }
                }

                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}

                    