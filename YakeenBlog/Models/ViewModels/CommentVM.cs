using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YakeenBlog.Models.EFModels;

namespace YakeenBlog.Models.ViewModels
{
    public class CommentVM
    {
        public CommentVM()
        {
            Newcomment = new Comment();
        }
        public int PostId { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Comment Newcomment { get; set; }
    }
}