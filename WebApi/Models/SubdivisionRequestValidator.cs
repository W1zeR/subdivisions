using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Entity;

namespace WebApi.Models
{
    public class SubdivisionRequestValidator : AbstractValidator<SubdivisionRequest>
    {
        private readonly AppDbContext context;

        public SubdivisionRequestValidator(AppDbContext context)
        {
            this.context = context;

            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellation) =>
                {
                    bool exists = await IsNameExists(name);
                    return !exists;
                })
                .WithMessage("Наименование подразделения должно быть уникальным");

            RuleFor(x => x.MainId)
                .MustAsync(async (id, cancellation) =>
                {
                    if (id == null)
                    {
                        return true;
                    }
                    return await IsIdExists((int)id);
                })
                .WithMessage($"Некорректное значение идентификатора главного подразделения");
        }

        private async Task<bool> IsNameExists(string name)
        {
            Subdivision? subdivision = await context.Subdivisions
                .Where(s => s.Name.ToLower() == name.ToLower())
                .SingleOrDefaultAsync();
            return subdivision != null;
        }

        private async Task<bool> IsIdExists(int id)
        {
            Subdivision? subdivision = await context.Subdivisions.FindAsync(id);
            return subdivision != null;
        }
    }
}
