using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SteamStore.styles
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
            var cats = new List<ListItem>();

            using (var con = OracleClientFactory.Instance.CreateConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();

                var com = con.CreateCommand();
                com.CommandText = "SELECT categorieId, superCategorie, categorieName FROM categorie WHERE supercategorie IS NOT NULL";
                var r = com.ExecuteReader();
                while (r.Read())
                {
                    //add each item to list
                    var i = new ListItem { Id = (int)r["categorieId"], Text = (string)r["categorieName"], IsTop = true};
                    
                    if ((int) r["superCategorie"] != 1)
                    {
                        cats.First(it=>it.Id==(int)r["superCategorie"]).Children.Add(i);
                        i.IsTop = false;
                    }
                    cats.Add(i);
                }
                r.Close();
                r.Dispose();
                com.Dispose();
                topUl.InnerHtml = string.Join("\n", cats.Where(i => i.IsTop).Select(i => i.ToString()));
            }
            /*
            var lis = new List<ListItem>();
            lis.Add(new ListItem(){Id = 0,Text="Test",Children = new List<ListItem>()
            {
                new ListItem(){Id = 1,Text = "Test"},
                new ListItem(){Id = 2,Text = "Test2", Children = new List<ListItem>()
                {
                    new ListItem(){Id = 3, Text = "Test3"},
                    new ListItem(){Id = 4, Text = "Test4", Children = new List<ListItem>()
                    {
                        new ListItem(){Id = 5, Text = "Test5"},
                        new ListItem(){Id = 6, Text = "Test6"}
                    }}
                }}
            }});
            lis.Add(new ListItem(){Id = 7, Text = "Test7"});
            lis.Add(new ListItem(){Id = 8, Text = "Test8"});
            topUl.InnerHtml = string.Join("", lis.Select(i => i.ToString()));
            */
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
            var sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(string.Format("<a href=\"?id={0}\">{1}</a>",Id,Text));
            if (Children.Count > 0)
            {
                sb.Append("<ul>");
                foreach (var listItem in Children)
                {
                    sb.Append(listItem.ToString());
                }
                sb.Append("</ul>");
            }
            sb.Append("</li>");
            return sb.ToString();
        }
    }
}