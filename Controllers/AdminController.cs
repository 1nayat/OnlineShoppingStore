using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System.IO;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // Get Category list for the dropdown
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var categories = _unitOfWork.GetRepositoryInstances<TblCategory>().GetAllRecords();

            foreach (var item in categories)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }

            return list;
        }

        // Dashboard View
        public ActionResult Dashboard()
        {
            return View();
        }

        // Categories List View
        public ActionResult Categories()
        {
            List<TblCategory> allCategories = _unitOfWork.GetRepositoryInstances<TblCategory>()
                                                          .GetAllRecordsIQueryable()
                                                          .Where(i => i.IsDelete == false)
                                                          .ToList();
            return View(allCategories);
        }

        // Add Category View
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);  // Add new category
        }

        // Edit Category View
        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != 0)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(
                            _unitOfWork.GetRepositoryInstances<TblCategory>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();  // New category if ID is 0
            }
            return View("UpdateCategory", cd);
        }

        // Product List View
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstances<TblProduct>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            var product = _unitOfWork.GetRepositoryInstances<TblProduct>().GetFirstorDefault(productId);
            if (product != null && !string.IsNullOrEmpty(product.ProductImage))
            {
                // Set the correct URL path for the image (assuming ProductImage is stored as just the filename)
                product.ProductImage = "/ProductImg/" + product.ProductImage;
            }

            ViewBag.CategoryList = GetCategory();
            return View(product);
        }



        [HttpPost]
        public ActionResult ProductEdit(TblProduct tbl)
        {
            if (ModelState.IsValid)
            {
                tbl.ModifiedDate = DateTime.Now;

                _unitOfWork.GetRepositoryInstances<TblProduct>().Update(tbl);

                _unitOfWork.SaveChanges();

               
                return RedirectToAction("Product");
            }

            return View(tbl);
        }


        public ActionResult ProductAdd()
        {
            var categories = _unitOfWork.GetRepositoryInstances<TblCategory>()
                                        .GetAllRecords()
                                        .Select(c => new SelectListItem
                                        {
                                            Text = c.CategoryName,
                                            Value = c.CategoryId.ToString()
                                        }).ToList();

            ViewBag.CategoryList = categories;
            return View();
        }

        // Handle product add (POST)
        [HttpPost]
        public ActionResult ProductAdd(TblProduct tbl, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (file != null && file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";  // Ensure unique file names
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImg", fileName);

                    // Save the file to the file system
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Set the ProductImage property to the relative URL
                    tbl.ProductImage = $"/ProductImg/"+fileName;
                }

                tbl.CreatedDate = DateTime.Now;

                // Save the new product
                _unitOfWork.GetRepositoryInstances<TblProduct>().Add(tbl);
                _unitOfWork.SaveChanges();

                return RedirectToAction("Product");
            }

            return View(tbl);
        }


        // Edit Category View (for updating)
        public ActionResult CategoryEdit(int CatId)
        {
            return View(_unitOfWork.GetRepositoryInstances<TblCategory>().GetFirstorDefault(CatId));
        }

        // Handle Category edit (POST)
        [HttpPost]
        public ActionResult CategoryEdit(TblCategory tbl)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstances<TblCategory>().Update(tbl);
                return RedirectToAction("Categories");
            }

            return View(tbl);
        }
    }
}
