using AutoMapper;
using BUSINESS_LOGIC.Dtos;
using DAL.Models;


namespace CORE.Infrastructure.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ImportDataViewModel, Product>()
                 .ForMember(x => x.CreatedOn, src => src.Ignore())
                .ForMember(x => x.ArticleId, src => src.Ignore())
                .ForMember(x => x.Article, src => src.Ignore())
                .ForMember(x => x.ColorId, src => src.Ignore())
                .ForMember(x => x.Color, src => src.Ignore());

        }
    }
}
