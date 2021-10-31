using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    [SerializeField] private AudioClip _runAudioClip;
    [SerializeField] private AudioClip _jumpAudioClip;
    [SerializeField] private AudioClip _collideAudioClip;
    [SerializeField] private AudioClip _celebrateAudioClip;
    
    private void Awake() => 
        _audioSource = GetComponent<AudioSource>();

    public void PlayJump() => 
        PlayAudio(_jumpAudioClip, false);

    public void PlayMove() => 
        PlayAudio(_runAudioClip, true);

    public void PlayCollide() => 
        PlayAudio(_collideAudioClip, false);

    public void Celebrate() => 
        PlayAudio(_celebrateAudioClip, false);

    private void PlayAudio(AudioClip audioClip, bool loop)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.loop = loop;
        _audioSource.Play();
    }
}
