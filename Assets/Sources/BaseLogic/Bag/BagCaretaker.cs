using Assets.Sources.BaseLogic.Item;
using Assets.Sources.Services.SaveLoadData;
using System;

namespace Assets.Sources.BaseLogic.Bag
{
    class BagCaretaker : IDisposable
    {
        private readonly Model.Bag _originator;
        private readonly ISaveLoadService _saveLoadService;

        private IMemento _memento;

        public BagCaretaker(Model.Bag originator, ISaveLoadService saveLoadService)
        {
            _originator = originator;
            _saveLoadService = saveLoadService;

            _originator.ItemAdded.AddListener(OnBagChanged);
            _originator.ItemRemoved.AddListener(OnBagChanged);
        }

        public void Dispose()
        {
            _originator.ItemAdded.RemoveListener(OnBagChanged);
            _originator.ItemRemoved.RemoveListener(OnBagChanged);
        }

        private void OnBagChanged(ItemObject item) =>
            Save();

        private void Save()
        {
            _memento = _originator.Save();
            _saveLoadService.Save(_memento.GetBagData());
        }
    }
}
