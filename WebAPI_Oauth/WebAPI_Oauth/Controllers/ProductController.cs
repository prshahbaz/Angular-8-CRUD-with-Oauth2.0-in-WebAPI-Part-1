using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Oauth.Models;

namespace WebAPI_Oauth.Controllers
{
        [RoutePrefix("Api/Product")]
        [Authorize]
        public class ProductController : ApiController
        {
            DataContext db = new DataContext();
            [HttpGet]
            [Route("GetProducts")]
            public IQueryable<Product> GetProducts()
            {
                try
                {
                    //  using (var db = new TESTEntities())
                    //  {
                    return db.Products;
                    //  }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            [HttpGet]
            [Route("GetProductById/{Id}")]
            public IHttpActionResult GetProductById(string Id)
            {
                Product product = new Product();
                try
                {
                    int ID = Convert.ToInt32(Id);
                    product = db.Products.Find(ID);
                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return Ok(product);
            }
            [HttpPost]
            [Route("InsertProduct")]
            public IHttpActionResult Create(Product product)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        db.Products.Add(product);
                        db.SaveChanges();
                        return Ok(product);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            [HttpPut]
            [Route("UpdateProduct")]
            public IHttpActionResult Update(Product product)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Ok(product);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            [HttpDelete]
            [Route("DeleteProduct/{Id}")]
            public IHttpActionResult Delete(int Id)
            {
                try
                {
                    Product product = db.Products.Find(Convert.ToInt32(Id));
                    if (product == null) { return NotFound(); }
                    else
                    {
                        db.Products.Remove(product);
                        db.SaveChanges();
                        return Ok(product);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
}
