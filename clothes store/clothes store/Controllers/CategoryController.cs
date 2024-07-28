using clothes_store.Models;
using clothes_store.Repository;
using clothes_store.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace clothes_store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _category;
        private readonly Project conteext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(ICategory category, Project conteext, IWebHostEnvironment hostEnvironment)
        {
            _category = category;
            this.conteext = conteext;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var item = _category.GetAll().Where(c => c.largeCategoryId == 1).ToList();
            return View(item);
        }
        public IActionResult IndexM()
        {
            var item = _category.GetAll().Where(c => c.largeCategoryId == 2).ToList();
            return View(item);
        }
        public IActionResult Indexkids()
        {
            var item = _category.GetAll().Where(c => c.largeCategoryId == 3).ToList();
            return View(item);
        }

        public IActionResult IndexMy()
        {
            var item = _category.GetAll();
            return View(item);
        }

        public IActionResult Details(int id)
        {
            var cat = _category.GetbyId(id);
            if (cat == null) return NotFound();
            return View(cat);
        }
        // Add 
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveAdd(categoryVM categoryVM, IFormFile Image)
        {

            var wwwroot = _hostEnvironment.WebRootPath + "/img/gallery/";
            Guid id = Guid.NewGuid();
            string fullpath = Path.Combine(wwwroot, id + Image.FileName);
            using (var filestream = new FileStream(fullpath, FileMode.Create))
            {
                Image.CopyTo(filestream);
            }
            categoryVM.Image = id + Image.FileName;

            var category = new Category
            {
                Name = categoryVM.Name,
                Id = categoryVM.Id,
                Image = categoryVM.Image,
                largeCategoryId = categoryVM.largeCategoryId,
            };
            conteext.Categories.Add(category);
            conteext.SaveChanges();
            if (category.largeCategoryId == 1)
            {
                return RedirectToAction("Index");
            }
            else if (category.largeCategoryId == 2)
            {
                return RedirectToAction("IndexM");

            }
            else
            {
                return RedirectToAction("IndexKids");

            }
        }

        public IActionResult Edit(int id)
        {
            var category = _category.GetbyId(id);
            if (category == null) return NotFound();
            categoryVM newcat = new categoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                largeCategoryId = category.largeCategoryId,

            };

            return View(newcat);
        }
        [HttpPost]
        public IActionResult SaveEdit(categoryVM categoryvm, IFormFile Image)
        {
            var cate = _category.GetbyId(categoryvm.Id);
            if (cate == null) return NotFound();
            cate.Name = categoryvm.Name;
            cate.largeCategoryId = categoryvm.largeCategoryId;

            var files = Request.Form.Files;
            if (files.Any())
            {
                var wwwroot = _hostEnvironment.WebRootPath + "/img/gallery/";
                Guid id = Guid.NewGuid();
                string fullpath = Path.Combine(wwwroot, id + Image.FileName);
                using (var filestream = new FileStream(fullpath, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                categoryvm.Image = id + Image.FileName;
            }
            cate.Image = categoryvm.Image;

            conteext.Entry(cate).State = EntityState.Modified;
            conteext.SaveChanges();

            if (cate.largeCategoryId == 1)
            {
                return RedirectToAction("Index");
            }
            else if (cate.largeCategoryId == 2)
            {
                return RedirectToAction("IndexM");

            }
            else
            {
                return RedirectToAction("IndexKids");

            }
        }
        //Delete
        public IActionResult Delete(int id)
        {
            var category = _category.GetbyId(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var category = _category.GetbyId(id);
            if (category == null) return NotFound();
            _category.Delete(id);
            if (category.largeCategoryId == 1)
            {
                return RedirectToAction("Index");
            }
            else if (category.largeCategoryId == 2)
            {
                return RedirectToAction("IndexM");

            }
            else
            {
                return RedirectToAction("IndexKids");

            }
        }
        public IActionResult IndexAdmin()
        {
            var item = _category.GetAll();
            return View(item);
        }
    }
}
