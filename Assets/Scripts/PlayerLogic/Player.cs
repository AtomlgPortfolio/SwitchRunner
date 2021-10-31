using Infrastructure.Extensions;
using UnityEngine;

namespace PlayerLogic
{
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerCollisionObserver))]
    [RequireComponent(typeof(PlayerEffects))]
    [RequireComponent(typeof(PlayerShadow))]
    [RequireComponent(typeof(PlayerCelebration))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAudio))]
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _skeletonTransform;
        public PlayerAnimator PlayerAnimator { get; private set; }
        public PlayerCelebration PlayerCelebration { get; private set; }
        public PlayerCollisionObserver PlayerCollisionObserver { get; private set; }
        public PlayerEffects PlayerEffects { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerShadow PlayerShadow { get; private set; }
        public Rigidbody PlayerRigidbody { get; private set; }
        public PlayerAudio PlayerAudio { get; private set; }
        public Transform SkeletonTransform => _skeletonTransform;

        public  void Initialize()
        {
            PlayerAnimator = gameObject.GetAddComponent<PlayerAnimator>();
            PlayerCelebration = gameObject.GetAddComponent<PlayerCelebration>();
            PlayerCollisionObserver = gameObject.GetAddComponent<PlayerCollisionObserver>();
            PlayerHealth = gameObject.GetAddComponent<PlayerHealth>();
            PlayerMovement = gameObject.GetAddComponent<PlayerMovement>();
            PlayerShadow = gameObject.GetAddComponent<PlayerShadow>();
            PlayerEffects = gameObject.GetAddComponent<PlayerEffects>();
            PlayerRigidbody = gameObject.GetAddComponent<Rigidbody>();
            PlayerAudio = gameObject.GetAddComponent<PlayerAudio>();
        }
    }
}