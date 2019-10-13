using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using YakeenBlog.BLL;
using YakeenBlog.Models;
using YakeenBlog.Models.EFModels;
using YakeenBlog.Models.ViewModels;

namespace YakeenBlog.Controllers
{
    public class PostsController : Controller
    {
        public UnitOfWork UnitOfWork
        {
            get
            {
                return HttpContext.GetOwinContext().Get<UnitOfWork>();
            }
        }

        // GET: Posts
        public ActionResult Index()
        {
            List<PostDetailVM> postsVM = UnitOfWork.PostManager.GetAll()
                .OrderByDescending(p => p.Time)
                .Select( p => new PostDetailVM()
                {
                    PostId = p.Id,
                    Header = p.Address,
                    Text = p.Text.Substring(0,500) + "...",
                    Time = p.Time,
                    Category = p.Category.Name,
                    WriterName = p.User.UserName,
                    CommentsVM = new CommentVM()
                    {
                        PostId = p.Id,
                        Comments = p.Comments.OrderBy(c => c.Time)
                    }
                })
                .ToList();
            PostIndexVM postIndex = new PostIndexVM()
            {
                Posts = postsVM,
                Categories = UnitOfWork.CategoryManager.GetAllBind()
            };
            return View(postIndex);
        }

        public ActionResult GetByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<PostDetailVM> postsVM = UnitOfWork.PostManager.GetAll()
                .Where( p => p.Category.Id == id)
                .OrderBy(p => p.Time)
                .Select(p => new PostDetailVM()
                {
                    PostId = p.Id,
                    Header = p.Address,
                    Text = p.Text.Substring(0, 500) + "...",
                    Time = p.Time,
                    Category = p.Category.Name,
                    WriterName = p.User.UserName,
                    CommentsVM = new CommentVM()
                    {
                        PostId = p.Id,
                        Comments = p.Comments.OrderBy(c => c.Time)
                    }
                })
                .ToList();
            return PartialView("_PostsIndex",postsVM);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = UnitOfWork.PostManager.GetById(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            PostDetailVM PostVM = new PostDetailVM()
            {
                PostId = post.Id,
                Header = post.Address,
                Text = post.Text,
                Category = post.Category.Name,
                WriterName = post.User.UserName,
                Time = post.Time,
                CommentsVM = new CommentVM()
                {
                    PostId = post.Id,
                    Comments = post.Comments.OrderBy(c => c.Time)
                }
            };
            
            return View(PostVM);
        }

        // GET: Posts/Create
        [Authorize (Roles = "admin,Admin")]
        public ActionResult Create()
        {
            
            PostCreateVM postCreateVm = new PostCreateVM()
            {
                Categories = UnitOfWork.CategoryManager.GetAll()
                    .Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                    .ToList(),
            };
            return View(postCreateVm);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                Category category = UnitOfWork.CategoryManager.GetById(post.Category.Id);
                post.Category = category;
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var user = UnitOfWork.ApplicationUserManager.FindById(userId);
                post.User = user;
                post.Time = DateTime.Now;
                UnitOfWork.PostManager.Add(post);
                return RedirectToAction("Index");
            }

            PostCreateVM postCreateVm = new PostCreateVM()
            {
                Categories = UnitOfWork.CategoryManager.GetAll()
                    .Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                    .ToList(),
            };
            return View(postCreateVm);
        }

        //Create a comment
        [Authorize]
        public ActionResult CreateComment(string text ,int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Post post = UnitOfWork.PostManager.GetById(id);
            if (!text.IsNullOrWhiteSpace())
            {
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var user = UnitOfWork.ApplicationUserManager.FindById(userId);
                Comment comment = new Comment()
                {
                    Text = text,
                    User = user,
                    Time = DateTime.Now,
                    Post = post
                };
                UnitOfWork.CommentManager.Add(comment);
            }
            CommentVM commentVm = new CommentVM()
            {
                PostId = post.Id,
                Comments = post.Comments.OrderBy(c => c.Time)
            };

            return PartialView("_comments", commentVm);

        }

        [Authorize(Roles = "admin,Admin")]
        public ActionResult UserArticles()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var user = UnitOfWork.ApplicationUserManager.FindById(userId);

            List<PostDetailVM> postsVM = UnitOfWork.PostManager.GetAll()
                .Where(p => p.User.Id == userId)
                .OrderByDescending(p => p.Time)
                .Select(p => new PostDetailVM()
                {
                    PostId = p.Id,
                    Header = p.Address,
                    Text = p.Text.Substring(0, 500) + "...",
                    Time = p.Time,
                    Category = p.Category.Name,
                    WriterName = p.User.UserName,
                    CommentsVM = new CommentVM()
                    {
                        PostId = p.Id,
                        Comments = p.Comments.OrderBy(c => c.Time)
                    }
                })
                .ToList();
            PostIndexVM postIndex = new PostIndexVM()
            {
                Posts = postsVM,
                Categories = UnitOfWork.CategoryManager.GetAllBind()
            };

            return View("Index", postIndex);
        }

        public ActionResult GetBySearch(string txt_search)
        {
            if (txt_search.IsNullOrWhiteSpace())
            {
                List<PostDetailVM> postsVMs = UnitOfWork.PostManager.GetAll()
                    .OrderByDescending(p => p.Time)
                    .Select(p => new PostDetailVM()
                    {
                        PostId = p.Id,
                        Header = p.Address,
                        Text = p.Text.Substring(0, 500) + "...",
                        Time = p.Time,
                        Category = p.Category.Name,
                        WriterName = p.User.UserName,
                        CommentsVM = new CommentVM()
                        {
                            PostId = p.Id,
                            Comments = p.Comments.OrderBy(c => c.Time)
                        }
                    })
                    .ToList();
                return PartialView("_PostsIndex", postsVMs);
            }
            List<PostDetailVM> postsVM = UnitOfWork.PostManager.GetAll()
                .Where(p => p.Address.ToLower().Contains(txt_search)
                            ||p.Text.ToLower().Contains(txt_search)
                            ||p.User.UserName.ToLower().Contains(txt_search))
                .OrderBy(p => p.Time)
                .Select(p => new PostDetailVM()
                {
                    PostId = p.Id,
                    Header = p.Address,
                    Text = p.Text.Substring(0, 500) + "...",
                    Time = p.Time,
                    Category = p.Category.Name,
                    WriterName = p.User.UserName,
                    CommentsVM = new CommentVM()
                    {
                        PostId = p.Id,
                        Comments = p.Comments.OrderBy(c => c.Time)
                    }
                })
                .ToList();
            return PartialView("_PostsIndex", postsVM);
        }


        //// GET: Posts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Address,Text,Time")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(post).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(post);
        //}

        //// GET: Posts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Post post = db.Posts.Find(id);
        //    db.Posts.Remove(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
