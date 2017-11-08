using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using S01_PersonalBlog.DAL;
using S01_PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace S01_PersonalBlog.Custom_Attributes
{
    public abstract class ResourceOwner : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool userOrRoleSpecified = false;

            if (!string.IsNullOrWhiteSpace(Roles) || !string.IsNullOrWhiteSpace(Users))
            {
                userOrRoleSpecified = true;
            }

            bool isSpecifiedUserOrInRole = base.AuthorizeCore(httpContext) && userOrRoleSpecified;
            if  (isSpecifiedUserOrInRole)
            {
                return true;
            }

            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(httpContext.User.Identity.GetUserId());
            var resourceOwnerId = string.Empty;
            try
            {
                resourceOwnerId = GetResourceOwnerId(httpContext);
            }
            catch (Exception)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.End();
            }

            bool IsResourceOwner = resourceOwnerId == user.Id;

            if (IsResourceOwner)
            {
                return true;
            }

            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(filterContext);
            else
            {
                filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
                filterContext.HttpContext.Response.StatusCode = 403;
            }
        }


        public abstract string GetResourceOwnerId(HttpContextBase httpContext);
    }


    public class PostOwner : ResourceOwner
    {
        public string IdName { get; set; }

        public override string GetResourceOwnerId(HttpContextBase httpContext)
        {
            if(IdName == null)
            {
               throw new ArgumentNullException("IdName", "IdName cannot be null");
            }
            object idFromUrl = httpContext.Request.RequestContext.RouteData.Values[IdName];
            string postIdstr = idFromUrl == null ? null : idFromUrl.ToString();
            if (string.IsNullOrWhiteSpace(postIdstr))
            {
                throw new HttpException(400, "Bad Request. Resource id is not provided");
            }
            int id;
            if(!int.TryParse(postIdstr, out id))
            {
                throw new ArgumentException("Provided argument is not valid");
            }
            using (var db = new ApplicationDbContext())
            {
                Post post = db.Posts.Find(id);
                if (post == null)
                {
                    throw new HttpException(400, "Bad Request. Resource not found");
                }
                return post.AuthorID;
            }
        }
    }
    public class CommentOwner : ResourceOwner
    {
        public string IdName { get; set; }

        public override string GetResourceOwnerId(HttpContextBase httpContext)
        {
            if (IdName == null)
            {
                throw new ArgumentNullException("IdName", "IdName cannot be null");
            }
            object idFromUrl = httpContext.Request.RequestContext.RouteData.Values[IdName];
            string postIdstr = idFromUrl == null ? null : idFromUrl.ToString();
            if (string.IsNullOrWhiteSpace(postIdstr))
            {
                throw new HttpException(400, "Bad Request. Resource id is not provided");
            }
            int id;
            if (!int.TryParse(postIdstr, out id))
            {
                throw new ArgumentException("Provided argument is not valid");
            }
            using (var db = new ApplicationDbContext())
            {
                Comment post = db.Comments.Find(id);
                if (post == null)
                {
                    throw new HttpException(400, "Bad Request. Resource not found");
                }
                return post.AuthorID;
            }
        }
    }
}