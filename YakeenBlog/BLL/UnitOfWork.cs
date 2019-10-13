using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YakeenBlog.Models;

namespace YakeenBlog.BLL
{
    public class UnitOfWork : IDisposable
    {

        private readonly ApplicationDbContext context;

        private UnitOfWork(IOwinContext owinContext)
        {
            context = owinContext.Get<ApplicationDbContext>();
            ApplicationUserManager = owinContext.Get<ApplicationUserManager>();
        }

        public static UnitOfWork Create(IdentityFactoryOptions<UnitOfWork> options, IOwinContext owinContext)
        {
            return new UnitOfWork(owinContext);
        }

        public void Dispose()
        {

        }


        public PostManager PostManager
        {

            get
            {
                return new PostManager(context);
            }
        }

        public CommentManager CommentManager
        {
            get
            {
                return new CommentManager(context);
            }
        }

        public CategoryManager CategoryManager
        {
            get
            {
                return new CategoryManager(context);
            }
        }

        
        public ApplicationUserManager ApplicationUserManager { get; }



    }
}