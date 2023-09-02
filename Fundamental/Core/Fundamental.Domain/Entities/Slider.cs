using Fundamental.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Domain.Entities
{
    public class Slider : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
    }
}
