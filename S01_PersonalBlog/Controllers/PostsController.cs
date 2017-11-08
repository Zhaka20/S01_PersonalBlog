using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using S01_PersonalBlog.DAL;
using S01_PersonalBlog.Models;
using S01_PersonalBlog.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PagedList;
using PagedList.EntityFramework;
using System.Web;
using S01_PersonalBlog.Custom_Attributes;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.IO;

namespace S01_PersonalBlog.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        [AllowAnonymous]
        public async Task<ActionResult> Index(string searchString,string currentFilter, string tagsOnly, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.TagsOnly = tagsOnly;
            ViewBag.CurrentFilter = searchString;
            ViewBag.Checked = string.IsNullOrEmpty(tagsOnly) ? null : "checked";

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                if (!string.IsNullOrWhiteSpace(tagsOnly) && tagsOnly == "on")
                {
                    var postsWithmatchingTags = 
                                      await (from post in db.Posts
                                                  where post.Tags.Any(t => t.Title.Contains(searchString))
                                                  orderby post.PostDate
                                                  select post
                                            ).
                                            ProjectTo<PostIndexViewModel>().
                                            ToPagedListAsync(pageNumber,pageSize);

                    return View(postsWithmatchingTags);
                }

                var filteredResult = await (from post in db.Posts
                                           where post.Title.Contains(searchString) ||
                                           post.Content.Contains(searchString) ||
                                           post.Tags.Any(t => t.Title.Contains(searchString))
                                           orderby post.PostDate
                                           select post
                                           ).
                                           ProjectTo<PostIndexViewModel>().
                                           ToPagedListAsync(pageNumber, pageSize);

                return View(filteredResult);
            }

   
            var viewModel = await db.Posts.OrderBy(p => p.PostDate).                                         
                                           ProjectTo<PostIndexViewModel>().
                                           ToPagedListAsync(pageNumber, pageSize);
            return View(viewModel);
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.Where(p => p.PostID == id).
                               Include(p => p.Author.Image). 
                               Include(p => p.Comments.Select(c => c.Author.Image)).
                               Include(p => p.Votes).
                               SingleOrDefaultAsync();                               

            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<PostDetailViewModel>(post);
            return View(viewModel);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View(new EditCreatePostViewModel());
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<ActionResult> CreatePost(EditCreatePostViewModel postVewModel)
        {
            if (ModelState.IsValid)
            {
                List<Tag> tagList = GetTagsList(postVewModel.Tags);

                Post post = new Post()
                {
                    Title = postVewModel.Title,
                    Header = Base64ImgReplacer(postVewModel.Header),
                    Content = Base64ImgReplacer(postVewModel.Content),
                    PostDate = DateTime.Now,
                    AuthorID = User.Identity.GetUserId(),
                    Tags = tagList,
                };

                db.Posts.Add(post);

                await db.SaveChangesAsync();

                return RedirectToRoute("Post", new { action = "Index", id = post.PostID});
            }
            return View(postVewModel);
        }

        // GET: Posts/Edit/5
        [PostOwner(Roles = "Admin",IdName = "id")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var tags = string.Join(",", post.Tags.Select(t => t.Title));

            EditCreatePostViewModel editPost = new EditCreatePostViewModel()
            {
                Title = post.Title,
                Header = post.Header,
                Content = post.Content,
                Tags = tags
            };
            return View(editPost);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [PostOwner(Roles = "Admin", IdName = "id")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditCreatePostViewModel editedPost)
        {
            //check if id is not tempered
            if (ModelState.IsValid)
            {

                List<Tag> tagList = GetTagsList(editedPost.Tags);

                Post post = await db.Posts.FindAsync(id);
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                post.Tags.Clear();
                post.Title = editedPost.Title;
                post.Header = Base64ImgReplacer(editedPost.Header);
                post.Content = Base64ImgReplacer(editedPost.Content);
                post.Tags = tagList;

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = id });
            }
            return View(editedPost);
        }

        // GET: Posts/Delete/5
        [PostOwner(Roles = "Admin", IdName = "id")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [PostOwner(Roles = "Admin", IdName = "id")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpVote(int id)
        {
            var post = await db.Posts.Include(p => p.Votes).
                                FirstOrDefaultAsync(p => p.PostID == id);
            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            var vote = post.Votes.FirstOrDefault(v => v.VoterId == userId);
            if (vote == null)
            {
                vote = new PostVote() { Value = true, VoterId = userId };
                post.Votes.Add(vote);
            }
            else
            {
                if (vote.Value == true)
                {
                    vote.Value = null;
                }
                else
                {
                    vote.Value = true;
                }
            }
            db.SaveChanges();
            if (HttpContext.Request.IsAjaxRequest())
            {
                if(vote.Value == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            return RedirectToRoute("Default", new { action = "Details", controller = "Posts", id = id });
        }
        [HttpPost]
        public async Task<ActionResult> DownVote(int id)
        {
            var post = await db.Posts.Include(p => p.Votes).
                                FirstOrDefaultAsync(p => p.PostID == id);
            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            var vote = post.Votes.FirstOrDefault(v => v.VoterId == userId);
            if (vote == null)
            {
                vote = new PostVote() { Value = false, VoterId = userId };
                post.Votes.Add(vote);
            }
            else
            {
                if (vote.Value == false)
                {
                    vote.Value = null;
                }
                else
                {
                    vote.Value = false;
                }
            }
            db.SaveChanges();

            if (HttpContext.Request.IsAjaxRequest())
            {
                if (vote.Value == false)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }

            return RedirectToRoute("Default", new { action = "Details", controller = "Posts", id = id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //**************Helper methods*******************
        private static List<Tag> GetTagsList(string tags)
        {
            var tagArr = tags.Split(',')
                            .Select(x => x.Trim())
                            .Where(x => !string.IsNullOrWhiteSpace(x))
                            .ToArray();

            List<Tag> tagList = new List<Tag>();
            foreach (var item in tagArr)
            {
                tagList.Add(new Tag { Title = item });
            }

            return tagList;
        }

        private static string Base64StrToFile (Match match) 
        {
            string base64StrRegx = @"data:image/([a-zA-Z]*);base64,([^""]+)";
            string originalStr = match.Value;

            string replasedStr = Regex.Replace(originalStr, base64StrRegx, (base64Str =>
            {
                Guid guid = Guid.NewGuid();
                string imgExt = base64Str.Value.Split(new[] { ':', '/', ';' })[2];
                string newImgFileName = guid + "." + imgExt;

                string imgStorePath = Path.Combine(HostingEnvironment.MapPath("~/Content/Images/"), newImgFileName);
                string base64ImageStr = base64Str.Value.Split(',')[1];
                System.IO.File.WriteAllBytes(imgStorePath, Convert.FromBase64String(base64ImageStr));
                return "/Content/Images/" + newImgFileName;
            }));
            return replasedStr;
        }

        private static string Base64ImgReplacer(string inputString)
        {
            string base64ImageTagRegx = @"<img([\s\S])+?/?>";
            string result = Regex.Replace(inputString, base64ImageTagRegx, Base64StrToFile);
            return result;
        }
    }
    
}
