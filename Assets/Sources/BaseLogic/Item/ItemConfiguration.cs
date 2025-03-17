using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    [CreateAssetMenu(fileName = "ItemConfiguration", menuName = "Configurations/Create new item configuration", order = 51)]
    public class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private string _identifier;

        public float Weight;
        public string Name;
        public ItemType Type;
        public ItemObject Prefab;
        public Vector3 StartPosition;

        public Guid Identifier
        {
            get
            {
                if (string.IsNullOrEmpty(_identifier))
                {
                    _identifier = Guid.NewGuid().ToString();
                }

                return new Guid(_identifier);
            }
        }
    }
}
