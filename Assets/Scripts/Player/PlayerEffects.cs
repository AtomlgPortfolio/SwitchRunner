using UnityEngine;

namespace Player
{
    public class PlayerEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _dustStepsEffect;

        public void Play()
        {
            if(!_dustStepsEffect.isPlaying)
                _dustStepsEffect.Play();
        }

        public void Stop()
        {
            if(_dustStepsEffect.isPlaying)
                _dustStepsEffect.Stop();
        }
    }
}
