using PagedList;
using PagedList.EntityFramework;
using S01_PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.ViewModels
{
    public class BloggersViewModel
    {
        [Display(Name = "Picture")]
        public string ImageFile { get; set; }
        public int  BloggerId { get; set; }
        public string About { get; set; }
        [Display(Name = "Nickname")]
        public string NickName { get; set; }
        public int PostCount { get; set; }
    }

    public class BloggerDetailViewModel
    {
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        [Display(Name = "Nickname")]
        public string NickName { get; set; }
        [Display(Name = "Picture")]
        public string ImageFile { get; set; }
        public int BloggerId { get; set; }
        public string About { get; set; }   
        public int PostCount { get; set; }

        public IPagedList<PostIndexViewModel> Posts { get; set; }
    }
}