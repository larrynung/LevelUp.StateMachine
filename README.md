# LevelUp.StateMachine


## Instance StateMachine

### With StateMachineBuilder

```C#
var initialState = StateType.State1;
var stateMachine = StateMachineBuilder<StateType, CommandType>
                   .Create(initialState)
                   .AddTranslation(StateType.State1, CommandType.Command1, StateType.State2)
                   .AddTranslation(StateType.State2, CommandType.Command2, StateType.State3)
                   .Build();
```


<br>

```C#
var initialState = StateType.State1;
var translations = new Dictionary<(StateType, CommandType), StateType>
{
    {(StateType.State1, CommandType.Command1), StateType.State2},
    {(StateType.State2, CommandType.Command2), StateType.State3}
};
var stateMachine = StateMachineBuilder<StateType, CommandType>
                   .Create()
                   .LoadState(initialState)
                   .LoadTranslations(translations)
                   .Build();
```

### With StateMachine

```C#
var initialState = StateType.State1;
var translations = new Dictionary<(StateType, CommandType), StateType>
{
    {(StateType.State1, CommandType.Command1), StateType.State2},
    {(StateType.State2, CommandType.Command2), StateType.State3}
};

// Instance StateMachine
var stateMachine = new StateMachine<StateType, CommandType>(initialState, translations);
```


## Trigger transaction

```C#
// Trigger transaction
stateMachine.Trigger(CommandType.Command1);
```

## Translate to target state

```C#
// Translate to target state
stateMachine.TranslateTo(StateType.State3);
```

## Read current state

```C#
// Read current state
Console.WriteLine(stateMachine.CurrentState);
```