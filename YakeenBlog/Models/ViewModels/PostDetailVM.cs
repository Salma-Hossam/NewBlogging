using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YakeenBlog.Models.EFModels;

namespace YakeenBlog.Models.ViewModels
{
    public class PostDetailVM
    {
        public int PostId { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        [MinLength(300)]
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string WriterName { get; set; }
        public string Category { get; set; }
        public CommentVM CommentsVM { get; set; }
        //public Post Post { get; set; }
        //public ApplicationUser User { get; set; }
        //public Category Category { get; set; }
    }
}