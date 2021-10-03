using Infrastructure.Services.Input;
using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 Axis => 
        MobileInputAxis();

    private Vector2 MobileInputAxis()
    {
        float y = 0;
        float x = 0;
        if (Input.touchCount > 0)
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
}
