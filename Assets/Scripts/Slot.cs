using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    public Item slotItem;
    public int quantity;

    public Slot(Item slotItem, int quantity)
    {
        this.slotItem = slotItem;
        this.quantity = quantity;
        
    }
}
