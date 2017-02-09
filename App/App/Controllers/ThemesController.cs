using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using App.Models;

namespace App.Controllers
{
    public class ThemesController : ApiController
    {
        private DataCollection db = new DataCollection();

        // GET: api/Themes
        public IQueryable<Themes> GetThemes()
        {
            return db.Themes;
        }

        // GET: api/Themes/5
        [ResponseType(typeof(Themes))]
        public IHttpActionResult GetThemes(int id)
        {
            Themes themes = db.Themes.Find(id);
            if (themes == null)
            {
                return NotFound();
            }

            return Ok(themes);
        }

        // PUT: api/Themes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThemes(int id, Themes themes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != themes.ThemeID)
            {
                return BadRequest();
            }

            db.Entry(themes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThemesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Themes
        [ResponseType(typeof(Themes))]
        public IHttpActionResult PostThemes(Themes themes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Themes.Add(themes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = themes.ThemeID }, themes);
        }

        // DELETE: api/Themes/5
        [ResponseType(typeof(Themes))]
        public IHttpActionResult DeleteThemes(int id)
        {
            Themes themes = db.Themes.Find(id);
            if (themes == null)
            {
                return NotFound();
            }

            var posts = db.Posts.Where(p => p.ThemeID == themes.ThemeID).ToList();
            foreach (Posts post in posts)
            {
                var responses = db.Responses.Where(r => r.RootID == post.PostID).ToList();
                foreach (Responses response in responses)
                    db.Responses.Remove(response);
                db.Posts.Remove(post);
            }
            db.Themes.Remove(themes);
            db.SaveChanges();

            return Ok(themes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThemesExists(int id)
        {
            return db.Themes.Count(e => e.ThemeID == id) > 0;
        }
    }
}