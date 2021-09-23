using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trailEffect;

    public void Play()
    {
        if(!_trailEffect.isPlaying)
            _trailEffect.Play();
    }

    public void Stop()
    {
        if(_trailEffect.isPlaying)
            _trailEffect.Stop();
    }
}
