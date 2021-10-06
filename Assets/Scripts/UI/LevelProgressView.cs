using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;

        public void SetFillAmount(float value) => 
            _progressImage.fillAmount = value;
    }
}
