using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    // Start is called before the first frame update
    public List<CraftingRecipe> craftable = new List<CraftingRecipe>();
    void Start()
    {
        craftable = transform.parent.GetComponent<CraftingUI>().craftable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
