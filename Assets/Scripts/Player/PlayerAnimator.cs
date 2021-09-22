using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => 
        _animator = GetComponent<Animator>();

    private static class Animations
    {
        public static readonly int WalkHash = Animator.StringToHash("Walk");
        public static readonly int DieHash = Animator.StringToHash("Die");
        public static readonly int JumpHash = Animator.StringToHash("Jump");
        public static readonly int CelebrateHash = Animator.StringToHash("Celebrate");
    }

    public void ResetJumpAnimation() => 
        _animator.ResetTrigger(Animations.JumpHash);

    public void PlayWalk() => 
        _animator.SetBool(Animations.WalkHash, true);
    
    public void StopWalk() => 
        _animator.SetBool(Animations.WalkHash, false);
    
    public  void PlayDie() => 
        _animator.SetTrigger(Animations.DieHash);

    public void PlayJump() => 
        _animator.SetTrigger(Animations.JumpHash);
    
    public void PlayCelebrate() => 
        _animator.SetTrigger(Animations.CelebrateHash);
}
