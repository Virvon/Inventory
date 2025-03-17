using Assets.Sources.BaseLogic.Item;
using System;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class BagViewController : IDisposable
    {
        private readonly Inventory _model;
        private readonly BagView _view;

        public BagViewController(Inventory inventory, BagView bagView)
        {
            _model = inventory;
            _view = bagView;

            _view.ItemAddTried += OnItemAddTried;
            _model.ItemRemoved += OnItemRemoved;
            _model.ItemAdded += OnItemAdded;
        }

        public void Dispose()
        {
            _view.ItemAddTried -= OnItemAddTried;
            _model.ItemRemoved -= OnItemRemoved;
            _model.ItemAdded -= OnItemAdded;
        }

        private void OnItemAddTried(ItemObject item) =>
            _model.TryAdd(item);

        private void OnItemRemoved(ItemObject item) =>
            _view.Remove(item);

        private void OnItemAdded() =>
            _view.Change(_model.Items);
    }
}
