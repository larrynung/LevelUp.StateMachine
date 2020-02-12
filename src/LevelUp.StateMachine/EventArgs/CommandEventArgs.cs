using System;

namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public class CommandEventArgs<TState, TCommand> : System.EventArgs
        where TState : Enum
        where TCommand : Enum
    {
        #region Constructors
        /// <summary>
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public CommandEventArgs(StateData<TState> stateData, TCommand command, params object[] args)
        {
            this.StateData = stateData;
            this.Command = command;
            this.Args = args;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// </summary>
        public object[] Args { get; protected set; }

        /// <summary>
        /// </summary>
        public TCommand Command { get; protected set; }

        /// <summary>
        /// </summary>
        public StateData<TState> StateData { get; }
        #endregion
    }
}