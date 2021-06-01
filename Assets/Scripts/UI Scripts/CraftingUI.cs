using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : UIElement
{
    public List<Transform> craftingImages = new List<Transform>();
    public List<Transform> craftingText = new List<Transform>();

    public List<CraftingRecipe> craftable = new List<CraftingRecipe>();
    void Awake()
    {
        foreach (Transform image in transform)
        {
            
            craftingImages.Add(image);
        }
        foreach (Transform text in transform)
        {
            craftingText.Add(text.Find(text.name + "_Text"));
        }
        transform.SetParent(Game.Instance.uim.canvas.transform);
        transform.localPosition = startPos;
    }

    private void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            int j = i;
            craftingImages[i].GetComponent<Image>().enabled = false;
            craftingImages[i].GetComponent<Button>().onClick.AddListener(() => OnClick(j));
        }
        hovered.SetAsFirstSibling();
        craftable = ItemDatabase.Instance.GetCraftableRecipes();
    }

    public override void UpdateUI()
    {
        string newText = "";
        for (int i = 0; i < 24; i++)
        {
            if (i < craftable.Count)
            {
                craftingImages[i].GetComponent<Image>().enabled = true;
                craftingImages[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("" + craftable[i].reward.slotItem.title);
                craftingText[i].GetComponent<TextMeshProUGUI>().text = newText + ItemDatabase.Instance.crafting.IsCraftableAmount(craftable[i]);
            }
            else
            {
                craftingImages[i].GetComponent<Image>().enabled = false;
                craftingText[i].GetComponent<TextMeshProUGUI>().text = newText;
            }

        }
    }

    public void OnClick(int index)
    {

        if(index < craftable.Count) { ItemDatabase.Instance.crafting.Craft(craftable[index]); ; }

        craftable = ItemDatabase.Instance.GetCraftableRecipes();
    }

}
