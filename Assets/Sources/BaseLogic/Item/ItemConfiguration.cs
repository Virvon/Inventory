using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    [CreateAssetMenu(fileName = "ItemConfiguration", menuName = "Configurations/Create new item configuration", order = 51)]
    public class ItemConfiguration : ScriptableObject
    {
        public float Weight;
        public string Name;
        public string Identifier;
        public ItemType Type;
        public ItemObject Prefab;
        public Vector3 StartPosition;
    }
}
