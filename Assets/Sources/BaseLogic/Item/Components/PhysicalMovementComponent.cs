using UnityEngine;

namespace Assets.Sources.BaseLogic.Item.Components
{
    public class PhysicalMovementComponent
    {
        private readonly Rigidbody _rigidbody;

        public PhysicalMovementComponent(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;

            Fixed = false;
        }

        public bool Fixed { get; private set; }

        public void SetGravity(bool isActive) =>
            _rigidbody.useGravity = isActive;

        public void Move(Vector3 position)
        {
            if (Fixed == false)
                _rigidbody.Move(position, _rigidbody.rotation);
        }

        public void SetFix(bool isFixed) =>
            Fixed = isFixed;
    }
}
