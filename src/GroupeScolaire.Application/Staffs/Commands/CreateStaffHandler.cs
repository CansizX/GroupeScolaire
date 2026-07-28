using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Domain.Entities;
using MediatR;

namespace GroupeScolaire.Application.Staffs.Commands.CreateStaff;

public class CreateStaffHandler : IRequestHandler<CreateStaffCommand, Guid>
{
    private readonly IEtablissementDbContext _context;

    public CreateStaffHandler(IEtablissementDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var staff = new Staff
        {
            Id = Guid.NewGuid(),
            Nom = request.Nom,
            Prenom = request.Prenom,
            Role = request.Role
        };

        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync(cancellationToken);

        return staff.Id;
    }
}