using System;
using LevelUp.StateMachine.Tests.Enums;
using NUnit.Framework;

namespace LevelUp.StateMachine.Tests
{
    public class StateMachineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadState_LoadSpecifiedState_CurrentStateIsSpecifiedState()
        {
            // Arrange
            const StateType state = StateType.State2;
            const StateType expected = state;
            var target = new StateMachine<StateType, CommandType>(StateType.State1);
            var actual = default(StateType);

            // Act
            target.LoadState(state);
            actual = target.CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Trigger_WithAcceptableCommand_ChangeCurrentStateToSpecifiedState()
        {
            // Arrange
            const StateType expected = StateType.State2;
            var target = new StateMachine<StateType, CommandType>(StateType.State1);
            var actual = default(StateType);

            target.AddTranslation(StateType.State1, CommandType.Command1, StateType.State2);

            // Act
            target.Trigger(CommandType.Command1);
            actual = target.CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Trigger_WithUnAcceptableCommand_Excaption()
        {
            // Arrange
            var target = new StateMachine<StateType, CommandType>(StateType.State1);
            var actual = default(Exception);

            target.AddTranslation(StateType.State1, CommandType.Command1, StateType.State2);

            // Act
            actual = Assert.Throws<Exception>(()=>target.Trigger(CommandType.Command2));

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}