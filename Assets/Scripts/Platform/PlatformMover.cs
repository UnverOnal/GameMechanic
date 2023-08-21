using DG.Tweening;
using UnityEngine;

namespace Platform
{
    public class PlatformMover
    {
        public GameObject NextStack { get; private set; }
        public GameObject CurrentStack { get; private set; }
        
        private GameObject[] _stacks;

        private int _activePlatformIndex;

        private readonly float _duration;
        private readonly float _distance;
        private float _direction = 1f;

        private Tween _moveTween;
        
        public PlatformMover(GameObject[] stacks, int activePlatformIndex, float distance, float duration)
        {
            _stacks = stacks;
            _activePlatformIndex = activePlatformIndex;
            _distance = distance;
            _duration = duration;
            
            SetPrevious(stacks[_activePlatformIndex]);
            ActivateNext();
        }

        public void ActivateNext()
        {
            _activePlatformIndex++;
            
            NextStack = _stacks[_activePlatformIndex];
            Place(NextStack);
            StartMoving(NextStack);
            
            _direction *= -1f;
        }

        public void SetPrevious(GameObject previous)
        {
            CurrentStack = previous;
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
            _moveTween = stack.transform.DOMoveX(_distance * _direction * -1f, _duration);
        }
    }
}
