using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using S01_PersonalBlog.DAL;
using S01_PersonalBlog.Models;
using S01_PersonalBlog.Custom_Attributes;
using S01_PersonalBlog.DTOs;
using Microsoft.AspNet.Identity;

namespace S01_PersonalBlog.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts/{postId:int}/Comments")]
    public class CommentsAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("")]
        [AllowAnonymous]
        public IQueryable<Comment> GetComments(int postId)
        {
            return db.Comments.Where(c => c.PostID == postId);
        }

        [Route("details/{id:int}")]
        [AllowAnonymous]
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [Route("edit/{id:int}")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [CommentOwner(IdName = "id", Roles = "Admin")]
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PutComment(int id, PutCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comment comment = AutoMapper.Mapper.Map<Comment>(commentDTO);

            db.Entry(comment).Property(c => c.Content).IsModified = true;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("create")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(int postId, PostCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await db.Posts.FindAsync(postId);
            if(post == null)
            {
                return BadRequest(ModelState);
            }

            Comment comment = AutoMapper.Mapper.Map<Comment>(commentDTO);
            comment.CommentDate = DateTime.Now;
            comment.AuthorID = User.Identity.GetUserId();
            post.Comments.Add(comment);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("create/{id:int}")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostCommentReply(int postId, int id, PostCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var parentComment = await db.Comments.FindAsync(id);
            if (parentComment == null)
            {
                return BadRequest("Comment does not exist");
            }

            Comment comment = AutoMapper.Mapper.Map<Comment>(commentDTO);
            comment.CommentDate = DateTime.Now;
            comment.AuthorID = User.Identity.GetUserId();
            parentComment.Comments.Add(comment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);

        }

        [Route("delete/{id:int}")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [CommentOwner(IdName = "id",Roles = "Admin")]
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentID == id) > 0;
        }
    }
}