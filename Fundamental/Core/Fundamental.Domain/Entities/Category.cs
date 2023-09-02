using Fundamental.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Domain.Entities
{
    public class Category : BaseEntity
    { 
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string BackgroundImage { get; set; }
        public string PosterImage { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
}
