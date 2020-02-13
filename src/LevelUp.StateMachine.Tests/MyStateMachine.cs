using LevelUp.StateMachine.Collections;
using LevelUp.StateMachine.Extensions;
using LevelUp.StateMachine.Tests.Enums;

namespace LevelUp.StateMachine.Tests
{
    public class MyStateMachineWithCommand : StateMachine<StateType, CommandType>
    {
        public MyStateMachineWithCommand()
            : base(new TranslationItemCollection<StateType, CommandType>
            {
                {StateType.State1, CommandType.Command1, StateType.State2}
            })
        {
        }
    }

    public class MyStateMachineWithoutCommand : StateMachine<StateType>
    {
        public MyStateMachineWithoutCommand()
            : base(new TranslationItemCollection<StateType>
            {
                {StateType.State1, StateType.State2}
            })
        {
        }
    }
}