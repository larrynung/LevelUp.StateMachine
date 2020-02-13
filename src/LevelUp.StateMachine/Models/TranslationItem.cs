using System;

namespace LevelUp.StateMachine.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class TranslationItem<TState>
        where TState : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        public TState SourceState { get; }

        /// <summary>
        /// 
        /// </summary>
        public TState TargetState { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="targetState"></param>
        public TranslationItem(TState sourceState, TState targetState)
        {
            this.SourceState = sourceState;
            this.TargetState = targetState;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public class TranslationItem<TState, TCommand> : TranslationItem<TState>
        where TState : Enum
        where TCommand : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        public TCommand Command { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="command"></param>
        /// <param name="targetState"></param>
        public TranslationItem(TState sourceState, TCommand command, TState targetState)
            : base(sourceState, targetState)
        {
            this.Command = command;
        }
    }
}