using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService: IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";


        public Vector2 Axis
        {
            get
            {
#if UNITY_STANDALONE
                return StandaloneInput();
#else
                return MobileInput();
#endif
            }
        }

        private Vector2 MobileInput()
        {
            float y = 0;
            float x = 0;
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Stationary:
                        y = 1;
                        break;
                    case TouchPhase.Moved:
                        y = 1;
                        if (touch.deltaPosition.x > 0)
                        {
                            x = 1;
                        }
                        else if (touch.deltaPosition.x < 0)
                        {
                            x = -1;
                        }

                        break;
                }
            }

            return new Vector2(x, y);
        }

        private Vector2 StandaloneInput() =>
            new Vector2(UnityEngine.Input.GetAxisRaw(Horizontal), UnityEngine.Input.GetAxisRaw(Vertical));
    }
}