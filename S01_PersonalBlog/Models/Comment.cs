using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        public string Content { get; set; }
        public int NestingLevel { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime CommentDate { get; set; }

        [ForeignKey("Author")]
        public string AuthorID { get; set; }
        public virtual ApplicationUser Author { get; set; }
       
        public int? PostID { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        public int? ParentCommentID { get; set; }
        [ForeignKey("ParentCommentID")]
        public Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentVote> Votes { get; set; }
    }
}