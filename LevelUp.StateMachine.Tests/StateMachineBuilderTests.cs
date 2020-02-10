using System;
using LevelUp.StateMachine.Tests.Enums;
using NUnit.Framework;

namespace LevelUp.StateMachine.Tests
{
    public class StateMachineBuilderTests
    {
        [Test]
        public void Build_WithCurrentState_ReturnStateMachineWithCurrentState()
        {
            // Arrange
            const StateType state = StateType.State1;
            const StateType expected = state;
            var target = StateMachineBuilder<StateType, CommandType>
                .Create(state);
            var actual = default(StateType);
            
            // Act
            actual = target.Build().CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Build_WithTransactions_ReturnStateMachine()
        {
            // Arrange
            var target = StateMachineBuilder<StateType, CommandType>
                .Create(StateType.State1)
                .AddTranslation(StateType.State1, CommandType.Command1, StateType.State2);
            var actual = default(Type);
            var expected = typeof(StateMachine<StateType, CommandType>);

            // Act
            actual = target.Build().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}