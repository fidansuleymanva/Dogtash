using AutoMapper;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence.Services.PresentationServices
{
    public class LayoutService
    {

        IUnitOfWork _unitOfWork;
        IStorage _storage;
        IConfiguration _configuration;
        static string _categoryImagePath = "/uploads/categories/";
        public LayoutService(IUnitOfWork unitOfWork, IStorage storage, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
            _configuration = configuration;
        }

        public string GetCurrentCulture() => Thread.CurrentThread.CurrentUICulture.Name;
        public async Task<Language> GetLanguage(string culture) => await _unitOfWork.RepositoryLanguage.GetAsync(x => x.Code.Equals(culture));
        public async Task<List<Category>> GetCategories()
        {
            var culture = GetCurrentCulture();
            var language = await GetLanguage(culture);
            var categories = _unitOfWork.RepositoryCategory.GetAllAsync(x => x.IsDeleted == false && x.LanguageId == language.Id,false,"SubCategories").ToList();
            categories.ForEach(x => x.BackgroundImage = _storage.GetUrl(_categoryImagePath,x.BackgroundImage));
            return categories;
        }

        public List<SosialMedia> GetSocialMedia(int languageId)
        {
            var activeSocialMedia = _unitOfWork.RepositorySosialMedia
                .GetAllAsync(x => x.LanguageId == languageId && !x.IsDeleted)
                .ToList();

            
            foreach (var socialMedia in activeSocialMedia)
            {
                socialMedia.Icon = _configuration["SocialMedia:BaseUrl"] + socialMedia.Icon;
            }

            return activeSocialMedia;
        }



    }
}
