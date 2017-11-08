using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.Models
{
    public class Post
    {
        public int PostID { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        [Required]
        public string Header { get; set; }
        [Required]
        public string Content { get; set; }
        

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH/mm/ss}")]
        public DateTime PostDate { get; set; }

        public string AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<PostVote> Votes { get; set; }
    }
}