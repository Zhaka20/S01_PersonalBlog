using S01_PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S01_PersonalBlog.ViewModels
{
    public class CreateCommentViewModel
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        public int? PostID { get; set; }
    }

    public class EditCommentViewModel
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Content { get; set; }
        public int? PostID { get; set; }
    }
    public class DeleteCommentViewModel
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime CommentDate { get; set; }
        public virtual AuthorViewModel Author { get; set; }
        public int? PostID { get; set; }
        public int CommentID { get; set; }
    }
    public class CommentDetailsViewModel
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime CommentDate { get; set; }
        public virtual AuthorViewModel Author { get; set; }
        public int? PostID { get; set; }
        public int CommentID { get; set; }
    }

    public class CommentPartialViewModel
    {
        public int CommentID { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int NumOfComments { get; set; }
        public int NestingLevel { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime CommentDate { get; set; }
        public int? ParentCommentID { get; set; }
        public int? PostID { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public string AuthorID { get; set; }
        public CommentVote CurrentUserVote { get; set; }

        public virtual AuthorViewModel Author { get; set; }
        public virtual ICollection<CommentPartialViewModel> Comments { get; set; }
    }

}