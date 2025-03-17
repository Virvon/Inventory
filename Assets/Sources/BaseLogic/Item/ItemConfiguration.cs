using UnityEngine;

namespace Assets.Sources.BaseLogic.Item
{
    [CreateAssetMenu(fileName = "ItemConfiguration", menuName = "Configurations/Create new item configuration", order = 51)]
    public class ItemConfiguration : ScriptableObject
    {
        public float Weight;
        public string Name;
        public ItemType Type;
        public ItemObject Prefab;
        public Vector3 StartPosition;

        [SerializeField] private string _identifier;

        public string Identifier
        {
            get
            {
                if (string.IsNullOrEmpty(_identifier))
                {
                    _identifier = Animator.StringToHash(Name).ToString();
                }

                return _identifier;
            }
        }
    }
}
