using FluentAssertions;
using GroupeScolaire.Application.Staffs.Commands.CreateStaff;
using GroupeScolaire.Application.Tests.Common;
using Xunit;

namespace GroupeScolaire.Application.Tests.Staffs;

public class CreateStaffHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateStaff_AndReturnItsId()
    {
        var context = TestDbContextFactory.Create();
        var handler = new CreateStaffHandler(context);
        var command = new CreateStaffCommand("Alaoui", "Karim", "Prof");

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        var staffInDb = context.Staffs.FirstOrDefault(s => s.Id == result);
        staffInDb.Should().NotBeNull();
        staffInDb!.Role.Should().Be("Prof");
    }

    [Theory]
    [InlineData("Prof")]
    [InlineData("Admin")]
    [InlineData("Direction")]
    public async Task Handle_ShouldAcceptAnyRoleString_NoValidationYet(string role)
    {
        var context = TestDbContextFactory.Create();
        var handler = new CreateStaffHandler(context);
        var command = new CreateStaffCommand("Test", "Test", role);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
    }
}