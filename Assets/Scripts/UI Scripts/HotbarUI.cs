using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HotbarUI : UIElement
{
    public List<Transform> hotbarImages = new List<Transform>();
    public List<Transform> hotbarText = new List<Transform>();
    public Transform highlighted;

    public bool active = true;

    void Awake()
    {
        foreach (Transform image in transform)
        {
            hotbarImages.Add(image);
        }
        foreach (Transform text in transform)
        {
            hotbarText.Add(text.Find(text.name + "_Text"));
        }
        highlighted = transform.Find("highlighted");
        transform.SetParent(Game.Instance.uim.canvas.transform);
        transform.localPosition = startPos;
    }

    private void Start()
    {
        foreach (Transform image in transform)
        {
            if (image.name == "highlighted")
            {
                continue;
            }
            image.GetComponent<Image>().enabled = false;
        }
        highlighted.SetAsFirstSibling();
    }


    public override void UpdateUI()
    {
        int selected = InventoryManager.Instance.selected;

        highlighted.transform.position = hotbarImages[selected].position;
        for (int i = 0; i < 8; i++)
        {
            string newText = "";
            if (InventoryManager.Instance.inventory.ContainsKey(i))
            {

                hotbarImages[i].GetComponent<Image>().enabled = true;
                hotbarImages[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("" + InventoryManager.Instance.inventory[i].slotItem.title);//invContent[i].slotItem.itemSprite;
                hotbarText[i].GetComponent<TextMeshProUGUI>().text = newText + InventoryManager.Instance.inventory[i].quantity;
            }
            else
            {
                hotbarImages[i].GetComponent<Image>().enabled = false;
                hotbarText[i].GetComponent<TextMeshProUGUI>().text = newText;
            }
        }


    }
}
