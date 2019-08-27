using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Survey
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onBtnLoginClick(object sender, EventArgs e)
        {
            // save user id to session
            Session["UserID"] = textBoxUserID.Text;
            Server.Transfer("Survey.aspx");
        }
    }
}