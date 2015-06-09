using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore.styles
{
    public partial class smallGameView : System.Web.UI.UserControl
    {
        public String Title { get; set; }
        public String ImageId { get; set; }
        public String GameId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadData()
        {
            gameContainerImage.Src = string.Format("/images/games/{0}.png",ImageId);
            gameContainerTitle.InnerText = Title;
            gameContainerImage.Alt = Title;
            gameContainerLink.HRef = "?id="+GameId;
        }
    }
}