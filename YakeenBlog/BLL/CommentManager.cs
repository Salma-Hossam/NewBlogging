using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YakeenBlog.Models;
using YakeenBlog.Models.EFModels;
using YakeenBlog.Repository;

namespace YakeenBlog.BLL
{
    public class CommentManager : Repository<Comment, ApplicationDbContext>
    {
        public CommentManager(ApplicationDbContext context) : base(context)
        {

        }
    }
}