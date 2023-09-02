using AutoMapper;
using Fundamental.Application.DTOs.SettingDTOs;
using Fundamental.Application.Helper.FileCheckers;
using Fundamental.Application.Helper.Managers;
using Fundamental.Application.Repositories;
using Fundamental.Application.Services;
using Fundamental.Application.Services.EntityServices.SettingServices;
using Fundamental.Application.Storages;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence.Services.EntityServices.SettingServices
{
    public class SettingService : ISettingService
    {
        IUnitOfWork _unitOfWork;
        IFileChecker _fileChecker;
        IStorage _storage;
        IMapper _mapper;
        static string storagePath = "/uploads/settings/";
        public SettingService(IUnitOfWork unitOfWork, IFileChecker fileChecker, IStorage storage, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileChecker = fileChecker;
            _storage = storage;
            _mapper = mapper;
        }

        public async Task<object> EditEntity(int id,object editDto)
        {
            var existSetting = await _unitOfWork.RepositorySetting.GetAsync(x => x.Id == id);
            if (existSetting == null) throw new Exception("yoxdu");

            SettingEditDto settingEditDto = (SettingEditDto)editDto;

            if (existSetting.Key.Contains("image") && settingEditDto.ImageFile != null)
            {
                _fileChecker.CheckImageFile(settingEditDto.ImageFile,ContentTypeManager.ImageContentTypes);
                var displayTuple = await _storage.UploadAsync(storagePath,settingEditDto.ImageFile);
                existSetting.Value = displayTuple.fileName;
                settingEditDto.Value = HttpService.StorageUrl(storagePath, existSetting.Value);
            }
            else
                existSetting.Value = settingEditDto.Value;

            settingEditDto = _mapper.Map<SettingEditDto>(existSetting);
            
            await _unitOfWork.CommitAsync();
            return settingEditDto;
        }

        public IQueryable<Setting> GetEntities() => _unitOfWork.RepositorySetting.GetAllAsync(x => true);
        public IQueryable<Setting> GetEntities(string word)
        {
            if(!string.IsNullOrWhiteSpace(word)) return _unitOfWork.RepositorySetting.GetAllAsync(x => x.Key.ToLower().Contains(word.ToLower()));
            return GetEntities();
        }

        public async Task<Setting> GetEntityById(int id) => await _unitOfWork.RepositorySetting.GetAsync(x => x.Id.Equals((int)id));
        public async Task<Setting> GetSetting(string key) => await _unitOfWork.RepositorySetting.GetAsync(x => x.Key.Equals(key));

       
        public async Task<object> GetEntityForEdit(int id)
        {
            var existSetting = await _unitOfWork.RepositorySetting.GetAsync(x => x.Id == id);
            if(existSetting.Key.Contains("image"))
            {
                existSetting.Value = HttpService.StorageUrl(storagePath, existSetting.Value);
            }
            //existSetting.Value = existSetting.Key.Contains("image") ? HttpService.StorageUrl(storagePath, existSetting.Value) : existSetting.Value;
            SettingEditDto settingEditDto = _mapper.Map<SettingEditDto>(existSetting);
            return settingEditDto;
        }
    }
}
