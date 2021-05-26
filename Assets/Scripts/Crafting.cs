using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<Item> allItems = new List<Item>();
    public Game game;

    public List<Slot> fruitSlots;
    public CraftingRecipe fruits;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        foreach (GameObject go in SavedData.Instance.itemPrefabs)
        {
            allItems.Add(go.GetComponent<Item>());
        }
        fruitSlots = new List<Slot>() { new Slot(allItems[0], 1), new Slot(allItems[1], 1) };
        fruits = new CraftingRecipe(fruitSlots, new Slot(allItems[2], 1), 1);

        
    }

    void Update()
    {
        Craft(fruits);
    }

    public bool IsCraftable(CraftingRecipe cr)
    {
        int counter = 0;
        foreach(Slot s in cr.required)
        {
            foreach(Slot x in game.im.inventory){
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
                game.im.RemoveFromInventory(s);
            }
            game.im.AddToInventory(cr.reward);
        }
    }
}


