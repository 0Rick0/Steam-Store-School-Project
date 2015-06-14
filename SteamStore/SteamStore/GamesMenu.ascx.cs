using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamStore
{
    public partial class GamesMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadListItems();
            }
        }

        private void LoadListItems()
        {
            //create a list and add a allgames item
            var cats = new List<ListItem>();
            cats.Add(new ListItem(){Id = 1, Text = "All Games", IsTop = true});

            //connect to the database
            using (var con = DbProvider.GetOracleConnection())
            {
                
                var com = con.CreateCommand();
                /*
                 Get all categories with id and super categorie
                 SELECT categorieId, superCategorie, categorieName
                 FROM categories
                 WHERE supercategorie IS NOT NULL;
                 */
                com.CommandText = "SELECT categorieId, superCategorie, categorieName FROM categorie WHERE supercategorie IS NOT NULL";
                var r = com.ExecuteReader();
                while (r.Read())
                {
                    //add each item to list
                    var i = new ListItem { Id = (int)r["categorieId"], Text = (string)r["categorieName"], IsTop = true};
                    
                    //if it's supercategorie is 1 then it is a top element
                    if ((int) r["superCategorie"] != 1)
                    {
                        cats.First(it=>it.Id==(int)r["superCategorie"]).Children.Add(i);
                        i.IsTop = false;
                    }
                    cats.Add(i);
                }
                //close everything
                r.Close();
                r.Dispose();
                com.Dispose();
            }
            //put everything in the html, call to string on only the top elements
            //every item on a new line
            topUl.InnerHtml = string.Join("\n", cats.Where(i => i.IsTop).Select(i => i.ToString()));
        }
        
    }

    class ListItem
    {
        public List<ListItem> Children;
        public String Text;
        public int Id;
        public bool IsTop;

        public ListItem()
        {
            Children=new List<ListItem>();
        }

        public override string ToString()
        {
            /*
             Create html in this format
             * <li>
             * (title)
             * [<ul>
             *  (subitems.tostring()<-same as this method)
             * </ul>]
             * </li>
             */

            var sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(string.Format("<a href=\"index.aspx?id={0}\">{1}</a>",Id,Text));
            if (Children.Count > 0)
            {
                sb.Append("\n<ul>");
                foreach (var listItem in Children)
                {
                    sb.Append(listItem.ToString()+"\n");
                }
                sb.Append("</ul>");
            }
            sb.Append("</li>\n");
            return sb.ToString();
        }
    }
}