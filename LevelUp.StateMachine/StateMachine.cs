using System;
using System.Collections.Generic;
using System.Linq;
using LevelUp.StateMachine.EventArgs;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// StateMachine class, ease to use finite state machine
    /// </summary>
    /// <typeparam name="TState">state</typeparam>
    /// <typeparam name="TCommand">command</typeparam>
    public class StateMachine<TState, TCommand>
        where TState : Enum
        where TCommand : Enum
    {
        #region Private fields
        private IDictionary<(TState, TCommand), TState> _translations;
        private HashSet<(TState, TState)> _translationPool;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentState">currentState</param>
        /// <param name="translations">translations, which define relation between states with command</param>
        public StateMachine(TState currentState,
            IEnumerable<KeyValuePair<(TState, TCommand), TState>> translations = null)
        {
            this.CurrentState = currentState;

            LoadTranslations(translations);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public TState CurrentState { get; protected set; }

        /// <summary>
        /// Gets the translations.
        /// </summary>
        public IDictionary<(TState, TCommand), TState> Translations
        {
            get { return _translations ??= new Dictionary<(TState, TCommand), TState>(); }
        }
        #endregion

        #region Private properties
        private HashSet<(TState, TState)> TranslationPool
        {
            get
            {
                return _translationPool ??= Translations.Select(item => (item.Key.Item1, item.Value)).ToHashSet();
            }
        }
        #endregion

        /// <summary>
        /// </summary>
        /// <param name="translations"></param>
        private void LoadTranslations(IEnumerable<KeyValuePair<(TState, TCommand), TState>> translations)
        {
            if (translations == null)
                return;

            var currentTranslations = this.Translations;
            currentTranslations.Clear();
            foreach (var translation in translations) currentTranslations.Add(translation);
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
            var translations = this.Translations;
            var key = (currentState, command);
            var haveTranslation = translations.TryGetValue(key, out var targetState);

            if (!haveTranslation)
                throw new Exception($"Translation({currentState.ToString()}.{command.ToString()}) not found!!");

            ChangeToState(targetState, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetState"></param>
        /// <param name="args"></param>
        public void TranslateTo(TState targetState, params object[] args)
        {
            var currentState = this.CurrentState;
            var key = (currentState, targetState);
            var haveTranslation =  TranslationPool.Contains(key);
            
            if (!haveTranslation)
                throw new Exception($"Translation form {currentState.ToString()} to {targetState.ToString()} is not found!!");
                
            ChangeToState(targetState, args);
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
            CommandTrigger?.Invoke(this, e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected void OnStateChanged(StateChangedEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:StateChanging" /> event.
        /// </summary>
        /// <param name="e">The <see cref="StateChangingEventArgs{TState}" /> instance containing the event data.</param>
        protected void OnStateChanging(StateChangingEventArgs<TState> e)
        {
            StateChanging?.Invoke(this, e);
        }
        #endregion

        #region Others
        /// <summary>
        /// </summary>
        public event EventHandler<CommandEventArgs<TCommand>> CommandTrigger;

        /// <summary>
        /// </summary>
        public event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// </summary>
        public event EventHandler<StateChangingEventArgs<TState>> StateChanging;
        #endregion
    }
}