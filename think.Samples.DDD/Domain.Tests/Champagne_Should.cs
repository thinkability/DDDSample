using System;
using System.Linq;
using Domain.Aggregates.Champagne;
using Domain.Aggregates.Champagne.Commands;
using Domain.Aggregates.Champagne.Events;
using Domain.Aggregates.Champagne.ValueObjects;
using Shouldly;
using Xunit;

namespace Domain.Tests
{
    public class Champagne_Should
    {
        [Theory, AutoMoqData]
        public void Record_ChampagneRenamed_Event_When_Changing_Champagne_Name(RenameChampagne cmd)
        {
            //Arrange
            var originalName = new ChampagneName("Bollinger 2007");
            var sut = new Champagne();
            sut.Execute(new CreateChampagne(new AggregateId(Guid.NewGuid()), originalName));
            sut.ClearUncommittedEvents();
            
            //Act
            sut.Execute(cmd);
            
            //Assert
            sut.GetUncommittedEvents().Count().ShouldBe(1);
            
            var evt = sut.GetUncommittedEvents().Single() as ChampagneRenamed;
            evt.ShouldNotBeNull();
            evt.OldName.ShouldBeSameAs(originalName);
            evt.NewName.ShouldBeSameAs(cmd.NewName);
        }
        
        [Fact]
        public void Throw_Error_When_Breaking_The_Law()
        {
            //Arrange
            var originalName = new ChampagneName("Bollinger 2007");
            var sut = new Champagne();
            sut.Execute(new CreateChampagne(new AggregateId(Guid.NewGuid()), originalName));
            sut.ClearUncommittedEvents();

            var grapes = new[]
            {
                new GrapeBlend(new GrapeBlendPercentage(0.7), new GrapeVariety("PinotNoir")),
                new GrapeBlend(new GrapeBlendPercentage(0.7), new GrapeVariety("PinotBlanc")),
            };
            var cmd = new UpdateGrapeBlend(sut.Id, grapes);
            
            //Act
            Should.Throw<DomainError>(() =>
            {
                sut.Execute(cmd);
            });
            
            //Assert
            sut.GetUncommittedEvents().Count().ShouldBe(0);
        }
        
        [Fact]
        public void Throw_Error_When_Reusing_Grapes_In_Blend()
        {
            //Arrange
            var originalName = new ChampagneName("Bollinger 2007");
            var sut = new Champagne();
            sut.Execute(new CreateChampagne(new AggregateId(Guid.NewGuid()), originalName));
            sut.ClearUncommittedEvents();

            var grapes = new[]
            {
                new GrapeBlend(new GrapeBlendPercentage(0.3), new GrapeVariety("PinotNoir")),
                new GrapeBlend(new GrapeBlendPercentage(0.7), new GrapeVariety("PinotNoir")),
            };
            var cmd = new UpdateGrapeBlend(sut.Id, grapes);
            
            //Act
            Should.Throw<DomainError>(() =>
            {
                sut.Execute(cmd);
            });
            
            //Assert
            sut.GetUncommittedEvents().Count().ShouldBe(0);
        }
        
        [Fact]
        public void Record_GrapeBlendUpdated_When_Updating_Blend()
        {
            //Arrange
            var originalName = new ChampagneName("Bollinger 2007");
            var sut = new Champagne();
            sut.Execute(new CreateChampagne(new AggregateId(Guid.NewGuid()), originalName));
            sut.ClearUncommittedEvents();

            var grapes = new[]
            {
                new GrapeBlend(new GrapeBlendPercentage(0.3), new GrapeVariety("PinotNoir")),
                new GrapeBlend(new GrapeBlendPercentage(0.7), new GrapeVariety("PinotBlanc")),
            };
            var cmd = new UpdateGrapeBlend(sut.Id, grapes);
            
            //Act
            sut.Execute(cmd);
            
            //Assert
            sut.GetUncommittedEvents().Count().ShouldBe(1);
            
            var evt = sut.GetUncommittedEvents().Single() as GrapeBlendUpdated;
            evt.ShouldNotBeNull();
            evt.Id.ShouldBeSameAs(sut.Id);
            evt.UpdatedBlend.ShouldBeSameAs(grapes);
        }
    }
}