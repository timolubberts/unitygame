using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    public string prefabItemName;

    public Item item;

    private void Start()
    {
        item = ItemDatabase.Instance.GetItem(prefabItemName);
    }
}
