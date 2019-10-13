using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YakeenBlog.Models.EFModels;

namespace YakeenBlog.Models.ViewModels
{
    public class PostCreateVM
    {
        //public PostCreateVM()
        //{
        //    Post = new Post();
        //}
        public Post Post { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}