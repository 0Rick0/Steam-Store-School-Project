using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] != null)
            {
                Session.Clear();//remove all data from session if loggout
                Session["loggedIn"] = false;
                if (!string.IsNullOrEmpty(Request.QueryString["returnpage"]))//if a return page is specified then goto that page
                {
                    Response.Redirect(Request.QueryString["returnpage"]);
                }
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            var user = username.Text;
            var pass = password.Text;
            var sha = SHA256.Create();
            var encrypted = sha.ComputeHash(Encoding.ASCII.GetBytes(pass));
            var con = DbProvider.GetOracleConnection();
            var com = con.CreateCommand();
            com.CommandText = "SELECT count(*) FROM steam_user WHERE Username = :usr AND passwordHash = :pass";
            
            
            var pUser = com.CreateParameter();
            pUser.DbType= DbType.String;
            pUser.Value = user;
            pUser.ParameterName = "usr";
            pUser.Direction= ParameterDirection.Input;

            var pPass = com.CreateParameter();
            pPass.DbType = DbType.String;
            pPass.Value = BitConverter.ToString(encrypted).Replace("-","");
            pPass.ParameterName = "pass";
            pPass.Direction = ParameterDirection.Input;

            com.Parameters.Add(pUser);
            com.Parameters.Add(pPass);
            
            Debug.WriteLine(pPass.Value);
            var cnt = (decimal)com.ExecuteScalar();
            Debug.WriteLine(cnt);
            if (cnt >= 1)
            {
                //login
                Session["loggedIn"] = true;
                Session["username"] = user;
                lblPassword.Text = "loggedIn!";
                ((Site1) Master).UpdateLoginLabel();
            }
            else
            {
                //error
                Session.Clear();
                Session["loggedIn"] = false;
                lblPassword.Text = "not loggedIn!";
            }
            

        }
    }
}