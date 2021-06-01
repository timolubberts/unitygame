using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //public List<Slot> inventory = new List<Slot>();
    public Dictionary<int, Slot> inventory = new Dictionary<int, Slot>();

    public int selected = 0;
    public int maxInventorySize = 24;

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

    }

    public void AddToInventory(Item i, int quantity,  bool selected)
    {
        if (IsFull())
        {
            return;
        }
        Slot temp = new Slot(i, quantity);
        ChangeQuantity(temp, selected);


    }
    public void RemoveFromInventory(Item i, int quantity, bool selected)
    {
        Slot temp = new Slot(i, -quantity);
        ChangeQuantity(temp, selected);
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

    public void ReplaceSlots(int start, int end)
    {
        Slot s1;
        Slot s2;

        if (inventory.ContainsKey(start))
        {
            s1 = inventory[start];
        }
        else
        {
            return;
        }

        if (!inventory.ContainsKey(end))
        {
            inventory.Add(end, s1);
            inventory.Remove(start);

        }
        else
        {
            s2 = inventory[end];
            inventory[start] = s2;
            inventory[end] = s1;
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
            foreach (Slot s in inventory.Values)
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
            foreach (Slot s in inventory.Values)
            {
                if(s != null){
                    if (s.slotItem.itemName == i.slotItem.itemName)
                    {
                        return true;
                    }
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

    public void ChangeQuantity(Slot s, bool selectedInvSlot)
    {
        int firstEmptySpot = -1;
        int ctr = 0;
        if (selectedInvSlot)
        {
            int newQuant = inventory[selected].quantity + s.quantity;
            if (newQuant == 0)
            {
                inventory.Remove(selected);
            }
            else
            {
                inventory[selected].quantity = newQuant;
            }
            return;
        }
        else
        {
            for (int i = 0; i < maxInventorySize; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    if (firstEmptySpot == -1) { firstEmptySpot = i; }
                    continue;
                }
                //Slot invSlot = inventory[i];
                if (inventory[i].slotItem == s.slotItem)
                {
                    int newQuant = inventory[i].quantity + s.quantity;
                    if (s.quantity < 0) // Remove quantity
                    {
                        if (newQuant == 0) // New quantity removes last item
                        {

                            if (inventory[i].quantity == s.slotItem.stackLimit) // If current inventory item is full
                            {
                                continue;
                            }
                            else
                            {
                                inventory.Remove(i);
                                return;

                            }
                        }
                        else
                        {
                            inventory[i].quantity = newQuant;
                        }
                        return;
                    }
                    else // Add quantity
                    {
                        if (inventory[i].quantity != s.slotItem.stackLimit)
                        {// Current inventory item is not full

                            inventory[i].quantity = newQuant;
                            return;
                        }
                    }

                }
                ctr++;
            }


            inventory.Add(firstEmptySpot, s);
            //inventory.Add(s);

        }
    }



}
