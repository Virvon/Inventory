using Assets.Sources.BaseLogic.Item;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuiList : MonoBehaviour
{
    [SerializeField] private List<ItemType> _types;

    private Dictionary<ItemType, TMP_Text> _dictionary = new();

    private void Start()
    {
        int count = transform.childCount;

        for(int i = 0; i < count; i++)
        {
            _dictionary.Add(_types[i], transform.GetChild(i).GetComponent<TMP_Text>());
        }
    }

    public TMP_Text Get(ItemType type)
    {
        return _dictionary.TryGetValue(type, out TMP_Text value) ? value : null;
    }

    public bool CheackHandleIntersection(Vector2 handlePosition, out ItemType itemType)
    {
        foreach(KeyValuePair<ItemType, TMP_Text> dictionary in _dictionary)
        {           
            if(RectTransformUtility.RectangleContainsScreenPoint(dictionary.Value.rectTransform, handlePosition))
            {
                itemType = dictionary.Key;

                return true;
            }
        }

        itemType = default;

        return false;
    }
}
