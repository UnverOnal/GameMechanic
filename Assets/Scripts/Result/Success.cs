using System;
using Player;

namespace Result
{
    public class Success : GameResult
    {
        public Success( ResultChecker resultChecker) : base(resultChecker){}

        public override void Subscribe(Action action)
        {
            ResultChecker.OnCollide += action;
            Actions.Add(action);
        }
    }
}