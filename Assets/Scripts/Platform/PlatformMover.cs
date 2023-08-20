using DG.Tweening;
using UnityEngine;

namespace Platform
{
    public class PlatformMover
    {
        public GameObject CurrentStack { get; private set; }
        public GameObject PreviousStack { get; private set; }
        
        private GameObject[] _stacks;

        private int _activePlatformIndex;
        
        private float _direction = 1f;
        private readonly float _distance;

        private Tween _moveTween;
        
        public PlatformMover(GameObject[] stacks, int activePlatformIndex, float distance)
        {
            _stacks = stacks;
            _activePlatformIndex = activePlatformIndex;
            _distance = distance;
            
            SetPrevious(stacks[_activePlatformIndex]);
            ActivateNext();
        }

        public void ActivateNext()
        {
            _activePlatformIndex++;
            
            CurrentStack = _stacks[_activePlatformIndex];
            Place(CurrentStack);
            StartMoving(CurrentStack);
            
            _direction *= -1f;
        }

        public void SetPrevious(GameObject previous)
        {
            PreviousStack = previous;
        }

        public void StopMoving()
        {
            _moveTween.Kill();
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
            _moveTween = stack.transform.DOMoveX(_distance * _direction * -1f, 10f);
        }
    }
}
