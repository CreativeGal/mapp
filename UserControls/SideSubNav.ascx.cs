using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_SideSubNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SideSubNavLiteral1.Text = String.Format("<ul id=\"subnav-menu\">{0}</ul>", DisplaySiteMapLevelAsBulletedList());
    }

    private string DisplaySiteMapLevelAsBulletedList()
    {
        //Get the SiteMapDataSourceView from the SiteMapDataSource
        SiteMapDataSourceView siteMapView = (SiteMapDataSourceView)SiteMapDataSource1.GetView(String.Empty);

        //Get the SiteMapNodeCollection from the SiteMapDataSourceView
        SiteMapNodeCollection nodes = (SiteMapNodeCollection)siteMapView.Select(DataSourceSelectArguments.Empty);

        //Recurse through the SiteMapNodeCollection
        return GetSiteMapLevelAsBulletedList(nodes);
    }

    private string GetSiteMapLevelAsBulletedList(SiteMapNodeCollection nodes)
    {
        string output = String.Empty;

        foreach (SiteMapNode node in nodes)
        {
            if (node["target"] != null) //link opens a new window or tab in the browser
            {
                output += String.Format("<li><a href=\"{0}\" target=\"" + node["target"].ToString() + "\">{1}</a>", node.Url, node.Title);
            }
            else //link DOES NOT open a new window or tab in the browser
            {
                output += IsCurrentPage(node) ? String.Format("<li class=\"li-subnav-menu-current\"><a class=\"a-subnav-menu-current\" href=\"{0}\">{1}</a>", node.Url, node.Title) : String.Format("<li><a href=\"{0}\">{1}</a>", node.Url, node.Title);    

            }

            //Add any children levels, if needed (recursively)
            if (node.HasChildNodes)
            {
                  if (node == SiteMap.CurrentNode || SiteMap.CurrentNode.IsDescendantOf(node))
                {
                    output += String.Format("<ul>{0}</ul>", GetSiteMapLevelAsBulletedList(node.ChildNodes));
                }
            }

            output += "</li>";
        }
        return output;
    }

    private bool IsCurrentPage(SiteMapNode node)
    {
        if (node == SiteMap.CurrentNode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}