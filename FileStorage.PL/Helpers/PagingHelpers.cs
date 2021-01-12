using FileStorage.PL.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace FileStorage.PL.Helpers
{
    /// <summary>
    /// PagingHelpers, page display
    /// </summary>
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected-btn");
                    tag.GenerateId("selected");
                    tag.AddCssClass("page-btn");
                }
                tag.AddCssClass("page-btn");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}