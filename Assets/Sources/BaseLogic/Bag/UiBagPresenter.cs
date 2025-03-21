﻿using Assets.Sources.BaseLogic.Bag.View;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag
{
    public class UiBagPresenter : IDisposable
    {
        private readonly Model.Bag _model;
        private readonly UiBagView _view;
        private readonly IInputService _inputService;
        private readonly ClickResieverComponent _clickResierver;

        public UiBagPresenter(Model.Bag model, UiBagView view, IInputService inputService, ClickResieverComponent clickResiever)
        {
            _model = model;
            _view = view;
            _inputService = inputService;
            _clickResierver = clickResiever;

            _view.Change(_model.Items);
            _view.Hide();

            _clickResierver.Clicked += OnBagClicked;
            _inputService.ClickEnded += OnClickEnded;
            _model.ItemAdded.AddListener(OnItemAdded);
            _view.ItemRemoved += OnViewTryItemRemoved;
            _model.ItemRemoved.AddListener(OnItemRemoved);
        }

        public void Dispose()
        {
            _clickResierver.Clicked -= OnBagClicked;
            _inputService.ClickEnded -= OnClickEnded;
            _model.ItemAdded.RemoveListener(OnItemAdded);
            _view.ItemRemoved -= OnViewTryItemRemoved;
            _model.ItemRemoved.RemoveListener(OnItemRemoved);
        }

        private void OnItemRemoved(ItemObject item)
        {
            _view.Remove(item);
        }

        private void OnBagClicked()
        {
            _view.Show();
        }

        private void OnClickEnded(Vector2 position)
        {
            _view.Hide();
        }

        private void OnItemAdded(ItemObject item)
        {
            _view.Change(_model.Items);
        }

        private void OnViewTryItemRemoved(ItemObject item)
        {
            _model.Remove(item);
        }
    }
}
