using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.CategoryDTOs
{
    public class CategoryEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile BackgroundImageFile { get; set; }
        public IFormFile PosterImageFile { get; set; }


        public string PosterImage { get; set; }
        public string BackgroundImage { get; set; }

        public int LanguageId { get; set; }
    }
}
