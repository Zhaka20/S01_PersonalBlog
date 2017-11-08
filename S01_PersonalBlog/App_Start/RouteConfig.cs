using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace S01_PersonalBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Comment",
               url: "posts/{postId}/Comments/{action}/{commentID}",
               defaults: new { Controller = "Comments", action = "Index", commentID = UrlParameter.Optional },
               constraints: new { postId = @"\d+", commentId = @"\d*" }
           );

            routes.MapRoute(
                name: "Post",
                url: "posts/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
