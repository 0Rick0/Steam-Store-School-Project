using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateLoginLabel();
        }

        public void UpdateLoginLabel()
        {
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