using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Slot> inventory = new List<Slot>();

    public int selected;
    private Game game;

    private void Awake()
    {

        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        Load();
    }

    public void AddToInventory(Slot s)
    {

        if (!InventoryContains(s, -1))
        {
            inventory.Add(s);
        }
        else
        {
            ChangeQuantity(s);
        }
        if (inventory.Count == 1)
        {
            selected = 0;
        }

        //game.uim.UpdateInventory();

    }

    public void RemoveFromInventory(Slot s)
    {

        if (selected == inventory.Count - 1 && s.quantity == 1)
        {
            //selected--;
            //inventory.Remove(s);
            Slot temp = new Slot(s.slotItem, -1);
            ChangeQuantity(temp);
        }
        else
        {
            Slot temp = new Slot(s.slotItem, -1);
            ChangeQuantity(temp);
        }
        //game.uim.UpdateInventory();
    }

    public bool IsEmpty()
    {
        if(inventory.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeSelected(float newPos)
    {
        int dir = Mathf.RoundToInt(newPos);
        if (dir == 1)
        {
            if (selected == 0)
            {
                selected = 8 - 1;
            }
            else
            {
                selected--;
            }
        }


        if (dir == -1)
        {
            if (selected == 8 - 1)
            {
                selected = 0;
            }
            else
            {
                selected++;
            }
        }
        if (!IsEmpty())
        {
            
            //game.uim.UpdateInventory();
        }

    }

    public Slot GetSlotFromItem(Item target)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(target.itemName == inventory[i].slotItem.itemName)
            {
                return inventory[i];
            }
        }
        return null;
    }

    public bool InventoryContains(Slot i, int quantity)
    {
        if(quantity != -1)
        {
            foreach (Slot s in inventory)
            {
                if (s.slotItem.itemName == i.slotItem.itemName && s.quantity == quantity)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            foreach (Slot s in inventory)
            {
                if (s.slotItem.itemName == i.slotItem.itemName)
                {
                    return true;
                }
            }
            return false;
        }
        
    }

    public void ChangeQuantity(Slot s)
    {

        foreach (Slot inv in inventory)
        {
            if(inv.slotItem == s.slotItem)
            {
                //if (inv.quantity == inv.slotItem.stackLimit) continue;
                int newQuant = inv.quantity + s.quantity;
                if (newQuant == 0)
                {
                    inventory.Remove(inv);
                }
                else if (newQuant > inv.slotItem.stackLimit)
                {
                    inventory.Add(new Slot(inv.slotItem, inv.slotItem.stackSize));
                }
                else
                {
                    inv.quantity = newQuant;
                }
                return;
            }
        }

        

    }

    public void Save()
    {
        SavedData.Instance.inventory = inventory;
        SavedData.Instance.selected = selected;
    }

    public void Load()
    {
        inventory = SavedData.Instance.inventory;
        selected = SavedData.Instance.selected;
        //game.uim.UpdateInventory();
    }



}
