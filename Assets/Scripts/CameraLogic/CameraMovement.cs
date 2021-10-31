using LevelLogic.UI;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        private bool _movementAllowed;
        private float _zOffset;
        private StartGameView _startGameView;
        private Transform _playerSkeletonTransform;
        private PlayerCollisionObserver _playerCollisionObserver;

        [Inject]
        private void Construct(StartGameView startGameView, Player player)
        {
            _playerCollisionObserver = player.PlayerCollisionObserver;
            _playerSkeletonTransform = player.SkeletonTransform;
            _startGameView = startGameView;
        }

        private void Start()
        {
            _zOffset = transform.position.z;
        }

        private void OnEnable()
        {
            _startGameView.Clicked += AllowMovement;
            _playerCollisionObserver.ObstacleCollided += ForbidMovement;
            _playerCollisionObserver.FinishZoneEnter += ForbidMovement;
            _playerCollisionObserver.NoPlatformJumpZoneEnter += ForbidMovement;
        }

        private void OnDisable()
        {
            _startGameView.Clicked -= AllowMovement;
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