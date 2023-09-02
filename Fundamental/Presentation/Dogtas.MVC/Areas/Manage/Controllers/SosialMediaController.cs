using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.PagenationDTOs;
using Fundamental.Application.DTOs.SosialMediaDTOs;
using Fundamental.Application.Helper.StaticValues;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SosialMediaController : Controller
    {
        private LanguageService _localization;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IStorage _storage;
        public SosialMediaController(LanguageService localization, IUnitOfWork unitOfWork, IMapper mapper, IStorage storage)
        {
            _localization = localization;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storage = storage;
        }
        public IActionResult Index(int page = 1, string searchWord = null, string isDeleted = "null", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositorySosialMedia.GetAllAsync(x => true, false, "Language");

           
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            if (isDeleted == "false") controllerQuery = controllerQuery.Where(x => x.IsDeleted == false);
            if (isDeleted == "true") controllerQuery = controllerQuery.Where(x => x.IsDeleted == true);

            var list = PagenatedListDto<SosialMedia>.Save(controllerQuery.OrderByDescending(x => x.UpdateAt).ThenByDescending(x => x.CreatedAt), page, ControllerStatic.PageCount);

            TempData["Title"] = _localization.Getkey("Sosial Media").Value;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            ViewBag.Word = searchWord;
            TempData["Page"] = page;
            ViewBag.IsDeleted = isDeleted;
            ViewBag.LanguageId = languageId;

            return View(list);
        }
        public IActionResult Create()
        {
            TempData["Title"] = _localization.Getkey("Sosial Media").Value;
            var languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            ViewBag.Languages = languages;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SosialMediaCreateDto createDto)
        {
            TempData["Title"] = _localization.Getkey("Sosial Media").Value;

            if (ModelState.IsValid)
            {
                var controllerObject = _mapper.Map<SosialMedia>(createDto);
                controllerObject.Title = createDto.Title; 

                await _unitOfWork.RepositorySosialMedia.InsertAsync(controllerObject);
                await _unitOfWork.CommitAsync();

                return RedirectToAction("Index");
            }

            
            var languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();
            ViewBag.Languages = languages;
            return View(createDto);
        }


        public async Task<IActionResult> Edit(int id)
        {
            TempData["Title"] = _localization.Getkey("Sosial Media").Value;

            var controllerObject = await _unitOfWork.RepositorySosialMedia.GetAsync(x => x.Id == id);
            if (controllerObject == null)
            {
                return RedirectToAction("Page", "NotFound");
            }

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var editDto = _mapper.Map<SosialMediaEditDto>(controllerObject);

            return View(editDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SosialMediaEditDto editDto)
        {
            TempData["Title"] = _localization.Getkey("Sosial Media").Value;

            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            var existControllerObject = await _unitOfWork.RepositorySosialMedia.GetAsync(x => x.Id == editDto.Id);
            if (existControllerObject == null)
            {
                return RedirectToAction("Page", "NotFound");
            }

            existControllerObject.URL = editDto.URL;
            existControllerObject.Icon = editDto.Icon;
            existControllerObject.Title = editDto.Title; // Add this line to update the Title property

            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var controllerObject = await _unitOfWork.RepositorySosialMedia.GetAsync(x => x.Id == id);
            if (controllerObject != null)
            {
                controllerObject.IsDeleted = true;
                await _unitOfWork.CommitAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Recover(int id)
        {
            var controllerObject = await _unitOfWork.RepositorySosialMedia.GetAsync(x => x.Id == id);
            if (controllerObject != null)
            {
                controllerObject.IsDeleted = false;
                await _unitOfWork.CommitAsync();
            }

            return RedirectToAction("Index");
        }


    }
}
