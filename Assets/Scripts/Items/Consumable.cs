using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int effect { get; set; }

    public Consumable(string itemName, int stackSize, int stackLimit, int effect) : base (itemName, stackSize, stackLimit)
    {
        this.effect = effect;
    }

    public override void Use()
    {
        Debug.Log("Using " + itemName + "..");
        PlayerController.Instance.Feed(effect);
        InventoryManager.Instance.RemoveFromInventory(this, 1, true);
    }
}
