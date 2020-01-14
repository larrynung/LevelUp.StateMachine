using System;
using System.Collections.Generic;
using LevelUp.StateMachine.EventArgs;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public class StateMachine<TState, TCommand>
    {
        #region Private fields
        private TState _currentState;
        private Dictionary<TransitionKey<TState, TCommand>, TState> _transitions;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine{TState, TCommand}" /> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public StateMachine(TState state)
        {
            this.CurrentState = state;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public TState CurrentState
        {
            get { return _currentState; }
            protected set
            {
                if (_currentState.Equals(value))
                    return;

                _currentState = value;
            }
        }
        #endregion

        #region Protected properties
        /// <summary>
        /// Gets the m_ transitions.
        /// </summary>
        /// <value>
        /// The m_ transitions.
        /// </value>
        protected Dictionary<TransitionKey<TState, TCommand>, TState> m_Transitions
        {
            get { return _transitions ?? (_transitions = new Dictionary<TransitionKey<TState, TCommand>, TState>()); }
        }
        #endregion

        /// <summary>
        /// Adds the translation.
        /// </summary>
        /// <param name="baseState">State of the base.</param>
        /// <param name="command">The command.</param>
        /// <param name="targetState">State of the target.</param>
        /// <returns></returns>
        public StateMachine<TState, TCommand> AddTranslation(TState baseState, TCommand command, TState targetState)
        {
            var key = new TransitionKey<TState, TCommand>(baseState, command);

            if (this.m_Transitions.ContainsKey(key))
                throw new Exception("Duplicated translation!!");

            this.m_Transitions.Add(key, targetState);

            return this;
        }

        /// <summary>
        /// Loads the state.
        /// </summary>
        /// <param name="targetState">State of the target.</param>
        public void LoadState(TState targetState)
        {
            this.CurrentState = targetState;
        }

        /// <summary>
        /// Triggers the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="Exception"></exception>
        public void Trigger(TCommand command, params object[] args)
        {
            OnCommandTrigger(new CommandEventArgs<TCommand>(command, args));

            var currentState = this.CurrentState;
            var key = new TransitionKey<TState, TCommand>(currentState, command);

            if (!this.m_Transitions.ContainsKey(key))
                throw new Exception(string.Format("Translation({0}.{1}) not found!!", currentState.ToString(),
                    command.ToString()));

            var state = this.m_Transitions[key];

            ChangeToState(state, args);
        }

        #region Protected methods
        /// <summary>
        /// Changes to state.
        /// </summary>
        /// <param name="targetState">State of the target.</param>
        /// <param name="args">The arguments.</param>
        protected void ChangeToState(TState targetState, params object[] args)
        {
            if (this.CurrentState.Equals(targetState))
                return;

            OnStateChanging(new StateChangingEventArgs<TState>(targetState, args));

            this.CurrentState = targetState;

            OnStateChanged(new StateChangedEventArgs(args));
        }

        /// <summary>
        /// Raises the <see cref="E:CommandTrigger" /> event.
        /// </summary>
        /// <param name="e">The <see cref="CommandEventArgs{TCommand}" /> instance containing the event data.</param>
        protected void OnCommandTrigger(CommandEventArgs<TCommand> e)
        {
            var handler = CommandTrigger;

            if (handler == null)
                return;

            handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:StateChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="StateChangedEventArgs{TState}" /> instance containing the event data.</param>
        protected void OnStateChanged(StateChangedEventArgs e)
        {
            var handler = StateChanged;

            if (handler == null)
                return;

            handler(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:StateChanging" /> event.
        /// </summary>
        /// <param name="e">The <see cref="StateChangingEventArgs{TState}" /> instance containing the event data.</param>
        protected void OnStateChanging(StateChangingEventArgs<TState> e)
        {
            var handler = StateChanging;

            if (handler == null)
                return;

            handler(this, e);
        }
        #endregion

        #region Others
        public event EventHandler<CommandEventArgs<TCommand>> CommandTrigger;

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public event EventHandler<StateChangingEventArgs<TState>> StateChanging;
        #endregion

        #region Nested types
        /// <summary>
        /// </summary>
        /// <typeparam name="TS">The type of the s.</typeparam>
        /// <typeparam name="TC">The type of the c.</typeparam>
        protected struct TransitionKey<TS, TC>
        {
            #region Constructors
            public TransitionKey(TS state, TC command)
                : this()
            {
                this.State = state;
                this.Command = command;
            }
            #endregion

            #region Public properties
            public TC Command { get; }

            public TS State { get; }
            #endregion
        }
        #endregion
    }
}