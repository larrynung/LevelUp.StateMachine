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
            var stateData = new StateData<StateType>(StateType.State1);
            var target = new MyStateMachine();
            var actual = default(StateType);

            // Act
            actual = target.Trigger(stateData, CommandType.Command1).State;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Trigger_WithAcceptableCommand_CommandTriggerEventInvoked()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.CommandTrigger += (sender, args) => { actual = true; };
                
            // Act
            target.Trigger(stateData, CommandType.Command1);

            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void Trigger_WithAcceptableCommand_StateChangingEventInvoked()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.StateChanging += (sender, args) => { actual = true; };
                
            // Act
            target.Trigger(stateData, CommandType.Command1);

            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void Trigger_WithAcceptableCommand_StateChangedEventInvoked()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.StateChanged += (sender, args) => { actual = true; };
                
            // Act
            target.Trigger(stateData, CommandType.Command1);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Trigger_WithUnAcceptableCommand_Exception()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(Exception);

            // Act
            actual = Assert.Throws<Exception>(() => target.Trigger(stateData, CommandType.Command2));

            // Assert
            Assert.IsNotNull(actual);
        }
        
        [Test]
        public void TranslateTo_WithAcceptableTargetState_ChangeCurrentStateToSpecifiedState()
        {
            // Arrange
            const StateType expected = StateType.State2;
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(StateType);

            // Act
            actual = target.TranslateTo(stateData, StateType.State2).State;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TranslateTo_WithAcceptableTargetState_CommandTriggerEventNotInvoke()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.CommandTrigger += (sender, args) => { actual = true; };

            // Act
            target.TranslateTo(stateData, StateType.State2);

            // Assert
            Assert.IsFalse(actual);
        }
        
        [Test]
        public void TranslateTo_WithAcceptableTargetState_StateChangingEventInvoked()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.StateChanging += (sender, args) => { actual = true; };

            // Act
            target.TranslateTo(stateData, StateType.State2);

            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void TranslateTo_WithAcceptableTargetState_StateChangedEventInvoked()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
            var target = new StateMachine<StateType, CommandType>(translations);
            var actual = default(bool);

            target.StateChanged += (sender, args) => { actual = true; };

            // Act
            target.TranslateTo(stateData, StateType.State2);

            // Assert
            Assert.IsTrue(actual);
        }
        
        [Test]
        public void TranslateTo_WithUnAcceptableTargetState_Exception()
        {
            // Arrange
            var stateData = new StateData<StateType>(StateType.State1);
            var target = new StateMachine<StateType, CommandType>();
            var actual = default(Exception);

            // Act
            actual = Assert.Throws<Exception>(() => target.TranslateTo(stateData, StateType.State2));

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}