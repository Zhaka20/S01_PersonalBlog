using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace S01_PersonalBlog.Models
{
    public class ImagePath
    {
            [StringLength(255)]
            public string FileName { get; set; }

            [Key, ForeignKey("Person")]
            public string PersonID { get; set; }
            public virtual ApplicationUser Person { get; set; }
    }
}