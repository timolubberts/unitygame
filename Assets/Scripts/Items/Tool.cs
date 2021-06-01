using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    public string usage;
    public int durability;

    public Tool(string itemName, int stackSize, int stackLimit, string usage) : base(itemName, stackSize, stackLimit)
    {
        this.usage = usage;
    }

    public override void Use()
    {
        Debug.Log("Using " + itemName + "..");
        switch(usage){
            case "fishing":
                //InventoryManager.Instance.AddToInventory(new Slot(ItemDatabase.Instance.GetItem("Salmon"), 1));
                Game.Instance.PlaceItem(ItemDatabase.Instance.items["Salmon"]);
                break;
        }
    }
}
