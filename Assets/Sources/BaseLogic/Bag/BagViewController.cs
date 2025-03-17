using Assets.Sources.BaseLogic.Bag.Model;
using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag
{
    public class BagViewController : IDisposable
    {
        private readonly Model.Bag _model;
        private readonly BagView _view;

        public BagViewController(Model.Bag inventory, BagView bagView)
        {
            _model = inventory;
            _view = bagView;

            _view.Initialize(_model.Items);

            _view.ItemAddTried += OnItemAddTried;

            _model.ItemRemoved.AddListener(OnItemRemoved);
            _model.ItemAdded.AddListener(OnItemAdded);
        }

        public void Dispose()
        {
            _view.ItemAddTried -= OnItemAddTried;
            _model.ItemRemoved.RemoveListener(OnItemRemoved);
            _model.ItemAdded.RemoveListener(OnItemAdded);
        }

        private void OnItemAddTried(ItemObject item) =>
            _model.TryAdd(item);

        private void OnItemRemoved(ItemObject item) =>
            _view.Remove(item);

        private void OnItemAdded(ItemObject item) =>
            _view.Change(_model.Items);
    }
}
