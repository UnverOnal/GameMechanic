using GameResource;
using UnityEngine;

namespace Result
{
    public class ResultUiDisplayer
    {
        private readonly ResultUiResources _uiResources;
        
        public ResultUiDisplayer(ResultUiResources uiResources)
        {
            _uiResources = uiResources;
        }
        
        public void ShowFailPage()
        {
            Show(true, _uiResources.failPage);
        }

        public void ShowSuccessPage()
        {
            Show(true, _uiResources.successPage);
        }
        
        public void HideFailPage()
        {
            Show(false, _uiResources.failPage);
        }

        public void HideSuccessPage()
        {
            Show(false, _uiResources.successPage);
        }

        private void Show(bool show, GameObject page)
        {
            page.SetActive(show);
        }
    }
}
