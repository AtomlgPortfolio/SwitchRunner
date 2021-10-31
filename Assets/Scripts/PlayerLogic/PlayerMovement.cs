using System;
using System.Collections;
using DG.Tweening;
using Infrastructure.Services.Input;
using LevelLogic.UI;
using UnityEngine;
using Utils;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _minXBorderValue = -1.08f;
        [SerializeField] private float _maxXBorderValue = 1.08f;
        [SerializeField] private float _jumpDuration = 2f;
        [SerializeField] private float _jumpHeight = 0.5f;
        [SerializeField] private float _jumpDistance = 1.3f;
        [SerializeField] private float _verticalMovementSpeed = 1;

        private Transform _skeletonTransform;
        private Rigidbody _rigidbody;
        private StartGameView _startGameView;
        private PlayerAudio _playerAudio;
        private PlayerEffects _playerEffects;
        private PlayerAnimator _playerAnimator;
        private PlayerCollisionObserver _playerCollisionObserver;
        private PlayerShadow _playerShadow;
        private IInputService _inputService;
        private bool _verticalMovementAllowed;
        private float _horizontal;
        private bool _isJumpStarted;
        private bool _isGrounded = true;
        private bool _isJumping;
        private Vector3 _parabolaVector;
        private float _finishPointY = -0.062f;

        public event Action Jumped;

        [Inject]
        private void Construct(IInputService inputService, StartGameView startGameView, Player player)
        {
            _inputService = inputService;
            _startGameView = startGameView;
            _playerAnimator = player.PlayerAnimator;
            _playerCollisionObserver = player.PlayerCollisionObserver;
            _playerEffects = player.PlayerEffects;
            _playerShadow = player.PlayerShadow;
            _rigidbody = player.PlayerRigidbody;
            _skeletonTransform = player.SkeletonTransform;
            _playerAudio = player.PlayerAudio;
        }

        private void OnEnable()
        {
            _playerCollisionObserver.ObstacleCollided += ForbidMovement;
            _playerCollisionObserver.DeathZoneEnter += ForbidMovement;
            _playerCollisionObserver.StartJumpZoneEnter += OnStartJumpZoneEnter;
            _playerCollisionObserver.PlatformStartJumpZoneEnter += OnPlatformStartJumpZoneEnter;
            _playerCollisionObserver.FinishJumpZoneEnter += OnFinishJumpZoneEnter;
            _playerCollisionObserver.PlatformFinishJumpZoneEnter += OnPlatformFinishJumpZoneEnter;
            _playerCollisionObserver.FinishZoneEnter += ForbidMovement;
            _startGameView.Clicked += AllowMovement;
        }

        private void OnDisable()
        {
            _playerCollisionObserver.ObstacleCollided -= ForbidMovement;
            _playerCollisionObserver.DeathZoneEnter -= ForbidMovement;
            _playerCollisionObserver.StartJumpZoneEnter -= OnStartJumpZoneEnter;
            _playerCollisionObserver.PlatformStartJumpZoneEnter -= OnPlatformStartJumpZoneEnter;
            _playerCollisionObserver.FinishJumpZoneEnter -= OnFinishJumpZoneEnter;
            _playerCollisionObserver.PlatformFinishJumpZoneEnter -= OnPlatformFinishJumpZoneEnter;
            _playerCollisionObserver.FinishZoneEnter -= ForbidMovement;
            _startGameView.Clicked -= AllowMovement;
        }

        private void OnPlatformStartJumpZoneEnter()
        {
            _isGrounded = false;
            StartJump();
        }

        private void OnFinishJumpZoneEnter()
        {
            _rigidbody.useGravity = false;
            _playerAnimator.PlayRoll();
        }

        private void OnRollAnimationCompleted()
        {
            transform.position = _skeletonTransform.position;
            _rigidbody.useGravity = true;
            _playerShadow.Enable();
            _isGrounded = true;
            _playerAudio.PlayMove();
        }
    
        private void OnPlatformFinishJumpZoneEnter()
        {
            _playerShadow.Enable();
            _isGrounded = true;
            _playerAudio.PlayMove();
        }

        private void OnStartJumpZoneEnter()
        {
            _isGrounded = false;
            StartJump();
        }


        private void AllowMovement()
        {
            _verticalMovementAllowed = true;
            _playerAudio.PlayMove();
        }

        private void ForbidMovement()
        {
            _verticalMovementAllowed = false;
            _playerAnimator.StopWalk();
        }

        private void StartJump()
        {
            _isJumpStarted = true;
            _playerShadow.Disable();
            _playerAnimator.StopWalk();
            _playerAnimator.PlayJump();
            _playerAudio.PlayJump();
        }

        private void Update()
        {
            if (_verticalMovementAllowed)
            {
                TryToJump();
                TryToMove();
                Rotate();
            }
        }

        private void Rotate()
        {
            if (_inputService.Axis.x > 0)
            {
                transform.rotation = Quaternion.Euler(0,30,0);
            }
            else if (_inputService.Axis.x < 0)
            {
                transform.rotation = Quaternion.Euler(0,-30,0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
        }

        private void TryToMove()
        {
            Vector3 moveVector;
            _horizontal = Mathf.Clamp(transform.position.x + _inputService.Axis.x * Time.deltaTime, _minXBorderValue,
                _maxXBorderValue);
            var vertical = transform.position.z + _verticalMovementSpeed * Time.deltaTime;

            if (_isJumping)
                JumpMove();
            else if (_isGrounded) 
                Move(vertical);
        }

        private void Move(float vertical)
        {
            var moveVector = new Vector3(_horizontal, 0, vertical);
            transform.position = moveVector;
            _playerAnimator.PlayWalk();
            _playerEffects.Play();
        }

        private void JumpMove()
        {
            var moveVector = new Vector3(_horizontal, _parabolaVector.y, _parabolaVector.z);
            transform.position = moveVector;
            _playerEffects.Stop();
        }

        private void TryToJump()
        {
            if (_isJumpStarted)
            {
                _isJumping = true;
                StartCoroutine(ParabolaJump());
                _playerAnimator.StopWalk();
                _playerAnimator.PlayJump();
                _isJumpStarted = false;
            }
        }

        private IEnumerator ParabolaJump()
        {
            _parabolaVector = Vector3.zero;
            Vector3 position = transform.position;
            Vector3 finishPoint = Vector3.zero;
            finishPoint.z = transform.position.z + _jumpDistance;
            finishPoint.y = _finishPointY;

            float time = 0;
            float step = 0;
        
            while (time < 1 && transform.position.y != 0)
            {
                step += Time.deltaTime;
                time = step / _jumpDuration;
                _parabolaVector = MathParabola.Parabola(position, finishPoint, _jumpHeight, time);
                yield return null;
            }

            _isJumping = false;
            Jumped?.Invoke();
        }
    }
}