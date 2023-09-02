using AutoMapper;
using Fundamental.Application.Services.EntityServices.CategoryServices;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence.Services.EntityServices.CategorySevices
{
    public class CategoryService : ICategoryService
    {

        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        static string storagePath = "/uploads/settings/";
        public CategoryService(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<object> EditEntity(int id, object editDto)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetEntities()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetEntities(string word)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetEntityForEdit(int id)
        {
            throw new NotImplementedException();
        }
    }
}
