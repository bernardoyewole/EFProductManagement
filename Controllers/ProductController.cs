using AutoMapper;
using EFProductManagement.Models;
using PMBLL;
using PMDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFProductManagement.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // Passes products from data access layer to view
        [HttpGet] 
        public JsonResult GetProducts()
        {
            ProductService ps = new ProductService();
            var products = ps.GetProductsService();

            List<ProductVM> productVMs = new List<ProductVM>();

            productVMs = Mapper.Map<List<Product>, List<ProductVM>>(products);
            return Json(productVMs, JsonRequestBehavior.AllowGet);
        }

        // Passes product data from view to data access layer
        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            ProductService ps = new ProductService();
            var productAdded = ps.AddProductService(product);
            return Json(productAdded, JsonRequestBehavior.AllowGet);
        }

        // Returns a product based on its id to view
        [HttpGet]
        public JsonResult GetProductById(int id)
        {
            ProductService ps = new ProductService();
            var product = ps.GetProductByIdService(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        
        // Passes updated product information to data repository
        [HttpPost]
        public JsonResult UpdateProduct(Product product)
        {
            ProductService ps = new ProductService();
            var isProductUpdated = ps.UpdateProductService(product);
            return Json(isProductUpdated, JsonRequestBehavior.AllowGet);
        }
        
        // Handles removing a product from view and data repository
        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            ProductService ps = new ProductService();
            if (ps.DeleteProductService(productId))
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonRequestBehavior.DenyGet);
            }
        }
    }
}