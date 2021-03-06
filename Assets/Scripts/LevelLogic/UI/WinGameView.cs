using DG.Tweening;
using UnityEngine;

namespace LevelLogic.UI
{
    public class WinGameView : MonoBehaviour
    {
        [SerializeField] private Transform _smile;
        [SerializeField] private float _duration;
    
        private void Start()
        {
            _smile.DOScale(1.2f, _duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1,LoopType.Yoyo);
        }
    }
}
