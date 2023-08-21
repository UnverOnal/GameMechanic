using System;
using UnityEngine;

namespace Result
{
    public class ResultChecker : MonoBehaviour
    {
        public event Action OnCollide;
    
        private string _tagToCollide;
    
        public void Initialize(string tagToCollide)
        {
            _tagToCollide = tagToCollide;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(_tagToCollide)) return;
        
            OnCollide?.Invoke();
        }
    }
}
