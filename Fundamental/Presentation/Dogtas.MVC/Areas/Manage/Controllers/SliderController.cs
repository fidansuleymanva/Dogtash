using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.CategoryDTOs;
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
    public class SliderController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        static string _imagePath = "/uploads/sliders/";
        public SliderController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }
        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositorySlider.GetAllAsync(x => true, false, "Language");

            if (searchWord != null) controllerQuery = controllerQuery.Where(x => x.Name.Contains(searchWord));
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<Slider>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Sliders").Value;
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            ViewBag.Word = searchWord;
            TempData["Page"] = page;
            ViewBag.IsDeleted = isDeleted;
            ViewBag.LanguageId = languageId;

            ImageUrlAdd(list);

            return View(list);
        }

        public IActionResult Create()
        {
            TempData["Title"] = _localization.Getkey("Slider").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateDto createDto)
        {
            TempData["Title"] = _localization.Getkey("Slider").Value;

            var existObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Name == createDto.Name && x.LanguageId == createDto.LanguageId);
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            if (existObject != null)
            {
                ModelState.AddModelError("Name", _localization["Name Error Message"]);
                return View(createDto);
            }

            if(createDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", _localization["Image Error Message"]);
                return View(createDto);
            }

            var imageInfo = await _storage.UploadAsync(_imagePath, createDto.ImageFile);
            var controllerObject = _mapper.Map<Slider>(createDto);
            controllerObject.Image = imageInfo.fileName;


            await _unitOfWork.RepositorySlider.InsertAsync(controllerObject);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Sliders").Value;

            var controllerObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Id == id);
            if (controllerObject == null) return RedirectToAction("page", "notfound");

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var editDto = _mapper.Map<SliderEditDto>(controllerObject);
            editDto.Image = _storage.GetUrl(_imagePath, controllerObject.Image);

            return View(editDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SliderEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Slider").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var existControllerObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null) return RedirectToAction("page", "notfound");

            var languageObject = await _unitOfWork.RepositoryLanguage.GetAsync(x => x.Id == editDto.LanguageId);
            if (languageObject == null) return RedirectToAction("page", "notfound");

            if(editDto.ImageFile != null)
            {
                //todo change to method
                var imageChecker = _storage.HasFile(_imagePath, existControllerObject.Image);
                if (imageChecker != null) await _storage.DeleteAsync(_imagePath, existControllerObject.Image);
                var imageInfo = await _storage.UploadAsync(_imagePath, editDto.ImageFile);
                existControllerObject.Image = imageInfo.fileName;
            }   

            existControllerObject.Name = editDto.Name;
            existControllerObject.LanguageId = editDto.LanguageId;

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }
        public async Task Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }
        public async Task Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositorySlider.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = false;
            await _unitOfWork.CommitAsync();
        }

        private void ImageUrlAdd(List<Slider> list)
        {
            list.ForEach(x => x.Image = _storage.GetUrl(_imagePath, x.Image));
        }

    }
}
