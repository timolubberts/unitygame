using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUI : UIElement
{
    public List<Transform> mainInventoryImages = new List<Transform>();
    public List<Transform> mainInventoryText = new List<Transform>();

    public Transform highlighted;

    public bool active = true;
    void Awake()
    {
        foreach (Transform image in transform)
        {
            mainInventoryImages.Add(image);
        }
        foreach (Transform text in transform)
        {
            mainInventoryText.Add(text.Find(text.name + "_Text"));
        }
        highlighted = transform.Find("highlighted");
        transform.SetParent(Game.Instance.uim.canvas.transform);
        transform.localPosition = startPos;
    }

    private void Start()
    {
        for(int i = 0; i < 24; i++)
        {
            int j = i;
            mainInventoryImages[i].GetComponent<Image>().enabled = false;
            mainInventoryImages[i].GetComponent<Button>().onClick.AddListener(() => OnClick(j));
        }
        highlighted.SetAsFirstSibling();
        hovered.SetSiblingIndex(1);

    }


    public override void UpdateUI()
    {
        int selected = InventoryManager.Instance.selected;

        highlighted.transform.position = mainInventoryImages[selected].position;
        for(int i = 0; i < 24; i++)
        {
            string newText = "";
            if (InventoryManager.Instance.inventory.ContainsKey(i))
            {

                mainInventoryImages[i].GetComponent<Image>().enabled = true;
                mainInventoryImages[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("" + InventoryManager.Instance.inventory[i].slotItem.title);//invContent[i].slotItem.itemSprite;
                mainInventoryText[i].GetComponent<TextMeshProUGUI>().text = newText + InventoryManager.Instance.inventory[i].quantity;
            }
            else
            {
                mainInventoryImages[i].GetComponent<Image>().enabled = false;
                mainInventoryText[i].GetComponent<TextMeshProUGUI>().text = newText;
            }
        }

    }

    public int holding = -1;
    public void OnClick(int index)
    {
        if(InventoryManager.Instance.inventory.ContainsKey(index))
        {
            holding = index;
            return;
        }
        else
        {
            InventoryManager.Instance.ReplaceSlots(holding, index);
            holding = -1;
        }
    }

    
}

