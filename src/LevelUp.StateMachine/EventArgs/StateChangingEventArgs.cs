using System;

namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateChangingEventArgs<TState> : System.EventArgs
        where TState : Enum
    {
        #region Constructors
        /// <summary>
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="newState"></param>
        /// <param name="args"></param>
        public StateChangingEventArgs(StateData<TState> stateData, TState newState, params object[] args)
        {
            this.StateData = stateData;
            this.NewState = newState;
            this.Args = args;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// </summary>
        public object[] Args { get; }

        /// <summary>
        /// </summary>
        public TState NewState { get; }

        /// <summary>
        /// </summary>
        public StateData<TState> StateData { get; }
        #endregion
    }
}