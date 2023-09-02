using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.SliderDTOs
{
    public class SliderCreateDto : BaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
