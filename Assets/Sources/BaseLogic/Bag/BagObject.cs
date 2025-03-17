using Assets.Sources.BaseLogic.Bag.View;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Bag
{
    public class BagObject : EntityObject
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
