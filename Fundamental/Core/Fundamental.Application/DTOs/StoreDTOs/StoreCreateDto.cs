﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.DTOs.StoreDTOs
{
    public class StoreCreateDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string Link { get; set; }
        public string Map { get; set; }


        public int StorePalacedTypeId { get; set; }
    }
}
