using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.CategoryDTOs;
using Fundamental.Application.DTOs.MenuSliderDTOs;
using Fundamental.Application.DTOs.PagenationDTOs;
using Fundamental.Application.DTOs.SliderDTOs;
using Fundamental.Application.Helper.StaticValues;
using Fundamental.Application.Services;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MenuSliderController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        public MenuSliderController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }
        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositoryMenuSlider.GetAllAsync(x => true, false, "Language");

            if (searchWord != null) controllerQuery = controllerQuery.Where(x => x.Name.Contains(searchWord));
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<MenuSlider>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Menu Sliders").Value;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            ViewBag.Word = searchWord;
            TempData["Page"] = page;
            ViewBag.IsDeleted = isDeleted;
            ViewBag.LanguageId = languageId;

            return View(list);
        }

        public IActionResult Create()
        {
            TempData["Title"] = _localization.Getkey("Categories").Value;
            var languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            ViewBag.Categories = _unitOfWork.RepositoryCategory.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == languages.FirstOrDefault().Id).ToList();
            ViewBag.SubCategories = _unitOfWork.RepositorySubCategory.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == languages.FirstOrDefault().Id).ToList();
            ViewBag.Languages = languages;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuSliderCreateDto createDto)
        {
            TempData["Title"] = _localization.Getkey("Menu Sliders").Value;

            var existObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Name == createDto.Name && x.LanguageId == createDto.LanguageId);
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            if (existObject != null)
            {
                ModelState.AddModelError("Name", _localization["Name Error Message"]);
                return View(createDto);
            }

            var controllerObject = _mapper.Map<MenuSlider>(createDto);


            await _unitOfWork.RepositoryMenuSlider.InsertAsync(controllerObject);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Menu Sliders").Value;

            var controllerObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Id == id);
            if (controllerObject == null) return RedirectToAction("page", "notfound");

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var editDto = _mapper.Map<MenuSliderEditDto>(controllerObject);

            return View(editDto);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MenuSliderEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Slider").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var existControllerObject = await _unitOfWork.RepositoryMenuSlider.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null) return RedirectToAction("page", "notfound");

            var languageObject = await _unitOfWork.RepositoryLanguage.GetAsync(x => x.Id == editDto.LanguageId);
            if (languageObject == null) return RedirectToAction("page", "notfound");

            existControllerObject.Name = editDto.Name;
            existControllerObject.LanguageId = editDto.LanguageId;
            existControllerObject.Icon = editDto.Icon;

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryMenuSlider.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }
        public async Task Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryMenuSlider.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

    }
}
