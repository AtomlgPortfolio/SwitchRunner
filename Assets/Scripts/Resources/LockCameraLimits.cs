using Cinemachine;
using UnityEngine;

namespace Resources
{
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's co-ordinates
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockCameraLimits : CinemachineExtension
    {
        [SerializeField] private bool _limitX;
        [SerializeField] private float _limitXPosition;
        [SerializeField] private bool _limitY;
        [SerializeField] private float _limitYPosition = 1.22f;
        [SerializeField] private bool _limitZ;
        [SerializeField] private float _limitZPosition;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;

                if (_limitX)
                    pos.x = _limitXPosition;
            
                if (_limitY)
                    pos.y = _limitYPosition;
            
                if (_limitZ)
                    pos.z = _limitZPosition;
            
                state.RawPosition = pos;
            }
        }
    }
}