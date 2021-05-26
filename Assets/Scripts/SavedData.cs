using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public static SavedData Instance;

    //InventoryManager
    public List<Slot> inventory = new List<Slot>();
    public int selected;

    //Spawns
    public Vector2 spawn = new Vector2(-1, -3);

    //Prefabs
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public GameObject playerPrefab;

    //Items
    public Item Apple = new Item("Apple", 1, 3);
    public Item Orange = new Item("Orange", 1, 3);
    public Item Fruits = new Item("Fruits", 1, 3);

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    


}
