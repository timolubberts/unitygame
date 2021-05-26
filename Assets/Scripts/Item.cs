using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string itemName = "";
    public int stackSize;
    public int stackLimit;

    public Sprite itemSprite;

    public Item(string itemName, int stackSize, int stackLimit)
    {
        this.itemName = itemName;
        this.stackSize = stackSize;
        this.stackLimit = stackLimit;
    }


}
