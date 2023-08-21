using System;
using System.Collections.Generic;
using UnityEngine;

namespace Result
{
    public abstract class GameResult
    {
        protected readonly ResultUiResources UiResources;
        protected readonly ResultChecker ResultChecker;
        protected readonly List<Action> Actions = new();
        
        public GameResult(ResultChecker resultChecker)
        {
            ResultChecker = resultChecker;
        }
        
        public abstract void Subscribe(Action action);

        public virtual void Reset()
        {
            UnSubscribe();
        }
        
        private void UnSubscribe()
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                var action = Actions[i];
                ResultChecker.OnCollide -= action;
            }
        }
    }
}
