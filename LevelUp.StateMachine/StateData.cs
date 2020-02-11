using System;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateData<TState>
        where TState : Enum
    {
        #region Constructors
        /// <summary>
        /// </summary>
        public StateData()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="state"></param>
        public StateData(TState state) : this()
        {
            this.State = state;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// </summary>
        public TState State { get; internal set; }
        #endregion
    }
}