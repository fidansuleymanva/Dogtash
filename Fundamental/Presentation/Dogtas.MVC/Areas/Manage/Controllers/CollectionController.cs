using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.CollectionDTOs;
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
    public class CollectionController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        public CollectionController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }

        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null")
        {
            var controllerQuery = _unitOfWork.RepositoryCollection.GetAllAsync(x => true, false, "Language");

            if (searchWord != null) controllerQuery = controllerQuery.Where(x => x.Name.Contains(searchWord));
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<Collection>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Collection").Value;

            ViewBag.Word = searchWord;
            TempData["Page"] = page;
            ViewBag.IsDeleted = isDeleted;

            return View(list);
        }

        public IActionResult Create()
        {
            TempData["Title"] = _localization.Getkey("Collection").Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CollectionCreateDto createDto)
        {
            var existObject = await _unitOfWork.RepositoryCollection.GetAsync(x => x.Name == createDto.Name);

            if (existObject != null)
            {
                ModelState.AddModelError("Name", _localization["Name Error Message"]);
                return View(createDto);
            }

            var controllerObject = _mapper.Map<Collection>(createDto);

            await _unitOfWork.RepositoryCollection.InsertAsync(controllerObject);
            await _unitOfWork.CommitAsync();

            TempData["Title"] = _localization.Getkey("Collection").Value;

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Collection").Value;

            var controllerObject = await _unitOfWork.RepositoryCollection.GetAsync(x => x.Id == id);
            if (controllerObject == null) return RedirectToAction("page", "notfound");

            var editDto = _mapper.Map<CollectionEditDto>(controllerObject);

            return View(editDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CollectionEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Collection").Value;

            var existControllerObject = await _unitOfWork.RepositoryCollection.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null) return RedirectToAction("page", "notfound");

            existControllerObject.Name = editDto.Name;

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        public async Task Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryCollection.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = true;
            controllerObject.Products.ForEach(x => x.IsDeleted = true);
            await _unitOfWork.CommitAsync();
        }
        public async Task Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositoryCollection.GetAsync(x => x.Id == id);
            controllerObject.IsDeleted = false;
            controllerObject.Products.ForEach(x => x.IsDeleted = false);
            await _unitOfWork.CommitAsync();
        }

    }
}
