using System.Collections.Generic;
using LevelUp.StateMachine.Tests.Enums;

namespace LevelUp.StateMachine.Tests
{
    public class MyStateMachine : StateMachine<StateType, CommandType>
    {
        public static MyStateMachine Default { get; } = new MyStateMachine();

        public MyStateMachine()
        {
            this.Translations = new Dictionary<(StateType, CommandType), StateType>
            {
                {(StateType.State1, CommandType.Command1), StateType.State2}
            };
        }
    }
}