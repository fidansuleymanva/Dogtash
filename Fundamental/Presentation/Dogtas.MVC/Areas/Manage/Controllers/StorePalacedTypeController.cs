using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.CategoryDTOs;
using Fundamental.Application.DTOs.PagenationDTOs;
using Fundamental.Application.DTOs.StorePalacedTypeDTOs;
using Fundamental.Application.Helper.StaticValues;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class StorePalacedTypeController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        public StorePalacedTypeController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }

        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositoryStorePalacedType.GetAllAsync(x => true, false, "Language");

            if (searchWord != null) controllerQuery = controllerQuery.Where(x => x.Name.Contains(searchWord));
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<StorePalacedType>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Store Palaced Type").Value;
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            ViewBag.Word = searchWord;
            TempData["Page"] = page;
            ViewBag.IsDeleted = isDeleted;
            ViewBag.LanguageId = languageId;

            return View(list);
        }

        public IActionResult Create()
        {
            TempData["Title"] = _localization.Getkey("Store Palaced Type").Value;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StorePalacedTypeCreateDto createDto)
        {
            var existObject = await _unitOfWork.RepositoryStorePalacedType.GetAsync(x => x.Name == createDto.Name && x.LanguageId == createDto.LanguageId);
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            if (existObject != null)
            {
                ModelState.AddModelError("Name", _localization["Name Error Message"]);
                return View(createDto);
            }

            var controllerObject = _mapper.Map<StorePalacedType>(createDto);

            await _unitOfWork.RepositoryStorePalacedType.InsertAsync(controllerObject);
            await _unitOfWork.CommitAsync();

            TempData["Title"] = _localization.Getkey("Store Palaced Type").Value;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Store Palaced Type").Value;

            var controllerObject = await _unitOfWork.RepositoryStorePalacedType.GetAsync(x => x.Id == id);
            if (controllerObject == null) return RedirectToAction("page", "notfound");

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var editDto = _mapper.Map<StorePalacedTypeEditDto>(controllerObject);

            return View(editDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StorePalacedTypeEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Store Palaced Type").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var existControllerObject = await _unitOfWork.RepositoryStorePalacedType.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null) return RedirectToAction("page", "notfound");

            var languageObject = await _unitOfWork.RepositoryLanguage.GetAsync(x => x.Id == editDto.LanguageId);
            if (languageObject == null) return RedirectToAction("page", "notfound");

            existControllerObject.Name = editDto.Name;
            existControllerObject.LanguageId = editDto.LanguageId;

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryStorePalacedType.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = true;
            controllerObject.Stores.ForEach(x => x.IsDeleted = true);
            await _unitOfWork.CommitAsync();
        }
        public async Task Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryStorePalacedType.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = false;
            controllerObject.Stores.ForEach(x => x.IsDeleted = false);
            await _unitOfWork.CommitAsync();
        }

        public List<StorePalacedType> GetAll(int languageId)
        => _unitOfWork.RepositoryStorePalacedType.GetAllAsync(x => x.LanguageId == languageId).ToList();

    }
}
