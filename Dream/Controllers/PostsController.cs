using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dream.Models;

namespace Dream.Controllers
{
    public class PostsController : Controller
    {
        private PostContext db = new PostContext();

        // GET: Posts
        public ActionResult Index()
        {

            var posts = db.Posts.ToList();
            var ratings = db.Ratings.ToList();
            posts.Reverse();
            return View(posts);
        }
        public ActionResult Ratings()
        {

            var posts = db.Posts.ToList();
            var ratings = db.Ratings.ToList();
            posts.Sort((a,b)=>a.avrgRating.CompareTo(b.avrgRating));
            posts.Reverse();
            return View(posts);
        }
        [Authorize]
        public ActionResult Like(int postId)
        {
            Rating rating = new Rating();
            rating.PostId = postId;
            rating.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Post post=new Post();
            foreach (var b in db.Ratings)
            {
                if (b.PostId==rating.PostId && b.UserId==rating.UserId)
                {
                    post = db.Posts.Find(b.PostId);
                    post.avrgRating--;
                    db.Entry(post).State = EntityState.Modified;
                    db.Ratings.Remove(b);
                   
                }
            }
            if (db.Entry(post).State==EntityState.Modified)
            {
                db.SaveChanges();
                return RedirectToAction("Index", "Posts");
            }
            //rating.RatingId = db.Ratings.LastOrDefault().RatingId + 1;
            db.Ratings.Add(rating);
            post = db.Posts.Find(rating.PostId);
            post.avrgRating++;
            db.Entry(post).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index","Posts");
        }
        // GET: Posts/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return RedirectToAction("Index");
        }

        // POST: Posts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AuthorId,Text,avrgRating,Time")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Time=DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            //return PartialView(post);
        }

        // GET: Posts/Edit/5

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Text,avrgRating,Time")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            List<int> ids = new List<int>();
            foreach(var b in db.Comments)
            {
                if (b.ParentId==post.Id)
                {
                    ids.Add(b.CommentId);
                }
            }
            foreach (var b in ids)
            {
                db.Comments.Remove(db.Comments.Find(b));
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
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
