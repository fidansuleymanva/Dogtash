using Fundamental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.Services.EntityServices
{
    public interface IEntityService<T> where T : class
    {
        Task<T> GetEntityById(int id);
        Task<object> GetEntityForEdit(int id);
        Task<object> EditEntity(int id, object editDto);
        IQueryable<T> GetEntities();
        IQueryable<T> GetEntities(string word);
    }
}
