using Fundamental.Application.Repositories;
using Fundamental.Domain.Entities;

namespace Fundamental.Application.UnitOfWorks
{
    public interface IUnitOfWork
	{
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

        Task<int> CommitAsync();
	}
}
