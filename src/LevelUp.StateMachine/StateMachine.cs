using System;
using System.Collections.Generic;
using System.Linq;
using LevelUp.StateMachine.EventArgs;
using LevelUp.StateMachine.Models;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class StateMachine<TState>
        where TState : Enum
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        protected StateMachine()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translations"></param>
        public StateMachine(IEnumerable<TranslationItem<TState>> translations)
            : this()
        {
            this.Translations =
                translations.ToLookup(item => item.SourceState, item => item.TargetState);
        }
        #endregion

        #region Private properties
        /// <summary>
        /// Gets the translations.
        /// </summary>
        private ILookup<TState, TState> Translations { get; }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        public virtual bool HasTranslation(TState sourceState, TState targetState)
        {
            var translations = this.Translations;

            return translations.Any(item =>
                Equals(item.Key, sourceState) && translations[item.Key].Contains(targetState));
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
        #endregion

        #region Protected methods
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
        
        /// <summary>
        /// Changes to state.
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="targetState">State of the target.</param>
        /// <param name="args">The arguments.</param>
        protected void ChangeToState(StateData<TState> stateData, TState targetState, params object[] args)
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
        public event EventHandler<StateChangedEventArgs<TState>> StateChanged;

        /// <summary>
        /// </summary>
        public event EventHandler<StateChangingEventArgs<TState>> StateChanging;
        #endregion
    }

    /// <summary>
    /// StateMachine class, ease to use finite state machine
    /// </summary>
    /// <typeparam name="TState">state</typeparam>
    /// <typeparam name="TCommand">command</typeparam>
    public class StateMachine<TState, TCommand> : StateMachine<TState>
        where TState : Enum
        where TCommand : Enum
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="translations"></param>
        public StateMachine(IEnumerable<TranslationItem<TState, TCommand>> translations)
        {
            this.Translations =
                translations.ToLookup(item => (item.SourceState, item.Command), item => item.TargetState);
        }
        #endregion

        #region Private properties
        /// <summary>
        /// Gets the translations.
        /// </summary>
        private ILookup<(TState, TCommand), TState> Translations { get; }
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

            return this.Translations.Contains(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceState"></param>
        /// <param name="targetState"></param>
        /// <returns></returns>
        public override bool HasTranslation(TState sourceState, TState targetState)
        {
            var translations = this.Translations;

            return translations.Any(item =>
                Equals(item.Key.Item1, sourceState) && translations[item.Key].Contains(targetState));
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
            var targetState = this.Translations[key].Single();

            ChangeToState(stateData, targetState, args);

            return stateData;
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
        #endregion

        #region Others
        /// <summary>
        /// </summary>
        public event EventHandler<CommandEventArgs<TState, TCommand>> CommandTrigger;
        #endregion
    }
}