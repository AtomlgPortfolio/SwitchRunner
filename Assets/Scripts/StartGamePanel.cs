using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGamePanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _hand;
    [SerializeField] private float _handMoveSpeed;
    [SerializeField] private Vector3[] _path;

    public event Action Clicked;

    private void Start()
    {
        _hand
            .DOLocalPath(_path, _handMoveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetSpeedBased();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        gameObject.SetActive(false);
    }
}