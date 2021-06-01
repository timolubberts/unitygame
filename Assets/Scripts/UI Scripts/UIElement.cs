using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    public int id;
    public Vector2 startPos;
    public Transform hovered;

    private void Awake()
    {
        //hovered = transform.Find("hovered");
    }

    public virtual void UpdateUI()
    {

    }

}
