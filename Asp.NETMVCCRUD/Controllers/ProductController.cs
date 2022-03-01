using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;



namespace Asp.NETMVCCRUD.Controllers
{
    public class ProductController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModels db = new DBModels())
            {
                List<Product> Prolist = db.Products.ToList<Product>();
                return Json(new { data = Prolist }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                using (DBModels db = new DBModels())
                {
                    return View(db.Products.Where(x => x.ProductId==id).FirstOrDefault<Product>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Product pro)
        {
            using (DBModels db = new DBModels())
            {
                if (pro.ProductId == 0)
                {
                    db.Products.Add(pro);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(pro).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModels db = new DBModels())
            {
                Product emp = db.Products.Where(x => x.ProductId== id).FirstOrDefault<Product>();
                db.Products.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}