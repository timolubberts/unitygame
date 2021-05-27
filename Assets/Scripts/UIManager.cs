using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public Transform itembar;
    public List<Transform> slotImages = new List<Transform>();
    public List<Transform> slotText = new List<Transform>();

    private void Awake()
    {

        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        itembar = canvas.transform.Find("Itembar");
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
                newText += ">";
            }
            if(i < invContent.Count )
            {
                slotImages[i].GetComponent<Image>().enabled = true;
                //slotText[i].GetComponent<Text>().enabled = true;
                slotImages[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("" + ItemDatabase.Instance.GetItem(invContent[i].slotItem.itemName).title);//invContent[i].slotItem.itemSprite;
                slotText[i].GetComponent<Text>().text = newText + invContent[i].quantity;

            }
            else
            {
                slotImages[i].GetComponent<Image>().enabled = false;
                slotText[i].GetComponent<Text>().text = newText;
                //slotText[i].GetComponent<Text>().enabled = false;
            }


        }

    }
  
}
