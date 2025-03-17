using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    public class PhysicalMovementComponent
    {
        private readonly Rigidbody _rigidbody;

        public PhysicalMovementComponent(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Move(Vector3 position)
        {
            _rigidbody.Move(position, _rigidbody.rotation);
        }
    }
}
