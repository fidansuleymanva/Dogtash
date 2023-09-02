using Fundamental.Application.Repositories;
using Fundamental.Domain.Entities;
using Fundamental.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MainContext mainContext) : base(mainContext)
        {
        }
    }
}
