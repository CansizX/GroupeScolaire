using FluentAssertions;
using GroupeScolaire.Application.Eleves.Commands.CreateEleve;
using GroupeScolaire.Application.Tests.Common;
using Xunit;

namespace GroupeScolaire.Application.Tests.Eleves;

public class CreateEleveHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateEleve_AndReturnItsId()
    {
        // Arrange
        var context = TestDbContextFactory.Create();
        var handler = new CreateEleveHandler(context);
        var command = new CreateEleveCommand(
            Nom: "Benali",
            Prenom: "Yassine",
            DateNaissance: new DateOnly(2015, 3, 12),
            ClasseId: Guid.NewGuid()
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();

        var eleveInDb = context.Eleves.FirstOrDefault(e => e.Id == result);
        eleveInDb.Should().NotBeNull();
        eleveInDb!.Nom.Should().Be("Benali");
        eleveInDb.Prenom.Should().Be("Yassine");
    }

    [Theory]
    [InlineData("", "Prenom valide")]
    [InlineData("Nom valide", "")]
    public async Task Handle_ShouldStillCreateEleve_EvenWithEmptyNameFields(string nom, string prenom)
    {
        // Ce test documente le comportement ACTUEL (pas de validation métier pour l'instant)
        // Arrange
        var context = TestDbContextFactory.Create();
        var handler = new CreateEleveHandler(context);
        var command = new CreateEleveCommand(nom, prenom, new DateOnly(2015, 1, 1), Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        // Note: si tu ajoutes FluentValidation plus tard, ce test devra être mis à jour
        // pour vérifier qu'une exception de validation est levée à la place
    }
}