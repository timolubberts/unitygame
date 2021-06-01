using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public bool IsCraftable(CraftingRecipe cr)
    {
        int counter = 0;
        foreach(Slot s in cr.required)
        {
            foreach(Slot x in InventoryManager.Instance.inventory.Values){
                if(x.slotItem == s.slotItem)
                {
                    if(x.quantity >= s.quantity)
                    {
                        counter++;
                        break;
                    }
                }
            }

        }
        if (counter == cr.required.Count)
        {
            return true;
        }

        return false;
    }

    public void Craft(CraftingRecipe cr)
    {
        //Debug.Log(cr.reward.quantity);
        if (IsCraftable(cr))
        {
            foreach(Slot s in cr.required)
            {
                InventoryManager.Instance.RemoveFromInventory(s.slotItem, s.quantity, false);
            }
            InventoryManager.Instance.AddToInventory(cr.reward.slotItem, cr.reward.quantity, false);

        }
    }

    public int IsCraftableAmount(CraftingRecipe cr)
    {
        int amount = 0;
        int counter = 0;
        Dictionary<int, Slot> tempInventory = InventoryManager.Instance.inventory;
        foreach (Slot s in cr.required)
        {
            foreach (Slot x in tempInventory.Values)
            {

                if (x.slotItem == s.slotItem)
                {
                    counter += (x.quantity - (x.quantity % s.quantity)) / s.quantity;

                }
            }
            if (counter <= amount || amount == 0)
            {
                amount = counter;
            }
        }

        return amount;
    }

}


