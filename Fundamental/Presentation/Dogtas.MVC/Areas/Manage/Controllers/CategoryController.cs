using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.CategoryDTOs;
using Fundamental.Application.DTOs.PagenationDTOs;
using Fundamental.Application.Helper.StaticValues;
using Fundamental.Application.Repositories;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Localization;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        private string _imagePath = "/uploads/categories/";
        public CategoryController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }

        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositoryCategory.GetAllAsync(x => true, false, "Language");

            if (searchWord != null) controllerQuery = controllerQuery.Where(x => x.Name.Contains(searchWord));
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<Category>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Categories").Value;
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
            TempData["Title"] = _localization.Getkey("Categories").Value;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto createDto)
        {
            var existObject = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Name == createDto.Name && x.LanguageId == createDto.LanguageId);
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            if (existObject != null)
            {
                ModelState.AddModelError("Name", _localization["Name Error Message"]);
                return View(createDto);
            }

            if(createDto.PosterImageFile == null)
            {
                ModelState.AddModelError("PosterImageFile", _localization["Poster Image File Error Message"]);
                return View(createDto);
            }

            if (createDto.BackgroundImageFile == null)
            {
                ModelState.AddModelError("BackgroundImageFile", _localization["Background Image File Error Message"]);
                return View(createDto);
            }

            var controllerObject = _mapper.Map<Category>(createDto);

            var posterImageInfo = await _storage.UploadAsync(_imagePath, createDto.PosterImageFile);
            controllerObject.PosterImage = posterImageInfo.fileName;

            var backgroundImageInfo = await _storage.UploadAsync(_imagePath, createDto.BackgroundImageFile);
            controllerObject.BackgroundImage = backgroundImageInfo.fileName;

            await _unitOfWork.RepositoryCategory.InsertAsync(controllerObject);
            await _unitOfWork.CommitAsync();

            TempData["Title"] = _localization.Getkey("Categories").Value;

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Categories").Value;

            var controllerObject = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == id);
            if (controllerObject == null) return RedirectToAction("page", "notfound");

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var editDto = _mapper.Map<CategoryEditDto>(controllerObject);

            editDto.PosterImage = _storage.GetUrl(_imagePath, controllerObject.PosterImage);
            editDto.BackgroundImage = _storage.GetUrl(_imagePath, controllerObject.BackgroundImage);

            return View(editDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Categories").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var existControllerObject = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null) return RedirectToAction("page", "notfound");

            var languageObject = await _unitOfWork.RepositoryLanguage.GetAsync(x => x.Id == editDto.LanguageId);
            if (languageObject == null) return RedirectToAction("page", "notfound");


            if (editDto.PosterImageFile != null)
            {
                //todo change to method
                var imageChecker = _storage.HasFile(_imagePath, existControllerObject.PosterImage);
                if (imageChecker != null) await _storage.DeleteAsync(_imagePath, existControllerObject.PosterImage);
                var imageInfo = await _storage.UploadAsync(_imagePath, editDto.PosterImageFile);
                existControllerObject.PosterImage = imageInfo.fileName;
            }

            if (editDto.BackgroundImageFile != null)
            {
                //todo change to method
                var imageChecker = _storage.HasFile(_imagePath, existControllerObject.BackgroundImage);
                if (imageChecker != null) await _storage.DeleteAsync(_imagePath, existControllerObject.BackgroundImage);
                var imageInfo = await _storage.UploadAsync(_imagePath, editDto.BackgroundImageFile);
                existControllerObject.BackgroundImage = imageInfo.fileName;
            }

            existControllerObject.Name = editDto.Name;
            existControllerObject.LanguageId = editDto.LanguageId;
            existControllerObject.Title = editDto.Title;
            existControllerObject.Description = editDto.Description;

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }
        public async Task Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = true;
            //controllerObject.SubCategories.ForEach(x => x.IsDeleted = true);
            await _unitOfWork.CommitAsync();
        }
        public async Task Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryCategory.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = false;
            //controllerObject.SubCategories.ForEach(x => x.IsDeleted = false);
            await _unitOfWork.CommitAsync();
        }

        public List<Category> GetAll(int languageId)
        => _unitOfWork.RepositoryCategory.GetAllAsync(x => x.LanguageId == languageId).ToList();


    }
}
