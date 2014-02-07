using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try 
        {
            users.Userinfo(Convert.ToString(txtName.Text.Trim()), Convert.ToString(txtEmail.Text.Trim()), Convert.ToString(txtMobile.Text.Trim()), Convert.ToString(txtAddress.Text.Trim()));
            Label1.Text = users.Userinfo(Convert.ToString(txtName.Text.Trim()), Convert.ToString(txtEmail.Text.Trim()), Convert.ToString(txtMobile.Text.Trim()), Convert.ToString(txtAddress.Text.Trim()));
        }
        catch(Exception  ex)
        {

        }
    }
   
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        try
        {
          // if (users.GetUserData(txtEnterMobile.Text))
            gvUserProfile.DataSource = users.GetUserData(txtEnterMobile.Text.Trim());
            gvUserProfile.DataBind();


        }
        catch
        {
 
        }
    }
}