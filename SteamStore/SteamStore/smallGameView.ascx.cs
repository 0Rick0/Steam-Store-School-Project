using System;

namespace SteamStore
{
    /// <summary>
    /// DIV with picture and name
    /// </summary>
    public partial class smallGameView : System.Web.UI.UserControl
    {
        /// <summary>
        /// The title of the game
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the id of the image
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// the id of the game
        /// </summary>
        public string GameId { get; set; }

        /// <summary>
        /// When the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        /// <summary>
        /// load the data into the control
        /// </summary>
        public void LoadData()
        {
            gameContainerImage.Src = string.Format("/images/games/{0}.png",ImageId);
            gameContainerTitle.InnerText = Title;
            gameContainerImage.Alt = Title;
            gameContainerLink.HRef = "game.aspx?appId="+GameId;
        }
    }
}