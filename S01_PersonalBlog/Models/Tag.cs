using S01_PersonalBlog.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public virtual Post Post{ get; set; }
    }
}