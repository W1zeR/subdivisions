using AutoMapper;
using WebApi.Entity;

namespace WebApi.Models
{
    public class SubdivisionResponseMapProfile : Profile
    {
        public SubdivisionResponseMapProfile()
        {
            CreateMap<Subdivision, SubdivisionResponse>();
        }
    }
}
