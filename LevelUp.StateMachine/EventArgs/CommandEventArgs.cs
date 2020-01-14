namespace LevelUp.StateMachine.EventArgs
{
    public class CommandEventArgs<TCommand> : System.EventArgs
    {
        #region Properties
        public System.Object[] Args { get; protected set; }

        public TCommand Command { get; protected set; }
        #endregion Properties

        #region Constructors
        public CommandEventArgs(TCommand command, params System.Object[] args)
        {
            this.Command = command;
            this.Args = args;
        }
        #endregion Constructors
    }
}