using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<Slot> inventory = new List<Slot>();

    public int selected = 0;
    public int maxInventorySize = 8;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Load();
    }

    public void AddToInventory(Slot s)
    {
        if (IsFull())
        {
            return;
        }

        if (!InventoryContains(s, -1))
        {
            inventory.Add(s);
        }
        else
        {
            ChangeQuantity(s);
        }


    }

    public void RemoveFromInventory(Item i)
    {
        Slot temp = new Slot(i, -1);
        ChangeQuantity(temp);
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

    public bool IsFull()
    {
        if(inventory.Count <= maxInventorySize)
        {
            return false;
        }
        return true;
    }

    public void ChangeQuantity(Slot s)
    {
        int ctr = 0;
        foreach (Slot inv in inventory)
        {
            if(inv.slotItem == s.slotItem)
            {
                int newQuant = inv.quantity + s.quantity;
                if(s.quantity == -1) // Remove quantity
                {
                    if(newQuant == 0) // New quantity removes last item
                    {
                        inventory.RemoveAt(ctr);
                    }
                    else
                    {
                        inv.quantity = newQuant;
                    }
                    return; ;
                }
                else // Add quantity
                {
                    if(inv.quantity != s.slotItem.stackLimit) // Current inventory item is not full
                    {
                        inv.quantity = newQuant;
                        return;
                    }
                    else
                    {
                        continue;
                    }

                }

            }
            ctr++;
        }
        inventory.Add(s);

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
