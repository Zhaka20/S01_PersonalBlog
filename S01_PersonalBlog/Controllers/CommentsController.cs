using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S01_PersonalBlog.DAL;
using S01_PersonalBlog.Models;
using S01_PersonalBlog.ViewModels;
using Microsoft.AspNet.Identity;
using S01_PersonalBlog.Custom_Attributes;
using AutoMapper;

namespace S01_PersonalBlog.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Comments
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var comments = db.Comments.Include(c => c.Author);
            return View(await comments.ToListAsync());
        }

        // GET: Comments/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(commentId);
            var viewModel = Mapper.Map<CommentDetailsViewModel>(comment);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: Comments/Create
        public ActionResult Create(int postId)
        {
            var viewModel = new CreateCommentViewModel()
            {
                PostID = postId,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int postId, int? commentId, CreateCommentViewModel submittedComment)
        {
            if (!ModelState.IsValid)
            {
                return View(submittedComment);
            }

            Comment newComment = new Comment()
            {
                CommentDate = DateTime.Now,
                AuthorID = User.Identity.GetUserId(),
                PostID = postId,
                ParentCommentID = commentId,
                Content = submittedComment.Comment
            };

            if (commentId != null)
            {
                var parentComment = await db.Comments.FindAsync(commentId);
                if (parentComment == null || parentComment.NestingLevel >= 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                newComment.NestingLevel = parentComment.NestingLevel + 1;
                parentComment.Comments.Add(newComment);
            }
            else
            {
                db.Comments.Add(newComment);
            }

            await db.SaveChangesAsync();

            if (HttpContext.Request.IsAjaxRequest())
            {
                newComment = db.Comments.Where(c => c.CommentID == newComment.CommentID).Include(c => c.Author.Image).Single();
                var commentVM = Mapper.Map<CommentPartialViewModel>(newComment);
                return PartialView("_Comment", commentVM);
            }
            return RedirectToAction("Details", "Posts", new { id = postId });
        }

        // GET: Comments/Edit/5
        [CommentOwner(IdName = "commentID", Roles = "Admin")]
        public async Task<ActionResult> Edit(int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(commentId);         
            if (comment == null)
            {
                return HttpNotFound();
            }
            EditCommentViewModel viewModel = new EditCommentViewModel { Content = comment.Content, PostID = comment.PostID };
            return View(viewModel);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CommentOwner(IdName = "commentID", Roles = "Admin")]
        public async Task<ActionResult> Edit(EditCommentViewModel comment, int postId, int? commentID)
        {
            if(commentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            if (ModelState.IsValid)
            {
                Comment originalComment = await db.Comments.FindAsync(commentID);
                if(originalComment == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                originalComment.Content = comment.Content;
                await db.SaveChangesAsync();
                return RedirectToRoute("Post", new { Action = "Details", id = postId});
            }
            return View(comment);
        }


        // GET: Comments/Delete/5
        [CommentOwner(IdName = "commentID", Roles = "Admin")]
        public async Task<ActionResult> Delete(int? commentId)
        {
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return HttpNotFound();
            }
            var viewModel = Mapper.Map<DeleteCommentViewModel>(comment);
            return View(viewModel);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CommentOwner(IdName = "commentID", Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int commentId, int postId)
        {
            Comment comment = await db.Comments.Include(c => c.Comments).FirstOrDefaultAsync( u => u.CommentID == commentId);

            DeleteAllChildComments(comment.Comments);
            //db.Comments.RemoveRange(comment.Comments);
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToRoute("Post", new { Action = "Details", id = postId });
        }

        [HttpPost]
        public async Task<ActionResult> UpVote(int postId, int? commentID)
        {
            if (commentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var user = await db.Users.Include(u => u.CommentVotes).
                                      FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var voteInDb = user.CommentVotes.Where(v => v.CommentID == commentID).SingleOrDefault();
            if (voteInDb == null)
            {               
                var newVote = new CommentVote()
                {
                    Value = true
                };
                user.CommentVotes.Add(newVote);
                var comment = await db.Comments.FindAsync(commentID);
                comment.Votes.Add(newVote);
            }
            else
            {
                if (voteInDb.Value == true)
                {
                    voteInDb.Value = null;
                }
                else
                {
                    voteInDb.Value = true;
                }
            }

            await db.SaveChangesAsync();

            return RedirectToRoute("Default", new { action = "Details", controller = "Posts", id = postId });

        }
        [HttpPost]
        public async Task<ActionResult> DownVote(int postId, int? commentID)
        {
            if (commentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var user = await db.Users.Include(u => u.CommentVotes).
                                      FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var voteInDb = user.CommentVotes.Where(v => v.CommentID == commentID).SingleOrDefault();
            if (voteInDb == null)
            {
                var newVote = new CommentVote()
                {
                    Value = false
                };
                user.CommentVotes.Add(newVote);
                var comment = await db.Comments.FindAsync(commentID);
                comment.Votes.Add(newVote);
            }
            else
            {
                if (voteInDb.Value == false)
                {
                    voteInDb.Value = null;
                }
                else
                {
                    voteInDb.Value = false;
                }
            }
            await db.SaveChangesAsync();
            return RedirectToRoute("Default", new { action = "Details", controller = "Posts", id = postId });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //********************helper methods***********************
        private void DeleteAllChildComments(ICollection<Comment> nestedComments)
        {
            if(nestedComments == null) { return; }

            for (int i = nestedComments.Count - 1; i >= 0; i--)
            {
                DeleteAllChildComments(nestedComments.ElementAt(i).Comments);
                db.Comments.Remove(nestedComments.ElementAt(i));
            }
        }
        

    }
}
