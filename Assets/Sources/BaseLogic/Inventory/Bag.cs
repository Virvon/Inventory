using UnityEngine;

namespace Assets.Sources.BaseLogic.Inventory
{
    public class Bag : EntityObject
    {
        [SerializeField] private ClickResieverComponent _clickResieverComponent;
        [SerializeField] private BagView _bagView;

        private void Awake()
        {
            Add(_clickResieverComponent);
            Add(_bagView);
        }
    }
}
