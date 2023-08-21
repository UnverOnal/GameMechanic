using System;
using System.Collections.Generic;
using GameResource;
using Platform;
using UnityEngine;
using VContainer;

namespace Result
{
    public class ResultManager : IDisposable
    {
        private readonly GameObject _finish;
        private readonly GameObject _fail;
        
        private readonly ResultChecker _failChecker;
        private readonly ResultChecker _finishChecker;
        
        private readonly Collider _failCollider;

        private readonly ResultUiDisplayer _uiDisplayer;


        [Inject]
        public ResultManager(SceneResources sceneResources, PlatformManager platformManager)
        {
            _fail = sceneResources.fail;
            _failChecker = _fail.GetComponent<ResultChecker>();
            _failChecker.Initialize("Player");

            _finish = sceneResources.finish;
            _finishChecker = sceneResources.finish.GetComponentInChildren<ResultChecker>();
            _finishChecker.Initialize("Player");
            
            SetFailCollider(platformManager.CurrentPlatformCenter);
            
            _uiDisplayer = new ResultUiDisplayer(sceneResources.resultUiResources);
            SubscribeToFail(_uiDisplayer.ShowFailPage);
            SubscribeToSuccess(_uiDisplayer.ShowSuccessPage);
        }

        public void SubscribeToFail(Action action)
        {
            _failChecker.OnCollide += action;
        }        
        
        public void UnSubscribeFromFail(Action action)
        {
            _failChecker.OnCollide -= action;
        }

        public void SubscribeToSuccess(Action action)
        {
            _finishChecker.OnCollide += action;
        }
        
        public void UnSubscribeFromSuccess(Action action)
        {
            _finishChecker.OnCollide -= action;
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

        public void Dispose()
        {
            UnSubscribeFromFail(_uiDisplayer.ShowFailPage);
            UnSubscribeFromSuccess(_uiDisplayer.ShowSuccessPage);
        }
    }
}
