using CloudinaryDotNet;
using Dogtas.MVC.Languages;
using Dogtas.MVC.Localization.Services;
using Dogtas.MVC.ViewModels;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Dogtas.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IStorage _storage;

        static string _sliderImagePath = "/uploads/sliders/";

        public HomeController(ILogger<HomeController> logger, LanguageService localization, IUnitOfWork unitOfWork, IStorage storage)
        {
            _localization = localization;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

        public async Task<IActionResult> Index()
        {
            //todo culture view-da deyisilmelidi amma problem deyil
            //get culture information
            
            
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            ViewBag.CurrentCulture = currentCulture;

            var languageId = _unitOfWork.RepositoryLanguage.GetAsync(x => x.Code.Equals(currentCulture)).Result.Id;
            var sliders = _unitOfWork.RepositorySlider.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == languageId).ToList();
            var categories = _unitOfWork.RepositoryCategory.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == languageId).ToList();
            var menuSliders = _unitOfWork.RepositoryMenuSlider.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == languageId).ToList();

            SliderImagePath(sliders);

            HomeViewModel model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories,
                MenuSliders = menuSliders
            };

            return View(model);
        }

        private void SliderImagePath(List<Slider> list)
        {
            list.ForEach(x => x.Image = _storage.GetUrl(_sliderImagePath, x.Image));
        }

    }
}