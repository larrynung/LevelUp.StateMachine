# LevelUp.StateMachine

```C#
// Instance StateMachine
var stateMachine = new StateMachine<StateType, CommandType>(StateType.State1);

// Define transaction
stateMachine.AddTranslation(StateType.State1, CommandType.Command1, StateType.State2);
stateMachine.AddTranslation(StateType.State2, CommandType.Command2, StateType.State3);

// Trigger transaction
stateMachine.Trigger(CommandType.Command1);

// Read current state
Console.WriteLine(stateMachine.CurrentState);
```