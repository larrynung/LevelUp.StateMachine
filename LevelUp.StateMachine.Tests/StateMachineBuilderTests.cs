using System;
using LevelUp.StateMachine.Tests.Enums;
using NUnit.Framework;

namespace LevelUp.StateMachine.Tests
{
    public class StateMachineBuilderTests
    {
        [Test]
        public void Build_Invoke_ReturnStateMachineWithCurrentState()
        {
            // Arrange
            const StateType state = StateType.State1;
            const StateType expected = state;
            var target = new StateMachineBuilder<StateType, CommandType>(state);
            var actual = default(StateType);
            
            // Act
            actual = target.Build().CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Build_Invokes_ReturnStateMachine()
        {
            // Arrange
            var target = new StateMachineBuilder<StateType, CommandType>();
            var actual = default(Type);
            var expected = typeof(StateMachine<StateType, CommandType>);

            // Act
            actual = target.Build().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}