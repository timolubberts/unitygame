using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        Craft(ItemDatabase.Instance.GetRecipe("Fruits"));
    }

    public bool IsCraftable(CraftingRecipe cr)
    {
        int counter = 0;
        foreach(Slot s in cr.required)
        {
            foreach(Slot x in InventoryManager.Instance.inventory){
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
        if (IsCraftable(cr))
        {
            foreach(Slot s in cr.required)
            {
                InventoryManager.Instance.RemoveFromInventory(s.slotItem);
            }
            InventoryManager.Instance.AddToInventory(cr.reward);
        }
    }
}


