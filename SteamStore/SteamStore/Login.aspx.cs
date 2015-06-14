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
    /// <summary>
    /// Login form
    /// </summary>
    public partial class Login : Page
    {
        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] != null)
            {
                Session.Clear();//remove all data from session if loggout
                Session["loggedIn"] = false;
                if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))//if a return page is specified then goto that page
                {
                    Response.Redirect(Server.UrlDecode(Request.QueryString["returnUrl"]));
                }
            }
        }

        /// <summary>
        /// When the user clicks on submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void submit_Click(object sender, EventArgs e)
        {
            //encrypt the password
            var user = username.Text;
            var pass = password.Text;
            var sha = SHA256.Create();
            var encrypted = sha.ComputeHash(Encoding.ASCII.GetBytes(pass));
            var con = DbProvider.GetOracleConnection();
            var com = con.CreateCommand();
            com.CommandText = "SELECT count(*) FROM steam_user WHERE Username = :usr AND passwordHash = :pass";
            
            //create parameters
            var pUser = com.CreateParameter();
            pUser.DbType= DbType.String;
            pUser.Value = user;
            pUser.ParameterName = "usr";
            pUser.Direction= ParameterDirection.Input;

            var pPass = com.CreateParameter();
            pPass.DbType = DbType.String;
            pPass.Value = BitConverter.ToString(encrypted).Replace("-",string.Empty);
            pPass.ParameterName = "pass";
            pPass.Direction = ParameterDirection.Input;

            com.Parameters.Add(pUser);
            com.Parameters.Add(pPass);
            
            //check if the user can login
            var cnt = (decimal)com.ExecuteScalar();
            if (cnt >= 1)
            {
                //login
                Session["loggedIn"] = true;
                Session["username"] = user;
                lblPassword.Text = "loggedIn!";
                ((Site1) Master).UpdateLoginLabel();
                if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))
                {
                    //go to returnrl
                    Response.Redirect(Server.UrlDecode(Request.QueryString["returnUrl"]));
                }
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