using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.PagenationDTOs
{
    public class PagenationDto
    {
        public int TotalPage { get; set; }

        public int PageIndex { get; set; }

        public bool HasPrev
        {
            get => PageIndex > 1;
        }

        public bool HasNext
        {
            get => TotalPage > PageIndex;
        }
    }
}
