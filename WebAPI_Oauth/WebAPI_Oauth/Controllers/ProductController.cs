using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Oauth.Models;

namespace WebAPI_Oauth.Controllers
{
        [RoutePrefix("Api/Product")]//This is Route prefix filter which will be added in the url for this specfic controller
        [Authorize]//This filter redirect the request to the provider class first request will authenticate if authentication sucessful than it will come to here
        public class ProductController : ApiController
        {
            [HttpGet]
            [Route("GetProducts")]
            public List<Product> GetProducts()//This is th get method which get all the products from the db and return
            {
            List<Product> productList = new List<Product>();
            using (DataContext dataContext=new DataContext())
            {
                productList = dataContext.Products.ToList();
            }
            return productList;
            }
            [HttpGet]
            [Route("GetProductById/{Id}")]
            public Product GetProductById(string Id)//This is th get method which get one record on the basis of ID 
        {
                Product product = new Product();
                using (DataContext dataContext = new DataContext())
            {
                product = dataContext.Products.Find(Convert.ToInt32(Id));
            }              
                return (product);
            }
            [HttpPost]
            [Route("InsertProduct")]
            public IHttpActionResult Create(Product product)//This method will insert the product into db
            {
            using (DataContext dataContext = new DataContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    dataContext.Products.Add(product);
                    dataContext.SaveChanges();
                    return Ok(product);
                }
            }               
            }
            [HttpPut]
            [Route("UpdateProduct")]
            public IHttpActionResult Update(Product product)//Update method will update the product
            {
            using (DataContext dataContext = new DataContext())
            {
                if (ModelState.IsValid)
                {
                    dataContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    dataContext.SaveChanges();
                    return Ok(product);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }                      
            }
            [HttpDelete]
            [Route("DeleteProduct/{Id}")]
            public IHttpActionResult Delete(int Id)//this method will Delete the record
            {
            using (DataContext dataContext = new DataContext())
            {
                Product product = dataContext.Products.Find(Convert.ToInt32(Id));
                if (product == null) { return NotFound(); }
                else
                {
                    dataContext.Products.Remove(product);
                    dataContext.SaveChanges();
                    return Ok(product);
                }
            }                         
            }
        }
}
