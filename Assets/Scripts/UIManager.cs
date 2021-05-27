using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public Transform itembar;
    public Transform highlighted;
    public List<Transform> slotImages = new List<Transform>();
    public List<Transform> slotText = new List<Transform>();

    private void Awake()
    {

        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        itembar = canvas.transform.Find("itembar");
        highlighted = itembar.transform.Find("highlighted");

        foreach(Transform image in itembar)
        {
            slotImages.Add(image);
        }
        foreach(Transform text in itembar)
        {
            slotText.Add(text.Find(text.name + "_Text"));
        }
        
    }

    void FixedUpdate()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        List<Slot> invContent = InventoryManager.Instance.inventory;
        int selected = InventoryManager.Instance.selected;
        
        for (int i = 0; i < 8; i++)
        {
            string newText = "";
            if(i == selected)
            {
                highlighted.position = slotImages[i].position;
            }
            if(i < invContent.Count )
            {
                slotImages[i].GetComponent<Image>().enabled = true;
                //slotText[i].GetComponent<Text>().enabled = true;
                slotImages[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("" + ItemDatabase.Instance.items[invContent[i].slotItem.itemName].title);//invContent[i].slotItem.itemSprite;
                slotText[i].GetComponent<TextMeshProUGUI>().text = newText + invContent[i].quantity;

            }
            else
            {
                slotImages[i].GetComponent<Image>().enabled = false;
                slotText[i].GetComponent<TextMeshProUGUI>().text = newText;
                //slotText[i].GetComponent<Text>().enabled = false;
            }


        }

    }
  
}
