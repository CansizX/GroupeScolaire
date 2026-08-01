using FluentAssertions;
using GroupeScolaire.Application.Common.Interfaces;
using GroupeScolaire.Application.Presences.Commands.CreatePresence;
using GroupeScolaire.Application.Tests.Common;
using Moq;
using Xunit;

namespace GroupeScolaire.Application.Tests.Presences;

public class CreatePresenceHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreatePresence_AndNotifyWithCorrectTenant()
    {
        // Arrange
        var context = TestDbContextFactory.Create();
        var tenantId = Guid.NewGuid();

        var tenantProviderMock = new Mock<ITenantProvider>();
        tenantProviderMock.Setup(t => t.TenantId).Returns(tenantId);

        var notifierMock = new Mock<IPresenceNotifier>();

        var handler = new CreatePresenceHandler(context, tenantProviderMock.Object, notifierMock.Object);
        var personneId = Guid.NewGuid();
        var command = new CreatePresenceCommand(personneId, "Eleve", "Present");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();

        var presenceInDb = context.Presences.FirstOrDefault(p => p.Id == result);
        presenceInDb.Should().NotBeNull();
        presenceInDb!.Statut.Should().Be("Present");

        // Vérifie que la notification a été envoyée avec le bon tenant (en minuscules, cf. bug de casse résolu)
        notifierMock.Verify(
            n => n.NotifyPresenceCreated(
                tenantId.ToString().ToLowerInvariant(),
                result,
                personneId,
                "Present"),
            Times.Once
        );
    }

    [Fact]
    public async Task Handle_ShouldUseEmptyTenantId_WhenTenantProviderReturnsNull()
    {
        var context = TestDbContextFactory.Create();

        var tenantProviderMock = new Mock<ITenantProvider>();
        tenantProviderMock.Setup(t => t.TenantId).Returns((Guid?)null);

        var notifierMock = new Mock<IPresenceNotifier>();

        var handler = new CreatePresenceHandler(context, tenantProviderMock.Object, notifierMock.Object);
        var command = new CreatePresenceCommand(Guid.NewGuid(), "Eleve", "Present");

        await handler.Handle(command, CancellationToken.None);

        notifierMock.Verify(
            n => n.NotifyPresenceCreated(string.Empty, It.IsAny<Guid>(), It.IsAny<Guid>(), "Present"),
            Times.Once
        );
    }
}