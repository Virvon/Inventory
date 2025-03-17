using Assets.Sources.BaseLogic.Inventory;
using Assets.Sources.BaseLogic.Item;
using Assets.Sources.Services.SaveLoadData;
using System;

namespace Assets.Sources.Memento
{
    class BagCaretaker : IDisposable
    {
        private readonly Inventory _originator;
        private readonly ISaveLoadService _saveLoadService;

        private IMemento _memento;

        public BagCaretaker(Inventory originator, ISaveLoadService saveLoadService)
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

        private void OnBagChanged(ItemObject item)
        {
            Save();
        }

        private void Save()
        {
            _memento = _originator.Save();
            _saveLoadService.Save(_memento.GetBagData());
        }        
    }
}
