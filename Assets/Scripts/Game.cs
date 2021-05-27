using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;
    public static Game Instance;

    public List<GameObject> itemPrefabs = new List<GameObject>();

    void Awake()
    {
        transform.DetachChildren();
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(transform.root.gameObject);
        }
        
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("Prefabs/Items");
        foreach(GameObject go in loadedPrefabs)
        {
            itemPrefabs.Add(go);
        }

    }

    private void Start()
    {
        Instantiate(SavedData.Instance.playerPrefab, SavedData.Instance.spawn, Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void PlaceItem()
    {
        if (!InventoryManager.Instance.IsEmpty())
        {
            try
            {
                Instantiate(GetObjectFromPrefabs(InventoryManager.Instance.inventory[InventoryManager.Instance.selected].slotItem), player.transform.position, Quaternion.identity);
                InventoryManager.Instance.RemoveFromInventory(InventoryManager.Instance.inventory[InventoryManager.Instance.selected].slotItem);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

        }
    }

    public void PlaceItem(Item item)
    {
        Instantiate(GetObjectFromPrefabs(item), player.transform.position, Quaternion.identity);

    }

    public void PickUp(GameObject item)
    {
        foreach(GameObject go in itemPrefabs)
        {
            ObjectItem oi = item.GetComponent<ObjectItem>();
            if (oi.prefabItemName == item.GetComponent<ObjectItem>().prefabItemName)
            {
                if (InventoryManager.Instance.IsFull())
                {
                    return;
                }
                Slot temp = new Slot(ItemDatabase.Instance.GetItem(oi.prefabItemName), 1);
                InventoryManager.Instance.AddToInventory(temp);
                Destroy(item);
                return;
            }
        }
        

    }

    public GameObject GetObjectFromPrefabs(Item target)
    {
        foreach(GameObject go in itemPrefabs)
        {
            if(go.GetComponent<ObjectItem>().prefabItemName == target.itemName) // Check if 
            {
                return go;
            }
        }
        return null;
    }

    public void ChangeScene(string sceneName, GameObject obj)
    {
        SavedData.Instance.spawn = obj.GetComponent<SceneTransition>().destination;
        InventoryManager.Instance.Save();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController.Instance.transform.position = SavedData.Instance.spawn;
    }

}
