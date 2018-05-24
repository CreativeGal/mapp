using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{

    public BasePage()
    { }

    protected override void OnLoadComplete(EventArgs e)
    {
        // Set the page's title, if necessary
        if (string.IsNullOrEmpty(Page.Title) || Page.Title == "Untitled Page")
        {
            // Is this page defined in the site map?
            string newTitle = null;

            if (SiteMap.Enabled)
            {
                SiteMapNode current = SiteMap.CurrentNode;

                if (current != null)
                {
                    newTitle = "Model Approach to Partnerships in Parenting (MAPP) - " + current.Title;

                    if (SiteMap.CurrentNode["keywords"] != null)
                    {
                        HtmlMeta meta = new HtmlMeta();
                        meta.Name = "Keywords";
                        meta.Content = SiteMap.CurrentNode["Keywords"];
                        Page.Header.Controls.AddAt(1, meta);
                    }

                    if (SiteMap.CurrentNode.Description != null)
                    {
                        HtmlMeta meta = new HtmlMeta();
                        meta.Name = "Description";
                        meta.Content = SiteMap.CurrentNode.Description.ToString();
                        Page.Header.Controls.AddAt(2, meta);
                    }
                }
                else
                {
                    newTitle = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath);
                }

                Page.Title = newTitle;
            }
        }

        base.OnLoadComplete(e);
    }
}