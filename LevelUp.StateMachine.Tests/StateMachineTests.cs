using System;
using System.Collections.Generic;
using LevelUp.StateMachine.Tests.Enums;
using NUnit.Framework;

namespace LevelUp.StateMachine.Tests
{
    public class StateMachineTests
    {

        [Test]
        public void Trigger_WithAcceptableCommand_ChangeCurrentStateToSpecifiedState()
        {
            // Arrange
            const StateType expected = StateType.State2;
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(StateType.State1, translations);
            var actual = default(StateType);

            // Act
            target.Trigger(CommandType.Command1);
            actual = target.CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Trigger_WithUnAcceptableCommand_Exception()
        {
            // Arrange
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(StateType.State1, translations);
            var actual = default(Exception);

            // Act
            actual = Assert.Throws<Exception>(() => target.Trigger(CommandType.Command2));

            // Assert
            Assert.IsNotNull(actual);
        }
        
        [Test]
        public void TranslateTo_WithAcceptableTargetState_ChangeCurrentStateToSpecifiedState()
        {
            // Arrange
            const StateType expected = StateType.State2;
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(StateType.State1, translations);
            var actual = default(StateType);

            // Act
            target.TranslateTo(StateType.State2);
            actual = target.CurrentState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TranslateTo_WithUnAcceptableTargetState_Exception()
        {
            // Arrange
            var target = new StateMachine<StateType, CommandType>(StateType.State1);
            var actual = default(Exception);

            // Act
            actual = Assert.Throws<Exception>(() => target.TranslateTo(StateType.State2));

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}