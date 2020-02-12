using System;
using System.Collections.Generic;
using System.Linq;
using LevelUp.StateMachine.EventArgs;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateMachine<TState> : StateMachine<TState, TState>
        where TState : Enum
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="translations">translations, which define relation between states with command</param>
        public StateMachine(IDictionary<TState, TState> translations = null)
        {
            this.Translations = translations.ToDictionary(item => (item.Key, item.Value), item => item.Value);
        }
        #endregion
        
        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="command"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual StateMachine<TState> AddTranslation(TState sourceState, TState targetState)
        {
            var translations = this.Translations;
            var key = (sourceState, targetState);

            if (translations.ContainsKey(key))
                throw new Exception("Duplicated translation!!");

            translations[key] = targetState;

            return this;
        }
        #endregion
    }

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
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="translations">translations, which define relation between states with command</param>
        public StateMachine(IDictionary<(TState, TCommand), TState> translations = null)
        {
            this.Translations = translations;
        }
        #endregion

        #region Protected properties
        /// <summary>
        /// Gets the translations.
        /// </summary>
        protected IDictionary<(TState, TCommand), TState> Translations
        {
            get { return _translations ??= new Dictionary<(TState, TCommand), TState>(); }
            set { _translations = value; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool HasTranslation(TState sourceState, TCommand command)
        {
            var key = (sourceState, command);
            
            return this.Translations.ContainsKey(key);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        public bool HasTranslation(TState sourceState, TState targetState)
        {
            return this.Translations.Any(item =>
                Equals(item.Key.Item1, sourceState) && Equals(item.Value, targetState));
        }

        /// <summary>
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="targetState"></param>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public StateData<TState> TranslateTo(StateData<TState> stateData, TState targetState, params object[] args)
        {
            var sourceState = stateData.State;
            var hasTranslation = HasTranslation(sourceState, targetState);

            if (!hasTranslation)
            {
                throw new Exception(
                    $"Translation form {sourceState.ToString()} to {targetState.ToString()} is not found!!");
            }

            ChangeToState(stateData, targetState, args);

            return stateData;
        }

        /// <summary>
        /// Triggers the command.
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="command">The command.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="Exception"></exception>
        public StateData<TState> Trigger(StateData<TState> stateData, TCommand command, params object[] args)
        {
            OnCommandTrigger(new CommandEventArgs<TState, TCommand>(stateData, command, args));

            var sourceState = stateData.State;
            var hasTranslation = HasTranslation(sourceState, command);

            if (!hasTranslation)
                throw new Exception($"Translation({sourceState.ToString()}.{command.ToString()}) not found!!");

            var key = (currentState: sourceState, command);
            var targetState = this.Translations[key];
            
            ChangeToState(stateData, targetState, args);

            return stateData;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="command"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public StateMachine<TState, TCommand> AddTranslation(TState sourceState, TCommand command, TState targetState)
        {
            var translations = this.Translations;
            var key = (sourceState, command);

            if (translations.ContainsKey(key))
                throw new Exception("Duplicated translation!!");

            translations[key] = targetState;

            return this;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected void OnCommandTrigger(CommandEventArgs<TState, TCommand> e)
        {
            CommandTrigger?.Invoke(this, e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected void OnStateChanged(StateChangedEventArgs<TState> e)
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
        
        #region Private methods
        /// <summary>
        /// Changes to state.
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="targetState">State of the target.</param>
        /// <param name="args">The arguments.</param>
        private void ChangeToState(StateData<TState> stateData, TState targetState, params object[] args)
        {
            if (stateData.State.Equals(targetState))
                return;

            OnStateChanging(new StateChangingEventArgs<TState>(stateData, targetState, args));

            stateData.State = targetState;

            OnStateChanged(new StateChangedEventArgs<TState>(stateData, args));
        }
        #endregion

        #region Others
        /// <summary>
        /// </summary>
        public event EventHandler<CommandEventArgs<TState, TCommand>> CommandTrigger;

        /// <summary>
        /// </summary>
        public event EventHandler<StateChangedEventArgs<TState>> StateChanged;

        /// <summary>
        /// </summary>
        public event EventHandler<StateChangingEventArgs<TState>> StateChanging;
        #endregion
    }
}