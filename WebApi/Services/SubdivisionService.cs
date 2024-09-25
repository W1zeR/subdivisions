using AutoMapper;
using FluentValidation;
using WebApi.Entity;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class SubdivisionService(ISubdivisionRepository repository, IMapper mapper,
        IValidator<SubdivisionRequest> validator) : ISubdivisionService
    {
        public async Task Add(SubdivisionRequest subdivisionRequest)
        {
            await Validate(subdivisionRequest);
            Subdivision subdivision = mapper.Map<Subdivision>(subdivisionRequest);
            await repository.Add(subdivision);
        }

        public async Task<List<SubdivisionResponse>> GetAll()
        {
            List<Subdivision> subdivisions = await repository.GetAll();
            return mapper.Map<List<SubdivisionResponse>>(subdivisions);
        }

        public async Task<SubdivisionResponse> GetById(int id)
        {
            Subdivision? subdivision = await repository.GetById(id);
            return subdivision == null
                ? throw new SubdivisionException($"Подразделение с идентификатором '{id}' не найдено")
                : mapper.Map<SubdivisionResponse>(subdivision);
        }

        public async Task Update(int id, SubdivisionRequest subdivisionRequest)
        {
            await Validate(subdivisionRequest);
            Subdivision subdivision = await repository.GetById(id)
                ?? throw new SubdivisionException($"Подразделение с идентификатором '{id}' не найдено");
            subdivision = mapper.Map(subdivisionRequest, subdivision);
            await repository.Update(subdivision);
        }

        private async Task Validate(SubdivisionRequest request)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
