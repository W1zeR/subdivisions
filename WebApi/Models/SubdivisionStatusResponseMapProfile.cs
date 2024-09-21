using AutoMapper;
using WebApi.Entity;

namespace WebApi.Models
{
    public class SubdivisionStatusResponseMapProfile : Profile
    {
        public SubdivisionStatusResponseMapProfile()
        {
            CreateMap<Subdivision, SubdivisionStatusResponse>();
        }
    }
}
