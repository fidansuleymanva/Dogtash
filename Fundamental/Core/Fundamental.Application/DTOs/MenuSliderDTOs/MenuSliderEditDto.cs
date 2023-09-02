using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.MenuSliderDTOs
{
    public class MenuSliderEditDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubCategoryId { get; set; }
        public string Icon { get; set; }
    }
}
