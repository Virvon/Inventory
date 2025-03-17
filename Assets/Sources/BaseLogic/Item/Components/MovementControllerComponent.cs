using System;

namespace Assets.Sources.BaseLogic.Item.Components
{
    public class MovementControllerComponent : IDisposable
    {
        private readonly ParentChangerComponent _parentChangerComponent;
        private readonly PhysicalMovementComponent _physicalMovementComponent;

        public MovementControllerComponent(ParentChangerComponent parentChangerComponent, PhysicalMovementComponent physicalMovementComponent)
        {
            _parentChangerComponent = parentChangerComponent;
            _physicalMovementComponent = physicalMovementComponent;

            _parentChangerComponent.FixChanged += OnFixChanged;
        }

        public void Dispose() =>
            _parentChangerComponent.FixChanged += OnFixChanged;

        private void OnFixChanged(bool isFixed) =>
            _physicalMovementComponent.SetFix(isFixed);
    }
}
