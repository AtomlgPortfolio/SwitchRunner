using Infrastructure.Services.Input;
using UnityEngine;

public class StandaloneInputService : InputService
{
    public override Vector2 Axis =>
        StandaloneInput();

    private Vector2 StandaloneInput() =>
        new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
}