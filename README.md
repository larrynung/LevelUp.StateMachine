# LevelUp.StateMachine

## Install

### Package Manager

    Install-Package LevelUp.StateMachine -Version 2.0.0

### .NET CLI

    dotnet add package LevelUp.StateMachine --version 2.0.0

### PackageReference

    <PackageReference Include="LevelUp.StateMachine" Version="2.0.0" />

### Packet CLI

    paket add LevelUp.StateMachine --version 2.0.0


## Tutorial

### Instance StateMachine

```C#
// Instance StateMachine
var stateMachine = new StateMachine<StateType, CommandType>();
```


<br>

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

### Add translations

```C#
stateMachine
    .AddTranslation(StateType.State1, CommandType.Command1, StateType.State2)
    .AddTranslation(StateType.State2, CommandType.Command2, StateType.State3);
```


### Init StateData

```C#
// Init StateData
var stateData = new StateData<StateType>(StateType.State1);
```


### Trigger transaction

```C#
// Trigger transaction
stateMachine.Trigger(stateData, CommandType.Command1);
```

### Translate to target state

```C#
// Translate to target state
stateMachine.TranslateTo(stateData, StateType.State3);
```

### Read current state

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

### Event handling

```C#
stateMachine.CommandTrigger += (sender, e) => { 
    Console.WriteLine("CommandTrigger..."); 
};

stateMachine.StateChanging += (sender, e) => { 
    Console.WriteLine("StateChanging..."); 
};

stateMachine.StateChanged += (sender, e) => { 
    Console.WriteLine("StateChanged..."); 
};
```

## Example

![](./image/statemachine.png)

```C#
public class MyStateMachine : StateMachine<StateType, CommandType>
{
    public MyStateMachine()
    {
        this.Translations = new Dictionary<(StateType, CommandType), StateType>
        {
            {(StateType.Opening, CommandType.SensorOpened), StateType.Opened},
            {(StateType.Opening, CommandType.Close), StateType.Closing},
            {(StateType.Opened, CommandType.Close), StateType.Closing},
            {(StateType.Closing, CommandType.Open), StateType.Opening},
            {(StateType.Closing, CommandType.SensorClosed), StateType.Closed},
            {(StateType.Closing, CommandType.SensorClosed), StateType.Closed},
            {(StateType.Closed, CommandType.Open), StateType.Opening}
        };
    }
}

// Instance StateMachine
var stateMachine = new MyStateMachine();

// Init StateData
var stateData = new StateData<StateType>(StateType.Opening);

Console.WriteLine(stateMachine.Trigger(stateData, CommandType.SensorOpened).State);
Console.WriteLine(stateMachine.Trigger(stateData, CommandType.Open).State);
Console.WriteLine(stateMachine.Trigger(stateData, CommandType.Close).State);
Console.WriteLine(stateMachine.Trigger(stateData, CommandType.SensorClosed).State);
Console.WriteLine(stateMachine.Trigger(stateData, CommandType.Open).State);
```
