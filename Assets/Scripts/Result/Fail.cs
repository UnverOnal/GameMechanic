using System;

namespace Result
{
    public class Fail : GameResult
    {
        public Fail( ResultChecker resultChecker) : base( resultChecker){}

        public override void Subscribe(Action action)
        {
            ResultChecker.OnCollide += action;
            Actions.Add(action);
        }
    }
}