using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Commands.Handlers.Champagne;
using Domain;
using Domain.Aggregates.Champagne;
using Domain.Persistence;
using Marten;
using Moq;
using Shouldly;
using Xunit;

namespace Commands.Tests
{
    public class ChampagneHandler_Should
    {
        [Theory, AutoMoqData]
        public async Task Store_Events_When_Changing_Name_On_Existing_Champagne([Frozen]Mock<IPublishingAggregateRepository> repo, RenameChampagneCommand cmd, Champagne pagne, ChampagneHandler sut)
        {
            //Arrange
            repo.Setup(x => x.LoadAsync<Champagne>(It.IsAny<AggregateId>(), null))
                .ReturnsAsync(pagne);
            
            //Act
            var response = await sut.Handle(cmd);
            
            //Assert
            response.ShouldNotBeNull();
            response.IsSuccessful.ShouldBeTrue();

            repo.Verify(x =>
                    x.StoreAsync(pagne, It.IsAny<IDocumentSession>()),
                Times.Once);
        } 
        
        [Theory, AutoMoqData]
        public async Task Fail_When_Changing_Name_On_Non_Existing_Champagne([Frozen]Mock<IPublishingAggregateRepository> repo, RenameChampagneCommand cmd, Champagne pagne, ChampagneHandler sut)
        {
            //Arrange
            repo.Setup(x => x.LoadAsync<Champagne>(It.IsAny<AggregateId>(), null))
                .ReturnsAsync((Champagne) null);
            
            //Act
            Should.Throw<DomainError>(async () =>
            {
                await sut.Handle(cmd);
            });
            
            //Assert
            repo.Verify(x =>
                    x.StoreAsync(pagne, It.IsAny<IDocumentSession>()),
                Times.Never);
        } 
        
                
        [Theory, AutoMoqData]
        public async Task Never_Persist_If_Domain_Logic_Fails([Frozen]Mock<IPublishingAggregateRepository> repo, Champagne pagne, ChampagneHandler sut)
        {
            //Arrange
            repo.Setup(x => x.LoadAsync<Champagne>(It.IsAny<AggregateId>(), null))
                .ReturnsAsync(pagne);
            
            var cmd = new UpdateGrapeBlendCommand(Guid.NewGuid(), new []
            {
                (0.2, "PinotNoir"),
                (0.7, "Chardonnay"),
                (0.5, "Arbane")
            });
            
            //Act
            Should.Throw<DomainError>(async () =>
            {
                await sut.Handle(cmd);
            });
            
            //Assert
            repo.Verify(x =>
                    x.StoreAsync(pagne, It.IsAny<IDocumentSession>()),
                Times.Never);
        } 
    }
}