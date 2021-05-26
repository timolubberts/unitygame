using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;

    //Managers
    public UIManager uim;
    public InventoryManager im;
    public SavedData sd;
    public Crafting c;



    void Awake()
    {
        Instantiate(SavedData.Instance.playerPrefab, SavedData.Instance.spawn, Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player");


        c = GameObject.FindGameObjectWithTag("Crafting").GetComponent<Crafting>();
        sd = GameObject.FindGameObjectWithTag("SavedData").GetComponent<SavedData>();
        uim = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        im = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<InventoryManager>();
    }

    public void PlaceItem(Vector3 position)
    {
        if (!im.IsEmpty())
        {
            try
            {
                Instantiate(GetObjectFromPrefabs(im.inventory[im.selected].slotItem), player.transform.position, Quaternion.identity);
                im.RemoveFromInventory(im.inventory[im.selected]);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            

        }
    }

    public void PickUp(GameObject item)
    {
        Item i = GetObjectFromPrefabs(item.GetComponent<Item>()).GetComponent<Item>();
        Slot temp = new Slot(i, i.stackSize);
        im.AddToInventory(temp);
        Destroy(item);

    }

    public GameObject GetObjectFromPrefabs(Item target)
    {
        foreach(GameObject go in SavedData.Instance.itemPrefabs)
        {
            if(go.GetComponent<Item>().itemName == target.itemName)
            {
                return go;
            }
        }
        return null;
    }

    public void ChangeScene(string sceneName, GameObject obj)
    {
        SavedData.Instance.spawn = obj.GetComponent<SceneTransition>().destination;

        im.Save();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
