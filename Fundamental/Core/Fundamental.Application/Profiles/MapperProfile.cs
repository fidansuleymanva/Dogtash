using AutoMapper;
using Fundamental.Application.DTOs.CategoryDTOs;
using Fundamental.Application.DTOs.CollectionDTOs;
using Fundamental.Application.DTOs.MenuSliderDTOs;
using Fundamental.Application.DTOs.SettingDTOs;
using Fundamental.Application.DTOs.SliderDTOs;
using Fundamental.Application.DTOs.StoreDTOs;
using Fundamental.Application.DTOs.StorePalacedTypeDTOs;
using Fundamental.Application.DTOs.SubCategoryDTOs;
using Fundamental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.Profiles
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<Setting, SettingEditDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category,CategoryEditDto>();


            CreateMap<SubCategoryCreateDto,SubCategory>();
            CreateMap<SubCategory,SubCategoryEditDto>();

            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderEditDto>();

            CreateMap<MenuSliderCreateDto, MenuSlider>();
            CreateMap<MenuSlider, MenuSliderEditDto>();

            CreateMap<StorePalacedTypeCreateDto, StorePalacedType>();
            CreateMap<StorePalacedType, StorePalacedTypeEditDto>();

            CreateMap<StoreCreateDto, Store>();
            CreateMap<Store,StoreEditDto>();
        
            CreateMap<CollectionCreateDto, Collection>();
            CreateMap<CollectionEditDto, Collection>();

        }

    }
}
