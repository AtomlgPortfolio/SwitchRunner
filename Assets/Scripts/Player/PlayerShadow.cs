using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private GameObject _shadow;

    public void Enable()
    {
        _shadow.SetActive(true);
    }

    public void Disable()
    {
        _shadow.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        _shadow.transform.position = position;
    }
}
