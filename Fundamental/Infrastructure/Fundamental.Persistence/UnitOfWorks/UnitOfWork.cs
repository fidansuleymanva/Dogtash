using Fundamental.Application.Repositories;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Fundamental.Persistence.Contexts;
using Fundamental.Persistence.Repositories;

namespace Fundamental.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {

        readonly MainContext _context;

        public UnitOfWork(MainContext context)
        {
            _context = context;
            RepositoryUser = new Repository<AppUser>(_context);
            RepositorySetting = new Repository<Setting>(_context);
            RepositoryCategory = new Repository<Category>(_context);
            RepositoryLanguage = new Repository<Language>(_context);
            RepositorySubCategory = new Repository<SubCategory>(_context);
            RepositorySlider = new Repository<Slider>(_context);
            RepositoryMenuSlider = new Repository<MenuSlider>(_context);
            RepositoryStorePalacedType = new Repository<StorePalacedType>(_context);
            RepositoryStore = new Repository<Store>(_context);
            RepositoryCollection = new Repository<Collection>(_context);
            RepositorySosialMedia = new Repository<SosialMedia>(_context);
        }

        public IRepository<AppUser> RepositoryUser { get; set; }
        public IRepository<Setting> RepositorySetting { get; set; }
        public IRepository<Category> RepositoryCategory { get; set; }
        public IRepository<Language> RepositoryLanguage { get; set; }
        public IRepository<SubCategory> RepositorySubCategory { get; set; }
        public IRepository<Slider> RepositorySlider { get; set; }
        public IRepository<MenuSlider> RepositoryMenuSlider { get; set; }
        public IRepository<StorePalacedType> RepositoryStorePalacedType { get; set; }
        public IRepository<Store> RepositoryStore { get; set; }
        public IRepository<Collection> RepositoryCollection { get; set; }
        public IRepository<SosialMedia> RepositorySosialMedia { get; set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
