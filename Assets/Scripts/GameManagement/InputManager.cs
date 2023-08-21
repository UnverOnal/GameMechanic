using System;
using UnityEngine;

namespace GameManagement
{
    public class InputManager
    {
        public event Action OnTap;
        private bool IsTapped => Input.GetMouseButtonDown(0);

        private bool _ignore;

        public void Update()
        {
            if(_ignore) return;
        
            if(IsTapped)
                OnTap?.Invoke();
        }

        public void Ignore(bool ignore)
        {
            _ignore = ignore;
        }
    }
}