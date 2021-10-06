using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerSkeletonTransform;
    
        private bool _movementAllowed;
        private StartGamePanel _startGamePanel;
        private float _zOffset;
        private PlayerCollisionObserver _playerCollisionObserver;

        [Inject]
        private void Construct(StartGamePanel startGamePanel, PlayerCollisionObserver playerCollisionObserver)
        {
            _playerCollisionObserver = playerCollisionObserver;
            _startGamePanel = startGamePanel;
        }

        private void Start()
        {
            _zOffset = transform.position.z;
        }

        private void OnEnable()
        {
            _startGamePanel.Clicked += AllowMovement;
            _playerCollisionObserver.ObstacleCollided += ForbidMovement;
            _playerCollisionObserver.FinishZoneEnter += ForbidMovement;
            _playerCollisionObserver.NoPlatformJumpZoneEnter += ForbidMovement;
        }

        private void OnDisable()
        {
            _startGamePanel.Clicked -= AllowMovement;
            _playerCollisionObserver.ObstacleCollided -= ForbidMovement;
            _playerCollisionObserver.FinishZoneEnter -= ForbidMovement;
            _playerCollisionObserver.NoPlatformJumpZoneEnter -= ForbidMovement;
        }

        private void LateUpdate()
        {
            if (_movementAllowed)
            {
                Move();
            }
        }

        private void AllowMovement() => 
            _movementAllowed = true;

        private void ForbidMovement() => 
            _movementAllowed = false;

        private void Move()
        {
            Vector3 postion = transform.position;;
            postion.z = _playerSkeletonTransform.position.z + _zOffset;
            postion.z = Mathf.Clamp(postion.z, postion.z, _playerSkeletonTransform.position.z + _zOffset);
            transform.position = postion;
        }
    }
}