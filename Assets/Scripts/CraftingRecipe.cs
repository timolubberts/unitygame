using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe
{
    
    public List<Slot> required = new List<Slot>();
    public Slot reward;
    public int quantity;

    public CraftingRecipe(List<Slot> required, Slot reward, int quantity)
    {
        this.required = required;
        this.reward = reward;
        this.quantity = quantity;
    }

    //public CraftingRecipe Fruits = new CraftingRecipe(new List<Slot>(),)
}   
