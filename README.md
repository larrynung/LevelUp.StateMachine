# LevelUp.StateMachine


## Instance StateMachine

```C#
var translations = new Dictionary<(StateType, CommandType), StateType>
{
    {(StateType.State1, CommandType.Command1), StateType.State2},
    {(StateType.State2, CommandType.Command2), StateType.State3}
};

// Instance StateMachine
var stateMachine = new StateMachine<StateType, CommandType>(translations);
```


<br>

```C#
public class MyStateMachine : StateMachine<StateType, CommandType>
{
    public static MyStateMachine Default { get; } = new MyStateMachine();

    public MyStateMachine()
    {
        this.Translations = new Dictionary<(StateType, CommandType), StateType>
        {
            {(StateType.State1, CommandType.Command1), StateType.State2},
            {(StateType.State2, CommandType.Command2), StateType.State3}
        };
    }
}

// Instance StateMachine
var stateMachine = new MyStateMachine();
```

## Init StateData

```C#
var stateData = new StateData<StateType>(StateType.State1);
```


## Trigger transaction

```C#
// Trigger transaction
stateMachine.Trigger(stateData, CommandType.Command1);
```

## Translate to target state

```C#
// Translate to target state
stateMachine.TranslateTo(stateData, StateType.State3);
```

## Read current state

```C#
// Read current state
Console.WriteLine(stateData.State);
```


<br>

```C#
// Read current state
Console.WriteLine(stateMachine.Trigger(stateData, CommandType.Command1).State);
```


<br>

```C#
// Read current state
Console.WriteLine(stateMachine.TranslateTo(stateData, StateType.State3).State); 
```