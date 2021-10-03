using UnityEngine;

namespace Infrastructure.Services.Input
{
    public abstract class InputService: IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";

        public abstract Vector2 Axis { get; }
    }
}