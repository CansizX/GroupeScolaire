using FluentAssertions;
using GroupeScolaire.Application.Staffs.Queries.GetStaffsByRole;
using GroupeScolaire.Application.Tests.Common;
using GroupeScolaire.Domain.Entities;
using Xunit;

namespace GroupeScolaire.Application.Tests.Staffs;

public class GetStaffsByRoleHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnOnlyStaffsWithMatchingRole()
    {
        var context = TestDbContextFactory.Create();
        context.Staffs.AddRange(
            new Staff { Id = Guid.NewGuid(), Nom = "A", Prenom = "A", Role = "Prof" },
            new Staff { Id = Guid.NewGuid(), Nom = "B", Prenom = "B", Role = "Admin" },
            new Staff { Id = Guid.NewGuid(), Nom = "C", Prenom = "C", Role = "Prof" }
        );
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetStaffsByRoleHandler(context);
        var result = await handler.Handle(new GetStaffsByRoleQuery("Prof"), CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().OnlyContain(s => s.Role == "Prof");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoStaffMatchesRole()
    {
        var context = TestDbContextFactory.Create();
        var handler = new GetStaffsByRoleHandler(context);

        var result = await handler.Handle(new GetStaffsByRoleQuery("Inexistant"), CancellationToken.None);

        result.Should().BeEmpty();
    }
}