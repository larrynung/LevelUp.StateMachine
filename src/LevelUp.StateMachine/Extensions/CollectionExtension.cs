using System;
using System.Collections.Generic;
using LevelUp.StateMachine.Models;

namespace LevelUp.StateMachine.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="sourceState"></param>
        /// <param name="targetState"></param>
        /// <typeparam name="TState"></typeparam>
        public static void Add<TState>(this ICollection<TranslationItem<TState>> collection, TState sourceState,
            TState targetState)
            where TState : Enum
        {
            collection.Add(new TranslationItem<TState>(sourceState, targetState));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="sourceState"></param>
        /// <param name="command"></param>
        /// <param name="targetState"></param>
        /// <typeparam name="TState"></typeparam>
        /// <typeparam name="TCommand"></typeparam>
        public static void Add<TState, TCommand>(this ICollection<TranslationItem<TState, TCommand>> collection,
            TState sourceState, TCommand command, TState targetState)
            where TState : Enum
            where TCommand : Enum
        {
            collection.Add(new TranslationItem<TState, TCommand>(sourceState, command, targetState));
        }
    }
}