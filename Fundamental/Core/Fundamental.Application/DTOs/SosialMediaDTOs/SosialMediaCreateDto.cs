using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.SosialMediaDTOs
{
    public class SosialMediaCreateDto : BaseDto
    {
        public string URL { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
    }
}
