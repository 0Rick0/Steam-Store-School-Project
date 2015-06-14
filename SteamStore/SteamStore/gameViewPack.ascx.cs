using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    /// <summary>
    /// Container for a pack
    /// </summary>
    public partial class gameViewPack : System.Web.UI.UserControl
    {
        /// <summary>
        /// The title of the pack
        /// </summary>
        public string PackTitle { get; set; }

        /// <summary>
        /// The games in the pack
        /// </summary>
        public string PackGames { get; set; }

        /// <summary>
        /// The discount in percentage, "" if no discount
        /// </summary>
        public string Discount { get; set; }

        /// <summary>
        /// The old price without discount, "" if no discount
        /// </summary>
        public string OldPrice { get; set; }

        /// <summary>
        /// The new price with discount
        /// </summary>
        public string NewPrice { get; set; }

        /// <summary>
        /// The id of the pack
        /// </summary>
        public int PackId { get; set; }

        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}