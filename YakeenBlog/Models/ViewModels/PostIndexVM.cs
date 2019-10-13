using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YakeenBlog.Models.EFModels;

namespace YakeenBlog.Models.ViewModels
{
    public class PostIndexVM
    {
        public IEnumerable<PostDetailVM> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}