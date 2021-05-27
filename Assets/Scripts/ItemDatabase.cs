using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    //public List<Item> items = new List<Item>();
    public Dictionary<string, Item> items = new Dictionary<string, Item>();
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

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
        BuildDatabase();

    }

    public Item GetItem(string itemName)
    {
        foreach (Item i in items)
        {
         
            if(i.itemName == itemName)
            {
                return i;
            }
        }
        return null;
    }

    public CraftingRecipe GetRecipe(string itemName)
    {
        foreach (CraftingRecipe cr in recipes)
        {

            if (cr.reward.slotItem == GetItem(itemName))
            {
                return cr;
            }
        }
        return null;
    }
    void BuildDatabase()
    {
        items.Add("Apple", new Consumable("Apple", 1, 3, 2));
        items.Add("Orange", new Consumable("Orange", 1, 3, 2));
        items.Add("Fruits", new Consumable("Fruits", 1, 3, 6));
        items.Add("Salmon", new Consumable("Salmon", 1, 3, 1));
        items.Add("De Klok Bier", new Consumable("De Klok Bier", 1, 10, 10));

        items.Add("Wooden Fishing Rod", new Tool("Wooden Fishing Rod", 1, 1, "fishing"));

        recipes.Add(new CraftingRecipe(new List<Slot>(){new Slot(GetItem("Apple"), 1), new Slot(GetItem("Orange"), 1)}, new Slot(GetItem("Fruits"), 1), 1));
        


    }
}
