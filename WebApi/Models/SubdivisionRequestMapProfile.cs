using AutoMapper;
using WebApi.Entity;

namespace WebApi.Models
{
    public class SubdivisionRequestMapProfile : Profile
    {
        public SubdivisionRequestMapProfile()
        {
            CreateMap<SubdivisionRequest, Subdivision>();
        }
    }
}
