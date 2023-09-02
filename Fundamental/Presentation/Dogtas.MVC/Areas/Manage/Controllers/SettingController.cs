using AutoMapper;
using Dogtas.MVC.Localization.Services;
using Fundamental.Application.DTOs.PagenationDTOs;
using Fundamental.Application.DTOs.SettingDTOs;
using Fundamental.Application.Helper.StaticValues;
using Fundamental.Application.Services.EntityServices.SettingServices;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        ISettingService _settingService;
        IMapper _mapper;
        static string storagePath = "/uploads/settings/";
        IUnitOfWork _unitOfWork;
        LanguageService _languageService;
        public SettingController(ISettingService settingService, IMapper mapper, IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _settingService = settingService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _languageService = languageService;
        }

        public ActionResult Index(int pageIndex = 1,string searchWord = "", int? languageId = null)
        {
            var controllerQuery = _unitOfWork.RepositorySetting.GetAllAsync(x=>x.Key.Contains(searchWord),false);
            if (languageId != null) controllerQuery = controllerQuery.Where(x => x.LanguageId == languageId);
            var pageList = PagenatedListDto<Setting>.Save(controllerQuery, pageIndex, ControllerStatic.PageCount);

            ViewBag.Word = searchWord;
            TempData["Title"] = _languageService["Settings"];
            TempData["Page"] = pageIndex;
            ViewBag.LanguageId = languageId;
            ViewBag.Languages = _unitOfWork.RepositoryLanguage.GetAllAsync(x => true).ToList();

            return View(pageList);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var controllerEntity = await _settingService.GetEntityForEdit(id);
            TempData["Title"] = _languageService["Settings"];

            return View(controllerEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SettingEditDto settingditDto)
        {
            var controllerEntityEditDto = await _settingService.EditEntity(settingditDto.Id,settingditDto);
            TempData["Title"] = _languageService["Settings"];
            return RedirectToAction("Index");
            //int page = (int)TempData["Page"];
            //return RedirectToAction("Index",new { page = page});
        }

    }
}
