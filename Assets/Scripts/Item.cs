using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName { get; set; }
    public string title { get; set; }
    public int stackSize { get; set; }
    public int stackLimit { get; set; }
    public Sprite itemSprite { get; set; }

    public Item(string itemName, int stackSize, int stackLimit)
    {
        this.itemName = itemName;
        title = itemName.Replace(' ', '_').ToLower();
        this.stackSize = stackSize;
        this.stackLimit = stackLimit;
        //Debug.Log(title);
       
    }

    public virtual void Use()
    {
        
    }
}
