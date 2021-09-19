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
using VCPApi.Models;

namespace VCPApi.Controllers
{
    public class UserController : ApiController
    {
        private VCPEntities db = new VCPEntities();

        // GET: api/User
        public IQueryable<tblCustomer> GettblCustomers()
        {
            return db.tblCustomers;
        }

        // GET: api/User/5
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult GettblCustomer(int id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return Ok(tblCustomer);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCustomer(int id, tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCustomer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(tblCustomer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCustomerExists(id))
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

        // POST: api/User
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult PosttblCustomer(tblCustomer tblCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCustomers.Add(tblCustomer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblCustomerExists(tblCustomer.CustomerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblCustomer.CustomerID }, tblCustomer);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(tblCustomer))]
        public IHttpActionResult DeletetblCustomer(int id)
        {
            tblCustomer tblCustomer = db.tblCustomers.Find(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            db.tblCustomers.Remove(tblCustomer);
            db.SaveChanges();

            return Ok(tblCustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCustomerExists(int id)
        {
            return db.tblCustomers.Count(e => e.CustomerID == id) > 0;
        }
    }
}