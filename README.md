# LevelUp.StateMachine

## Install

### Package Manager

    Install-Package LevelUp.StateMachine -Version 3.0.0

### .NET CLI

    dotnet add package LevelUp.StateMachine --version 3.0.0

### PackageReference

    <PackageReference Include="LevelUp.StateMachine" Version="3.0.0" />

### Packet CLI

    paket add LevelUp.StateMachine --version 3.0.0


## Tutorial

### Instance StateMachine

```C#
var translations = new TranslationItemCollection<StateType, CommandType>
{
    {StateType.State1, CommandType.Command1, StateType.State2},
    {StateType.State2, CommandType.Command2, StateType.State3}
};

// Instance StateMachine
var stateMachine = new StateMachine<StateType, CommandType>(translations);
```

<br>


```C#
var translations = new TranslationItemCollection<StateType>
{
    {StateType.State1, StateType.State2},
    {StateType.State2, StateType.State3}
};

// Instance StateMachine
var stateMachine = new StateMachine<StateType>(translations);
```


<br>

```C#
public class MyStateMachine : StateMachine<StateType, CommandType>
{
    public MyStateMachine()
        : base(new TranslationItemCollection<StateType, CommandType>
        {
            {StateType.Opening, CommandType.SensorOpened, StateType.Opened},
            {StateType.Opening, CommandType.Close, StateType.Closing},
            {StateType.Opened, CommandType.Close, StateType.Closing},
            {StateType.Closing, CommandType.Open, StateType.Opening},
            {StateType.Closing, CommandType.SensorClosed, StateType.Closed},
            {StateType.Closing, CommandType.SensorClosed, StateType.Closed},
            {StateType.Closed, CommandType.Open, StateType.Opening}
        })
    {
    }
}

// Instance StateMachine
var stateMachine = new MyStateMachine();
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
var state = stateData.State;
```


<br>

```C#
// Read current state
var state = stateMachine.Trigger(stateData, CommandType.Command1).State;
```


<br>

```C#
// Read current state
var state = stateMachine.TranslateTo(stateData, StateType.State3).State; 
```

### Check transaction

```C#
// Check transaction
var hasTranslation = stateMachine.HasTranslation(sourceState, command);
```

<br>

```C#
// Check transaction
var hasTranslation = stateMachine.HasTranslation(sourceState, targetState);
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
        : base(new TranslationItemCollection<StateType, CommandType>
        {
            {StateType.Opening, CommandType.SensorOpened, StateType.Opened},
            {StateType.Opening, CommandType.Close, StateType.Closing},
            {StateType.Opened, CommandType.Close, StateType.Closing},
            {StateType.Closing, CommandType.Open, StateType.Opening},
            {StateType.Closing, CommandType.SensorClosed, StateType.Closed},
            {StateType.Closing, CommandType.SensorClosed, StateType.Closed},
            {StateType.Closed, CommandType.Open, StateType.Opening}
        })
    {
    }
}

// Instance StateMachine
var stateMachine = new MyStateMachine();

// Hook event
stateMachine.CommandTrigger += (sender, e) => { 
    Console.WriteLine(e.StateData.State); 
};

stateMachine.StateChanging += (sender, e) => { 
    Console.WriteLine(e.StateData.State); 
};

stateMachine.StateChanged += (sender, e) => { 
    Console.WriteLine(e.StateData.State); 
};

// Init StateData
var stateData = new StateData<StateType>(StateType.Opening);

// Trigger transaction
stateMachine.Trigger(stateData, CommandType.SensorOpened);
stateMachine.Trigger(stateData, CommandType.Open);
stateMachine.Trigger(stateData, CommandType.Close);
stateMachine.Trigger(stateData, CommandType.SensorClosed);
stateMachine.Trigger(stateData, CommandType.Open);

// Translate to target state
stateMachine.TranslateTo(stateData, StateType.Opened);
stateMachine.TranslateTo(stateData, StateType.Opening);
stateMachine.TranslateTo(stateData, StateType.Closing);
stateMachine.TranslateTo(stateData, StateType.Closed);
```
