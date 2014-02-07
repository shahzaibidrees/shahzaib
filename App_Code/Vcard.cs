using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Mail;
using System.IO;
/// <summary>
/// Summary description for Vcard
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Vcard : System.Web.Services.WebService
{

    public Vcard()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class UserContacts
    {
        public int ContactId;
        public int UserId;
        public string Name;
        public string MobileNumber;
        public string EmailAddress;
    }

    public class UserChat
    {
        public int SenderId;
        public int ReceiverId;
        public string Mobile;
        public string Message;
        public string IsDeleted;
        public string IsreadFrom;
        public string IsreadTo;
    }
   

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string Signup(string Name, string Email, string Mobile, string Address, string Image)
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
                        myparams[7].Value = Image;
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

    [WebMethod]
    public string GetUserData(string Mobile)
    {
        string userdata = "";
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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            userdata += dt.Rows[i].Field<int>("ID") + ";" + dt.Rows[i].Field<string>("Name").ToString() + ";" + dt.Rows[i].Field<string>("Email") + ";" + dt.Rows[i].Field<string>("Mobile") + ";" + dt.Rows[i].Field<string>("Address") + ";" + dt.Rows[i].Field<string>("Image");

                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //return dt;
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
        return userdata;
    }

    [WebMethod]
    public string Email(string messagefrom, string messageto, string subject, string message)
    {
        string EmailStatus = "";
        MailMessage MyMailMessage = new MailMessage();
        MyMailMessage.From = new MailAddress(messagefrom,"2bvision Testing Mail Service.");
        MyMailMessage.To.Add(messageto);
        MyMailMessage.Subject = subject;
        MyMailMessage.IsBodyHtml = true;

       MyMailMessage.Body = message;

        SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
        SMTPServer.Port = 587;
        SMTPServer.Credentials = new System.Net.NetworkCredential("2bvisionnn@gmail.com", "mubbaram123456");
        SMTPServer.EnableSsl = true;
        try
        {
            SMTPServer.Send(MyMailMessage);
            if (EmailStatus == "Success")
            {
                
            //Response.Redirect("Thankyou.aspx");

            }
            else
            {
                //ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('" + EmailStatus + "');", true);
            }
        }
        catch (Exception ex)
        {


        }

        return EmailStatus;
        
    }

    [WebMethod]
    public void InsertUserContact(string Name, string MobileNumber,int UserId)
    {
        #region InsertContacts
        //String Result = "";
        try
        {
            SqlParameter[] myparams = null;
            using (SqlConnection Connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                Connection.Open();
                myparams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_usercontacts");
                using (SqlTransaction Transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        myparams[0].Value = "1";
                        myparams[7].Value = UserId;
                        myparams[3].Value = Name;
                        myparams[4].Value = MobileNumber;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_usercontacts", myparams);
                       // DataTable dtUserData = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_usercontacts", myparams).Tables[0];
                        //string userexist = dtUserData.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;

                       
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
        //return Result;


    }

    [WebMethod]
    public List<UserContacts> getUserContacts(int UserId)
    {
        #region getUserContacts
        DataTable dtUserContacts = new DataTable();
        List<UserContacts> listUserContacts = new List<UserContacts>();
        try
        {
            SqlParameter[] myparams = null;
            using (SqlConnection Connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                Connection.Open();
                myparams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_usercontacts");
                using (SqlTransaction Transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        myparams[0].Value = "3";
                        myparams[2].Value = UserId;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_usercontacts", myparams);
                        dtUserContacts = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_usercontacts", myparams).Tables[0];
                        //UserContacts = dtUserData.Rows[0][0].ToString;
                        Transaction.Commit();
                        //Result = true;
                        if (dtUserContacts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtUserContacts.Rows.Count; i++)
                            {
                                UserContacts objUserContacts = new Vcard.UserContacts();
                                objUserContacts.ContactId = Convert.ToInt32(dtUserContacts.Rows[i]["ContactId"]);
                                objUserContacts.UserId = Convert.ToInt32(dtUserContacts.Rows[i]["UserId"]);
                                objUserContacts.Name = Convert.ToString(dtUserContacts.Rows[i]["Name"]);
                                objUserContacts.EmailAddress = Convert.ToString(dtUserContacts.Rows[i]["EmailAddress"]);
                                objUserContacts.MobileNumber = Convert.ToString(dtUserContacts.Rows[i]["MobileNumber"]);
                                listUserContacts.Add(objUserContacts);
                            }
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
        return listUserContacts;


    }

    [WebMethod]
    public string getUserContactDetail(string ContactId)
    {
        string usercontactdetail = "";
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
                        myParams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_usercontacts");
                        myParams[0].Value = "4";
                        myParams[1].Value = ContactId;


                        dt = SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, "sp_usercontacts", myParams).Tables[0];
                        transaction.Commit();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            usercontactdetail += dt.Rows[i].Field<int>("ID").ToString() + ";" + dt.Rows[i].Field<string>("Name").ToString() + ";" + dt.Rows[i].Field<string>("Mobile") + ";" + dt.Rows[i].Field<string>("Email") + ";" + dt.Rows[i].Field<string>("Address") + ";";

                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //return dt;
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
        return usercontactdetail;
    }

    [WebMethod]
    public void updateUserName(string MobileNumber, String Name)
    {
        #region updateName
        //String Result = "";
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
                        myparams[0].Value = "5";
                        myparams[2].Value = Name;
                        myparams[4].Value = MobileNumber;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_user", myparams);
                        // DataTable dtUserData = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_usercontacts", myparams).Tables[0];
                        //string userexist = dtUserData.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;


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
        //return Result;


    }

    [WebMethod]
    public void updateUserEmail(string MobileNumber, String Email)
    {
        #region updateName
        //String Result = "";
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
                        myparams[0].Value = "6";
                        myparams[3].Value = Email;
                        myparams[4].Value = MobileNumber;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_user", myparams);
                        // DataTable dtUserData = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_usercontacts", myparams).Tables[0];
                        //string userexist = dtUserData.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;


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
        //return Result;


    }

    [WebMethod]
    public void updateUserAddress(string MobileNumber, String Address)
    {
        #region updateAddress
        //String Result = "";
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
                        myparams[0].Value = "7";
                        myparams[5].Value = Address;
                        myparams[4].Value = MobileNumber;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_user", myparams);
                        // DataTable dtUserData = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_usercontacts", myparams).Tables[0];
                        //string userexist = dtUserData.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;


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
        //return Result;


    }



    [WebMethod]
    public List<UserChat> getChatBySenderandReceiverId(string SenderId, string ReceiverId)
    {
        #region getChat
        DataTable dtgetChat = new DataTable();
        List<UserChat> listUserChat = new List<UserChat>();
        try
        {
            SqlParameter[] myparams = null;
            using (SqlConnection Connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                Connection.Open();
                myparams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Chat");
                using (SqlTransaction Transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        myparams[0].Value = "1";
                        myparams[2].Value = SenderId;
                        myparams[3].Value = ReceiverId;
                        
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_Chat", myparams);
                        dtgetChat = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_Chat", myparams).Tables[0];
                        //UserContacts = dtUserData.Rows[0][0].ToString;
                        Transaction.Commit();
                        //Result = true;
                        if (dtgetChat.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtgetChat.Rows.Count; i++)
                            {
                                UserChat objUserChat = new Vcard.UserChat();
                                objUserChat.SenderId = Convert.ToInt32(dtgetChat.Rows[i]["SenderId"]);
                                objUserChat.ReceiverId = Convert.ToInt32(dtgetChat.Rows[i]["ReceiverId"]);
                                objUserChat.Message = Convert.ToString(dtgetChat.Rows[i]["Message"]);
                                objUserChat.IsDeleted = Convert.ToString(dtgetChat.Rows[i]["IsDeleted"]);
                                objUserChat.IsreadFrom = Convert.ToString(dtgetChat.Rows[i]["IsreadFrom"]);
                                objUserChat.IsreadTo = Convert.ToString(dtgetChat.Rows[i]["IsreadTo"]);
                                objUserChat.Mobile = Convert.ToString(dtgetChat.Rows[i]["Mobile"]);

                                listUserChat.Add(objUserChat);
                            }
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
        return listUserChat;


    }

    [WebMethod]
    public void SendChatMessage(int SenderId, int ReceiverId, int ContactId, string Message, DateTime SendDate, DateTime ReceiveDate, int IsDeleted, string IsReadFrom, string IsReadTo)
    {
        #region SendChatMessage
        //String Messagesend = "";
        try
        {
            SqlParameter[] myparams = null;
            using (SqlConnection Connection = new SqlConnection(BVisionConfigurationManager.GetConnectionString()))
            {
                Connection.Open();
                myparams = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Chat");
                using (SqlTransaction Transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        myparams[0].Value = "2";
                        // myparams[1].Value = GUID;
                        myparams[2].Value = SenderId;
                        myparams[3].Value = ReceiverId;
                        myparams[4].Value = ContactId;
                        myparams[5].Value = Message;
                        myparams[6].Value = SendDate;
                        myparams[7].Value = ReceiveDate;
                        myparams[8].Value = IsDeleted;
                        myparams[9].Value = IsReadFrom;
                        myparams[10].Value = IsReadTo;
                        SqlHelper.ExecuteNonQuery(BVisionConfigurationManager.GetConnectionString(), "sp_Chat", myparams);
                       // DataTable dtUserChat = SqlHelper.ExecuteDataset(Transaction, CommandType.StoredProcedure, "sp_Chat", myparams).Tables[0];
                       // string userexist = dtUserChat.Rows[0][0].ToString();
                        Transaction.Commit();
                        //Result = true;


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
        //return Result;


    }

}