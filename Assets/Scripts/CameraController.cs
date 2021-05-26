using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float height;
    private Vector3 playerPos;
    private Vector3 cameraPos;

    void Awake()
    {

        player = transform.parent.gameObject;
        playerPos = player.transform.position;
        cameraPos = new Vector3(playerPos.x, playerPos.y, height);

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        cameraPos = new Vector3(playerPos.x, playerPos.y, height);
        transform.position = cameraPos;
    }
}
