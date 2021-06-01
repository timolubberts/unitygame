using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Dictionary<int, GameObject> UIElements = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> ActiveUIElements = new Dictionary<int, GameObject>();

    public Canvas canvas;


    public bool mainInventoryIsOpen = false;



    private void Awake()
    {

        CreateReferences();


    }

    private void Start()
    {
        AddUIElement(UIElements[0]);

    }
    private void Update()
    {
        foreach (var ui in ActiveUIElements)
        {
            ui.Value.GetComponent<UIElement>().UpdateUI();
        }
    }
    public void AddUIElement(GameObject go)
    {
        GameObject element = Instantiate(go.gameObject, transform);
        ActiveUIElements.Add(go.GetComponent<UIElement>().id, element);

    }

    public void DestroyUIElement(GameObject go)
    {
        ActiveUIElements.Remove(go.GetComponent<UIElement>().id);
        Destroy(go);
    }
    public void CreateReferences()
    {
        ActiveUIElements.Clear();
        UIElements.Clear();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        //highlighted = canvas.transform.Find("highlighted").gameObject;

        Transform[] UIprefabs = Resources.LoadAll<Transform>("Prefabs/Game Functions/UI Elements");
        foreach (Transform go in UIprefabs)
        {
            if(go.GetComponent<UIElement>() == null) { continue; }
            UIElements.Add(go.GetComponent<UIElement>().id, go.gameObject);
        }

    }

    public void SwitchInventory()
    {
        if (!mainInventoryIsOpen) // If main inventory is NOT open when SwitchInventory() is called, it will open the main inventory..
        {

            mainInventoryIsOpen = true;
            DestroyUIElement(ActiveUIElements[0]);
            AddUIElement(UIElements[1]);
            AddUIElement(UIElements[2]);
            PlayerController.Instance.movementDisabled = true;

        }
        else if (mainInventoryIsOpen) // Else the hotbar will be activated
        {
            DestroyUIElement(ActiveUIElements[1]);
            DestroyUIElement(ActiveUIElements[2]);
            AddUIElement(UIElements[0]);
            mainInventoryIsOpen = false;
            PlayerController.Instance.movementDisabled = false;

        }
    }
}

