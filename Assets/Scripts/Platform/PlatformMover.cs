using DG.Tweening;
using UnityEngine;

namespace Platform
{
    public class PlatformMover
    {
        private GameObject[] _stacks;

        private int _activePlatformIndex;
        
        private float _direction = 1f;
        private float _distance;
        
        public PlatformMover(GameObject[] stacks, int activePlatformIndex, float distance)
        {
            _stacks = stacks;
            _activePlatformIndex = activePlatformIndex;
            _distance = distance;
            
            ActivateNext();
        }

        public void ActivateNext()
        {
            _activePlatformIndex++;
            
            var stack = _stacks[_activePlatformIndex];
            Place(stack);
            StartMoving(stack);
            
            _direction *= -1f;
        }

        private void Place(GameObject stack)
        {
            stack.SetActive(true);
            
            var position = stack.transform.position;
            position += Vector3.right * _direction * _distance;
            stack.transform.position = position;
        }

        private void StartMoving(GameObject stack)
        {
            stack.transform.DOMoveX(_distance * _direction * -1f, 1f)
                .SetEase(Ease.Linear)
                .SetLoops( -1, LoopType.Yoyo);
        }
    }
}
