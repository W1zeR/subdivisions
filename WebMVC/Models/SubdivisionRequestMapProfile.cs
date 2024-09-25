using AutoMapper;

namespace WebMVC.Models
{
    public class SubdivisionRequestMapProfile : Profile
    {
        public SubdivisionRequestMapProfile()
        {
            CreateMap<Subdivision, SubdivisionRequest>();
        }
    }
}
