using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_MainNav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MainMenuLiteral1.Text = String.Format("<ul class=\"nav navbar-nav\">{0}</ul>", DisplaySiteMapLevelAsBulletedList());
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
                if ((node["level"] == "1") && (node.HasChildNodes))
                {
                    output += IsCurrentPage(node) ?
                              String.Format("<li class=\"dropdown active\"><a href=\"{0}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"" + node["target"].ToString() + "\">{1} <b class=\"caret\"></b></a>", node.Url, node.Title) :
                              String.Format("<li class=\"dropdown\"><a href=\"{0}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"" + node["target"].ToString() + "\">{1} <b class=\"caret\"></b></a>", node.Url, node.Title);
                }
                else
                {
                    output += IsCurrentPage(node) ? String.Format("<li class=\"active\"><a href=\"{0}\" target=\"" + node["target"].ToString() + "\">{1}</a>", node.Url, node.Title) :
                              String.Format("<li><a href=\"{0}\" target=\"" + node["target"].ToString() + "\">{1}</a>", node.Url, node.Title);
                }
            }
            else //link DOES NOT open a new window or tab in the browser
            {
                if ((node["level"] == "1") && (node.HasChildNodes))
                {
                    output += IsCurrentPage(node) ?
                              String.Format("<li class=\"dropdown active\"><a href=\"{0}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">{1} <b class=\"caret\"></b></a>", node.Url, node.Title) :
                              String.Format("<li class=\"dropdown\"><a href=\"{0}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">{1} <b class=\"caret\"></b></a>", node.Url, node.Title);
                }
                else
                {
                    output += IsCurrentPage(node) ? String.Format("<li class=\"active\"><a href=\"{0}\">{1}</a>", node.Url, node.Title) :
                              String.Format("<li><a href=\"{0}\">{1}</a>", node.Url, node.Title);
                }

                //Add any children levels, if needed (recursively)
                if (node.HasChildNodes)
                {
                    output += String.Format("<ul class=\"dropdown-menu\">{0}</ul>", GetSiteMapLevelAsBulletedList(node.ChildNodes));
                }

                output += "</li>";
            }
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
            if (SiteMap.CurrentNode == null)
            {
                return false;
            }
            else
            {
                if (SiteMap.CurrentNode.IsDescendantOf(node))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}