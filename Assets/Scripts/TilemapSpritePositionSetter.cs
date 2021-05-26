using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSpritePositionSetter : MonoBehaviour
{

    void Awake()
    {
        SetPosition();
    }

    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;
        //GetComponent<TilemapRenderer>().sortingOrder = (int)transform.position.y*-1;
    }

}
