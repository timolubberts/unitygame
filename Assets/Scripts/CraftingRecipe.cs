using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe
{
    
    public List<Slot> required = new List<Slot>();
    public Slot reward;

    public CraftingRecipe(List<Slot> required, Slot reward)
    {
        this.required = required;
        this.reward = reward;
    }

    //public CraftingRecipe Fruits = new CraftingRecipe(new List<Slot>(),)
}   
