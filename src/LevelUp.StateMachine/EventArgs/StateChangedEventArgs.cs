using System;
using LevelUp.StateMachine.Models;

namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// </summary>
    public class StateChangedEventArgs<TState> : System.EventArgs
        where TState : Enum
    {
        #region Constructors
        /// <summary>
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="args"></param>
        public StateChangedEventArgs(StateData<TState> stateData, params object[] args)
        {
            this.StateData = stateData;
            this.Args = args;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// </summary>
        public object[] Args { get; protected set; }

        /// <summary>
        /// </summary>
        public StateData<TState> StateData { get; }
        #endregion
    }
}