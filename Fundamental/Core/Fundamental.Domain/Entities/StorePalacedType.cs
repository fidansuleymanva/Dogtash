using Fundamental.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Domain.Entities
{
    public class StorePalacedType : BaseEntity
    {
        public string Name { get; set; }
        public List<Store> Stores { get; set; }
    }
}
