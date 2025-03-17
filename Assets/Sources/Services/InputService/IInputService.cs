using System;
using UnityEngine;

namespace Assets.Sources.InputService
{
    public interface IInputService
    {
        event Action<Vector2> Clicked;
        event Action<Vector2> ClickEnded;
        event Action<Vector2> Dragged;

        void Update();
    }
}
