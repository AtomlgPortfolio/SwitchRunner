using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerObserver))]
[RequireComponent(typeof(PlayerEffects))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _minXBorderValue = -1.08f;
    [SerializeField] private float _maxXBorderValue = 1.08f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private float _height = 5f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private Vector3 _capsuleColliderJumpPosition;
    
    private PlayerEffects _playerEffects;
    private Vector3 _capsuleColliderOriginPosition;
    private CapsuleCollider _capsuleCollider;
    private PlayerHealth _playerHealth;
    private PlayerAnimator _playerAnimator;
    private PlayerObserver _playerObserver;
    private IInputService _inputService;
    private Vector2 _axis;
    private bool _verticalMovementAllowed;
    private float _horizontal;
    private bool _isJumpStarted;
    private bool _isGrounded = true;
    private Vector3 _velocity;
    private bool _isJumping;
    private Vector3 _parabolaVector;
    private Rigidbody _rigidbody;
    private float _finishPointY = -0.062f;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerObserver = GetComponent<PlayerObserver>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerEffects = GetComponent<PlayerEffects>();

        _capsuleColliderOriginPosition = _capsuleCollider.center;
        AllowMovement();
    }

    private void OnEnable()
    {
        _playerHealth.Died += ForbidMovement;
        _playerObserver.StartJumpZoneEnter += OnStartJumpZoneEnter;
        _playerObserver.PlatformJumpZoneEnter += OnPlatformJumpZoneEnter;
        _playerObserver.FinishJumpZoneEnter += OnFinishJumpZoneEnter;
        _playerObserver.FinishZoneEnter += ForbidMovement;
    }

    private void OnDisable()
    {
        _playerHealth.Died += ForbidMovement;
        _playerObserver.StartJumpZoneEnter -= OnStartJumpZoneEnter;
        _playerObserver.PlatformJumpZoneEnter -= OnPlatformJumpZoneEnter;
        _playerObserver.FinishJumpZoneEnter -= OnFinishJumpZoneEnter;
        _playerObserver.FinishZoneEnter -= ForbidMovement;
    }

    private void OnPlatformJumpZoneEnter()
    {
        _rigidbody.useGravity = false;
        StartJump();
    }

    private void OnFinishJumpZoneEnter()
    {
        _isGrounded = true;
        _capsuleCollider.center = _capsuleColliderOriginPosition;
    }

    private void OnStartJumpZoneEnter()
    {
        _isGrounded = false;
        StartJump();
    }


    private void AllowMovement()
    {
        _verticalMovementAllowed = true;
    }

    private void ForbidMovement()
    {
        _verticalMovementAllowed = false;
        _playerAnimator.StopWalk();
    }

    private void StartJump()
    {
        _isJumpStarted = true;
        _playerAnimator.StopWalk();
        _playerAnimator.PlayJump();
    }

    private void Update()
    {
        if (_verticalMovementAllowed)
        {
            TryToJump();
            TryToMove();
        }
    }

    private void TryToMove()
    {
        var moveVector = Vector3.zero;
        _horizontal = Mathf.Clamp(transform.position.x + _inputService.Axis.x * Time.deltaTime, _minXBorderValue,
            _maxXBorderValue);
        var vertical = transform.position.z + 1 * Time.deltaTime;

        if (_isJumping)
        {
            moveVector = new Vector3(_horizontal, _parabolaVector.y, _parabolaVector.z);
            transform.position = moveVector;
            _playerEffects.Stop();
        }
        else if (_isGrounded)
        {
            moveVector = new Vector3(_horizontal, 0, vertical);
            transform.position = moveVector;
            _playerAnimator.PlayWalk();
            _playerEffects.Play();
        }
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
        finishPoint.z = transform.position.z + _distance;
        finishPoint.y = _finishPointY;

        float time = 0;
        float step = 0;

        _capsuleCollider.enabled = false;
        while (time < 1 && transform.position.y != 0)
        {
            step += Time.deltaTime;
            time = step / _duration;
            _parabolaVector = MathParabola.Parabola(position, finishPoint, _height, time);
            yield return null;
        }

        _isJumping = false;
        yield return null;
        _rigidbody.useGravity = true;
        _capsuleCollider.center = _capsuleColliderJumpPosition;
        _capsuleCollider.enabled = true;
    }
}