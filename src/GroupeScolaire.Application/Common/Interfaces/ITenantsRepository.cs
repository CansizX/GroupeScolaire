using GroupeScolaire.Domain.Entities;

namespace GroupeScolaire.Application.Common.Interfaces;

public interface ITenantsRepository
{
    Tenant? GetById(Guid id);
}