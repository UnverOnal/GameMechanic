using Cinemachine;
using UnityEngine;

namespace Camera
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that ignores the camera's Y co-ordinate
    /// </summary>
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class LockCamera : CinemachineExtension
    {
        [Tooltip("Lock the camera's y position to this value")]
        public float yPosition = 10;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y = yPosition;
                state.RawPosition = pos;
            }
        }
    }
}