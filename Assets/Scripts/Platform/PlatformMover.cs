using System;
using DG.Tweening;
using UnityEngine;
using Util;

namespace Platform
{
    public class PlatformMover
    {
        public GameObject NextStack { get; private set; }
        public GameObject CurrentStack { get; private set; }
        
        private GameObject[] _stacks;

        private int _activePlatformIndex = 1;

        private readonly float _duration;
        private readonly float _distance;
        private readonly float _perfectTapTolerance;
        private float _direction = 1f;

        private Tween _moveTween;
        
        public PlatformMover(GameObject[] stacks, float distance, float duration, float perfectTapTolerance)
        {
            _stacks = stacks;
            _distance = distance;
            _duration = duration;
            _perfectTapTolerance = perfectTapTolerance;
            
            SetPrevious(stacks[_activePlatformIndex]);
            ActivateNext();
        }

        public void ActivateNext()
        {
            _activePlatformIndex++;

            if (_activePlatformIndex >= _stacks.Length ) return;
            NextStack = _stacks[_activePlatformIndex];
            Place(NextStack);
            SetScale();
            StartMoving(NextStack);
            
            _direction *= -1f;
        }

        public void SetPrevious(GameObject previous)
        {
            CurrentStack = previous;
        }

        public void StopMoving(out bool isCloseEnough)
        {
            _moveTween.Kill();
            isCloseEnough = IsCloseEnough(CurrentStack.transform, NextStack.transform);
            if (isCloseEnough)
            {
                var position = NextStack.transform.position;
                position.x = CurrentStack.GetCenter().x;
                NextStack.transform.position = position;
            }
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

        private void SetScale()
        {
            var x = CurrentStack.GetComponent<BoxCollider>().size.x;
            var scale = CurrentStack.transform.localScale;
            scale.x *= x;
            NextStack.transform.localScale = scale;
        }
        
        private bool IsCloseEnough(Transform current, Transform next)
        {
            var canCut = Math.Abs(current.position.x - next.position.x) < _perfectTapTolerance;
            return canCut;
        }
    }
}
