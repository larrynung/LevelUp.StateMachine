namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    public class StateChangedEventArgs : System.EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public StateChangedEventArgs(params object[] args)
        {
            this.Args = args;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// 
        /// </summary>
        public object[] Args { get; protected set; }
        #endregion
    }
}