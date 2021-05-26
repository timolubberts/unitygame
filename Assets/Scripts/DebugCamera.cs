using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour
{

    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        camera.targetDisplay = 2;
    }


}
