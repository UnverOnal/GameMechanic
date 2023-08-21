using System;
using System.Collections.Generic;
using GameResource;
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
        
    }
}
