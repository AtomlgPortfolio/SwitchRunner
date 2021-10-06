using UnityEngine;

namespace ActiveElements
{
    public class ActiveElement : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
    
        public virtual void Deactivate()
        {
            _collider.enabled = false;
        }

        public virtual void Activate()
        {
            _collider.enabled = true;
        }
    }
}
