using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.DTOs
{
    public class VoteResponseDTO
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool? Vote { get; set; }
    }
}