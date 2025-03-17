using System;
using UnityEngine;

namespace Assets.Sources.InputService
{
    public class InputService : IInputService
    {
        private bool _isDragged;

        public event Action<Vector2> Clicked;
        public event Action<Vector2> ClickEnded;
        public event Action<Vector2> Dragged;

        public InputService() =>
            _isDragged = false;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Clicked?.Invoke(Input.mousePosition);
                _isDragged = true;
            }

            if (Input.GetMouseButton(0) && _isDragged)
                Dragged?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && _isDragged)
            {
                ClickEnded?.Invoke(Input.mousePosition);
                _isDragged = false;
            }
        }
    }
}
