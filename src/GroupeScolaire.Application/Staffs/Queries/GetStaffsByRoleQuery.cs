using MediatR;

namespace GroupeScolaire.Application.Staffs.Queries.GetStaffsByRole;

public record GetStaffsByRoleQuery(string Role) : IRequest<List<StaffDto>>;

public record StaffDto(Guid Id, string Nom, string Prenom, string Role);