using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    /// <summary>
    /// Master page
    /// </summary>
    public partial class Site1 : System.Web.UI.MasterPage
    {
        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateLoginLabel();
        }

        /// <summary>
        /// method to update the logon label
        /// </summary>
        public void UpdateLoginLabel()
        {
            //check if the use is logged in and display login or logout+username
            if (Session["loggedin"] is bool && (bool)Session["loggedIn"])
            {
                loginBar.InnerHtml = string.Format("<a href=\"/login.aspx?logout=true&returnUrl="+Server.UrlEncode(Request.Url.ToString())+"\">logout</a> - <a href=\"/profile.aspx\">{0}</a>", Session["username"]);
            }
            else
            {
                loginBar.InnerHtml = "<a href=\"/Login.aspx?returnUrl="+Server.UrlEncode(Request.Url.ToString())+"\">LOGIN</a>";
            }
        }
    }
}