using Dogtas.MVC.Localization.Services;
using Dogtas.MVC.ViewModels;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Controllers
{
    public class SubCategoryController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IStorage _storage;
        private static string _imagePath = "/uploads/categories/";
        private static string _subCategoryImagePath = "/uploads/subcategories/";
        public SubCategoryController(LanguageService localization, IUnitOfWork unitOfWork, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

    }
}
