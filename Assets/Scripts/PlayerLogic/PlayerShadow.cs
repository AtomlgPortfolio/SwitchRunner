using UnityEngine;

namespace PlayerLogic
{
    public class PlayerShadow : MonoBehaviour
    {
        [SerializeField] private GameObject _shadow;

        public void Enable() => 
            _shadow.SetActive(true);

        public void Disable() => 
            _shadow.SetActive(false);
    }
}
