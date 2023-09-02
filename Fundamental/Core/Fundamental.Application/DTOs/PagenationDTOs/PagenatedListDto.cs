using Fundamental.Application.DTOs.PagenationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.PagenationDTOs
{
    public class PagenatedListDto<T> : List<T>
    {
        public PagenatedListDto(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.AddRange(items);

            PagenationDto = new PagenationDto()
            {
                PageIndex = pageIndex,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize)
            };

            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PagenationDto PagenationDto { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public static PagenatedListDto<T> Save(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagenatedListDto<T>(items, query.Count(), pageIndex, pageSize);
        }
    }
}
