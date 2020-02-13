using System;
using System.Collections.Generic;
using LevelUp.StateMachine.Models;

namespace LevelUp.StateMachine.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class TranslationItemCollection<TState> : List<TranslationItem<TState>>
        where TState : Enum
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public class TranslationItemCollection<TState, TCommand> : List<TranslationItem<TState, TCommand>>
        where TState : Enum
        where TCommand : Enum
    {
    }
}