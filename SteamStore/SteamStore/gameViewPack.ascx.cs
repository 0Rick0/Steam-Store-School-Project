using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class gameViewPack : System.Web.UI.UserControl
    {
        public string PackTitle { get; set; }
        public String PackGames { get; set; }
        public String Discount { get; set; }
        public String OldPrice { get; set; }
        public String NewPrice { get; set; }
        public int PackId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}