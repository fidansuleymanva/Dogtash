using Fundamental.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Domain.Entities
{
    public class SosialMedia : BaseEntity
    {
        public string URL { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
    }
}
