using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.CategoryDTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile BackgroundImageFile { get; set; }
        public IFormFile PosterImageFile { get; set; }


        public int LanguageId { get; set; }
    }
}
