using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.Models
{
    public abstract class Vote
    {
        [Key]
        public int Id { get; set; }
        public bool? Value { get; set; }

        public string  VoterId { get; set; }
        [ForeignKey("VoterId")]
        public ApplicationUser Voter { get; set; }      
    }

    public class PostVote : Vote
    {
        public int? PostID { get; set; }
        [ForeignKey("PostID")]
        public Post Post { get; set; }
    }

    public class CommentVote : Vote
    {
        public int? CommentID { get; set; }
        [ForeignKey("CommentID")]
        public Comment Comment { get; set; }
    }
}