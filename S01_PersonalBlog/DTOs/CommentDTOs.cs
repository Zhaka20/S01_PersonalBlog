using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.DTOs
{
    public class PostCommentDTO
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }

    public class PutCommentDTO
    {
        [MinLength(5, ErrorMessage = "Comment should be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Comment connot be longer than 1000 characters")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }

}