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
using S01_PersonalBlog.DTOs;
using Microsoft.AspNet.Identity;

namespace S01_PersonalBlog.Controllers
{
    public class PostAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PostAPI
        public IQueryable<Post> GetPosts()
        {
            return db.Posts;
        }

        // GET: api/PostAPI/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/PostAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostID)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/PostAPI
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = post.PostID }, post);
        }

        // DELETE: api/PostAPI/5
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            await db.SaveChangesAsync();

            return Ok(post);
        }


        [Route("~/api/posts/UpVote/{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> UpVote(int id)
        {
            var post = await db.Posts.Include(p => p.Votes).
                                FirstOrDefaultAsync(p => p.PostID == id);
            if (post == null)
            {
                return BadRequest();
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
            try
            {
                db.SaveChanges();

                VoteResponseDTO dto = new VoteResponseDTO()
                {
                    Likes =  post.Votes.Where(v => v.Value == true).Count(),
                    Dislikes =  post.Votes.Where(v => v.Value == false).Count(),
                    Vote = vote.Value
                };
                return Json(dto);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Ok();
        }
        [Route("~/api/posts/DownVote/{id:int}")]
        [HttpPost]
        public async Task<IHttpActionResult> DownVote(int id)
        {
            var post = await db.Posts.Include(p => p.Votes).
                               FirstOrDefaultAsync(p => p.PostID == id);
            if (post == null)
            {
                return BadRequest();
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
            try
            {
                db.SaveChanges();

                VoteResponseDTO dto = new VoteResponseDTO()
                {
                    Likes = post.Votes.Where(v => v.Value == true).Count(),
                    Dislikes = post.Votes.Where(v => v.Value == false).Count(),
                    Vote = vote.Value
                };

                return Json(dto);

            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostID == id) > 0;
        }
    }
}