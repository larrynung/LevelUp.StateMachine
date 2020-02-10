namespace LevelUp.StateMachine.EventArgs
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class CommandEventArgs<TCommand> : System.EventArgs
    {
        #region Constructors
        /// <summary>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        public CommandEventArgs(TCommand command, params object[] args)
        {
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
        #endregion
    }
}