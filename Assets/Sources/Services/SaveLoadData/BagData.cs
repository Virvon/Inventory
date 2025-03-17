using System;

namespace Assets.Sources.Services.SaveLoadData
{
    [Serializable]
    public class BagData : IMemento
    {
        public string[] ItemIdentifiers;

        public BagData(string[] temIdentifiers)
        {
            ItemIdentifiers = temIdentifiers;
        }

        public BagData GetBagData() =>
            this;
    }
}
