using S01_PersonalBlog.DAL;
using S01_PersonalBlog.Models;
using S01_PersonalBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PagedList.EntityFramework;

namespace S01_PersonalBlog.Controllers
{
    public class BloggersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Bloggers
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            var bloggers = await db.Users.Where(u => u.Posts.Count > 0).
                                    OrderBy(u => u.Posts.Count).
                                    ProjectTo<BloggersViewModel>().
                                    ToPagedListAsync(pageNumber, pageSize);

            return View(bloggers);
        }


        public async Task<ActionResult> Details(int? id,int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BloggerDetailViewModel bloggerVM =  await db.Users.Where(u => u.BloggerId == id).
                                                 Include(u => u.Image).
                                                 ProjectTo<BloggerDetailViewModel>().
                                                 FirstOrDefaultAsync();

            if (bloggerVM == null)
            {
                return HttpNotFound("Requested blogger does not exist");
            }
            bloggerVM.Posts = await db.Posts.Where(p => p.Author.BloggerId == id).
                                        OrderBy(p => p.PostDate).
                                        ProjectTo<PostIndexViewModel>().
                                        ToPagedListAsync(pageNumber,pageSize);
          
            return View(bloggerVM);
        }
    }   
}