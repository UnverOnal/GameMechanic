using System;
using System.Collections.Generic;
using GameResource;
using Platform;
using UnityEngine;
using VContainer;

namespace Result
{
    public class ResultManager : IReloadable
    {
        private readonly GameObject _finish;
        private readonly GameObject _fail;
        
        private readonly ResultChecker _failChecker;
        private readonly ResultChecker _finishChecker;
        
        private readonly Collider _failCollider;

        private readonly PlatformManager _platformManager;
        private readonly ResultUiDisplayer _uiDisplayer;

        private Dictionary<ResultType, GameResult> _results = new();

        [Inject]
        public ResultManager(SceneResources sceneResources, PlatformManager platformManager)
        {
            _fail = sceneResources.fail;
            _failChecker = _fail.GetComponent<ResultChecker>();

            _finish = sceneResources.finish;
            _finishChecker = sceneResources.finish.GetComponentInChildren<ResultChecker>();
            
            _platformManager = platformManager;
            _uiDisplayer = new ResultUiDisplayer(sceneResources.resultUiResources);
        }

        public void Initialize()
        {
            _failChecker.Initialize("player");
            _finishChecker.Initialize("player");
            
            SetFailCollider(_platformManager.CurrentPlatformCenter);
            
            SubscribeToFail(_uiDisplayer.ShowFailPage);
            SubscribeToSuccess(_uiDisplayer.ShowSuccessPage);
        }

        public void SubscribeToFail(Action action)
        {
            var fail = GetResult(ResultType.Fail);
            fail.Subscribe(action);
        }

        public void SubscribeToSuccess(Action action)
        {
            var success = GetResult(ResultType.Success);
            success.Subscribe(action);
        }
        
        public void Reset()
        {
            var failResult = GetResult(ResultType.Fail);
            failResult.Reset();

            var successResult = GetResult(ResultType.Success);
            successResult.Reset();
        }

        private GameResult GetResult(ResultType type)
        {
            if (_results.TryGetValue(type, out var result))
                return result;
            
            switch (type)
            {
                case ResultType.Fail :
                    result = new Fail( _failChecker);
                    break;
                case ResultType.Success :
                    result = new Success(_finishChecker);
                    break;
            }

            return result;
        }

        private void SetFailCollider(Vector3 playerStartPosition)
        {
            var middlePoint = (_finish.transform.position - playerStartPosition)/2f;
            middlePoint.y = _fail.transform.position.y;
            _fail.transform.position = middlePoint;

            var distance = Vector3.Distance(_finish.transform.position, playerStartPosition);

            var collider = _fail.GetComponent<BoxCollider>();
            var size = collider.size;
            size.z = distance;
            collider.size = size;
        }
    }
    
    public enum ResultType
    {
        Fail,
        Success
    }
}
