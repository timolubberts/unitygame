using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public static SavedData Instance;


    //Spawns
    public Vector2 spawn = new Vector2(-1, -3);

    //Prefabs
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public GameObject playerPrefab;

    //Items
    
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
