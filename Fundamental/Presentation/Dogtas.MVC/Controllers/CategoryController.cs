using Dogtas.MVC.Localization.Services;
using Dogtas.MVC.ViewModels;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IStorage _storage;
        private static string _imagePath = "/uploads/categories/";
        private static string _subCategoryImagePath = "/uploads/subcategories/";
        public CategoryController(ILogger<HomeController> logger, LanguageService localization, IUnitOfWork unitOfWork, IStorage storage)
        {
            _localization = localization;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _storage = storage;
        }


        public async Task<IActionResult> Index(int id)
        {
            var category = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == id,false, "SubCategories");
            if (category == null) return RedirectToAction("NotFound", "Page");

            category.BackgroundImage = _storage.GetUrl(_imagePath, category.BackgroundImage);
            category.PosterImage = _storage.GetUrl(_imagePath, category.PosterImage);

            category.SubCategories.ForEach(x => x.PosterImage = _storage.GetUrl(_imagePath, x.PosterImage));

            CategoryViewModel model = new CategoryViewModel()
            {
                Category = category
            };
            
            return View(model);
        }

        private void SliderImagePath(List<SubCategory> list)
        {
            list.ForEach(x => x.PosterImage = _storage.GetUrl(_subCategoryImagePath, x.PosterImage));
        }
    }
}
