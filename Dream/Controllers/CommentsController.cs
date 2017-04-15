using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dream.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Dream.Controllers
{
    public class CommentsController : Controller
    {
        private PostContext db = new PostContext();
        public int getPost()
        {
            string path = this.Request.Url.AbsolutePath;
            int i = path.Length - 1;
            while (path[i] != '/')
            {
                i--;
            }
            i++;
            string strId = "";
            for (int c = i; c < path.Length; c++)
            {
                strId += path[c];
            }
            int parentId = 0;
            if (strId.Any())
                parentId = Convert.ToInt32(strId);
            return parentId;
        }
        public string getName(string id)
        {
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = UserManager.FindById(id);
            return user.UserName;
        }
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Posts");
        }
        public ActionResult PostDetails()
        {
            PostView postView = new PostView();
            int parentId = getPost();
                if (parentId == 0)
            {
                return RedirectToAction("Index", "Posts");
            }

            postView.post = db.Posts.Find(parentId);
            List<Comment> justComments = new List<Comment>();
            foreach (var b in db.Comments)
            {
                if (b.ParentId == postView.post.Id)
                {
                    justComments.Add(b);
                }
            }
            foreach (var b in justComments)
            {
                b.AuthorId = getName(b.AuthorId);
            }
            postView.Comments = justComments;
            return View(postView);
        }
        // GET: Comments/PostDetails

        // POST: Comments/PostDetails
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult PostDetails([Bind(Include = "CommentId,AuthorId,ParentId,Text,Time")] Comment comment)
        {
            PostView postView = new PostView();
            int parentId = getPost();
            postView.post = db.Posts.Find(parentId);
            if (ModelState.IsValid)
            {
                comment.ParentId = parentId;
                comment.AuthorId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                comment.Time=DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                List<Comment> comments = new List<Comment>();
                foreach (var b in db.Comments)
                {
                    if (b.ParentId == postView.post.Id)
                    {
                        comments.Add(b);
                    }

                }
                foreach (var b in comments)
                {
                    b.AuthorId = getName(b.AuthorId);
                }
                
               // comments.Add(comment);
                postView.Comments = comments;
                return View(postView);
            }
            List<Comment> justComments = new List<Comment>();
            justComments = db.Comments.ToList();
            postView.post = db.Posts.Find(parentId);
            postView.Comments = justComments;
            return View(postView);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
