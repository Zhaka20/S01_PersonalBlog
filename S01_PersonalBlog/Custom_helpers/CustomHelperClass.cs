using S01_PersonalBlog.Models;
using S01_PersonalBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace S01_PersonalBlog.Custom_helpers
{
    public static class CustomHelperClass
    {
        public static IHtmlString TagsFromCollection(this HtmlHelper helper, IEnumerable<Tag> tagsCollection)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var tag in tagsCollection)
            {
                strBuilder.Append($"<span class=\"post-tag\">{tag.Title}</span>");
            }
            return new HtmlString(strBuilder.ToString());
        }
        public static IHtmlString GetImageURL(this HtmlHelper helper, string imgName)
        {
            string result = (string.IsNullOrEmpty(imgName) ? "" : string.Format("src={0}", "/Content/Images/" + imgName));

            return new HtmlString(result);
        }

        public static IHtmlString UserLiked(this HtmlHelper helper, Vote vote)
        {
            string result = "hidden";
          
            if (vote != null && vote.Value == true)
            {
                result = string.Empty;
            }
            return new HtmlString(result);
        }

        public static IHtmlString UserDisliked(this HtmlHelper helper, Vote vote)
        {
            string result = "hidden";

            if (vote != null && vote.Value == false)
            {
                result = string.Empty;
            }
            return new HtmlString(result);
        }
    }
}