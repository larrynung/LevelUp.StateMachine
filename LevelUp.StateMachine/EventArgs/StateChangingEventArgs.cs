namespace LevelUp.StateMachine.EventArgs
{
    public class StateChangingEventArgs<TState> : System.EventArgs
    {
        #region Constructors
        public StateChangingEventArgs(TState newState, params object[] args)
        {
            this.NewState = newState;
            this.Args = args;
        }
        #endregion

        #region Public properties
        public object[] Args { get; protected set; }

        public TState NewState { get; protected set; }
        #endregion
    }
}