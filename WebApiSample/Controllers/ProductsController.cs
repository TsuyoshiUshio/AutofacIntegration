﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApiSample.Models;
using System.Linq;

namespace WebApiSample.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product(){ Id = 1, Name = "Curry", Category = "Dinner", Price = 1},
            new Product(){ Id = 2, Name = "Tandory Chicken", Category = "Sub", Price = 2},
            new Product(){ Id = 3, Name = "Mouse", Category = "PC", Price = 10000}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return  NotFound();
            }
            return Ok(product);
        }
    }
}
