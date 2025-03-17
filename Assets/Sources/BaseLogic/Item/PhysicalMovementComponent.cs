using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    public class PhysicalMovementComponent
    {
        private readonly Rigidbody _rigidbody;
        private readonly ParentChangerComponent _parentChangerComponent;

        public PhysicalMovementComponent(Rigidbody rigidbody, ParentChangerComponent parentChangerComponent)
        {
            _rigidbody = rigidbody;
            _parentChangerComponent = parentChangerComponent;
        }

        public bool CanMoved => _parentChangerComponent.Fixed == false;

        public void SetGravity(bool isActive) =>
            _rigidbody.useGravity = isActive;

        public void Move(Vector3 position)
        {
            if(CanMoved)
                _rigidbody.Move(position, _rigidbody.rotation);
        }
    }
}
