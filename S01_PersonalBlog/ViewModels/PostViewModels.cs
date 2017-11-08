using S01_PersonalBlog.CustomValidators;
using S01_PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S01_PersonalBlog.ViewModels
{
    public class PostIndexViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Header { get; set; }
        public string AuthorID { get; set; }
        public int NumOfComments { get; set; }  
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public AuthorViewModel Author { get; set; }
    }

    public class PostDetailViewModel
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string AuthorID { get; set; }
        public int NumOfComments { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime PostDate { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public PostVote CurrentUserVote { get; set; }

        public AuthorViewModel Author { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        public string Comment { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<CommentPartialViewModel> Comments { get; set; }
    }

    public class EditCreatePostViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title should be at least 2 characters long")]
        [MaxLength(120, ErrorMessage = "Title cannot be longer than 60 character")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Header is required")]
        [MinLength(40, ErrorMessage = "Header content should be at least 40 characters long")]
        [DataType(DataType.MultilineText)]
        public string Header { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Post content is required")]
        [MinLength(320, ErrorMessage = "Post content should be at least 320 characters long")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required(ErrorMessage = "At least one tag is required")]
        [MinLength(2, ErrorMessage = "Tag length should be at least 2 characters long")]
        [ValidTag]
        [DataType(DataType.MultilineText)]
        public string Tags { get; set; }
    }

    public class AuthorViewModel
    {
        [Display(Name = "Nickname")]
        public string NickName { get; set; }
        public int BloggerId { get; set; }
        public string Email { get; set; }
        public string ImageFileName { get; set; }
    }
}