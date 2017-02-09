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
    public class ResponsesController : ApiController
    {
        private DataCollection db = new DataCollection();

        // GET: api/Responses
        public IQueryable<Responses> GetResponses()
        {
            return db.Responses;
        }

        // GET: api/Responses/5
        [ResponseType(typeof(Responses))]
        public IHttpActionResult GetResponses(int id)
        {
            Responses responses = db.Responses.Find(id);
            if (responses == null)
            {
                return NotFound();
            }

            return Ok(responses);
        }

        // PUT: api/Responses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponses(int id, Responses responses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responses.ResponseID)
            {
                return BadRequest();
            }

            db.Entry(responses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsesExists(id))
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

        // POST: api/Responses
        [ResponseType(typeof(Responses))]
        public IHttpActionResult PostResponses(Responses responses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Responses.Add(responses);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = responses.ResponseID }, responses);
        }

        // DELETE: api/Responses/5
        [ResponseType(typeof(Responses))]
        public IHttpActionResult DeleteResponses(int id)
        {
            Responses responses = db.Responses.Find(id);
            if (responses == null)
            {
                return NotFound();
            }

            db.Responses.Remove(responses);
            db.SaveChanges();

            return Ok(responses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponsesExists(int id)
        {
            return db.Responses.Count(e => e.ResponseID == id) > 0;
        }
    }
}