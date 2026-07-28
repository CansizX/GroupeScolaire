using MediatR;

namespace GroupeScolaire.Application.Staffs.Commands.CreateStaff;

public record CreateStaffCommand(
    string Nom,
    string Prenom,
    string Role
) : IRequest<Guid>;