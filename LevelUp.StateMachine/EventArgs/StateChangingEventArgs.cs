namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateChangingEventArgs<TState> : System.EventArgs
    {
        #region Constructors
        /// <summary>
        /// </summary>
        /// <param name="newState"></param>
        /// <param name="args"></param>
        public StateChangingEventArgs(TState newState, params object[] args)
        {
            this.NewState = newState;
            this.Args = args;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// </summary>
        public object[] Args { get; protected set; }

        /// <summary>
        /// </summary>
        public TState NewState { get; protected set; }
        #endregion
    }
}