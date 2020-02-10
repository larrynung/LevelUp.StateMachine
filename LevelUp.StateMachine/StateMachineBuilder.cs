using System;
using System.Collections.Generic;

namespace LevelUp.StateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    public class StateMachineBuilder<TState, TCommand>
        where TState : Enum
        where TCommand : Enum
    {
        #region Private fields
        private IDictionary<(TState, TCommand), TState> _translations;
        #endregion


        #region Public properties
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public TState CurrentState { get; protected set; }
        #endregion

        #region Protected properties
        /// <summary>
        /// Gets the translations.
        /// </summary>
        protected IDictionary<(TState, TCommand), TState> Translations
        {
            get { return _translations ??= new Dictionary<(TState, TCommand), TState>(); }
        }
        #endregion

        private StateMachineBuilder()
        {
        }

        private StateMachineBuilder(TState currentState)
            : this()
        {
            LoadState(currentState);
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
        /// </summary>
        /// <param name="translations"></param>
        public void LoadTranslations(IEnumerable<KeyValuePair<(TState, TCommand), TState>> translations)
        {
            if (translations == null)
                return;

            var currentTranslations = this.Translations;
            currentTranslations.Clear();
            foreach (var translation in translations) currentTranslations.Add(translation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StateMachineBuilder<TState, TCommand> Create()
        {
            return new StateMachineBuilder<TState, TCommand>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StateMachineBuilder<TState, TCommand> Create(TState currentState)
        {
            return new StateMachineBuilder<TState, TCommand>(currentState);
        }

        /// <summary>
        /// Adds the translation.
        /// </summary>
        /// <param name="baseState">State of the base.</param>
        /// <param name="command">The command.</param>
        /// <param name="targetState">State of the target.</param>
        /// <returns></returns>
        public StateMachineBuilder<TState, TCommand> AddTranslation(TState baseState, TCommand command,
            TState targetState)
        {
            var translations = this.Translations;
            var key = (baseState, command);

            if (translations.ContainsKey(key))
                throw new Exception("Duplicated translation!!");

            translations[key] = targetState;

            return this;
        }

        /// <summary>
        /// Build StateMachine instance
        /// </summary>
        /// <returns></returns>
        public StateMachine<TState, TCommand> Build()
        {
            return new StateMachine<TState, TCommand>(this.CurrentState, this.Translations);
        }
    }
}