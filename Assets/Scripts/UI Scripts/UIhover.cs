using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIhover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Transform hovered;

    private void Start()
    {
        hovered = transform.parent.GetComponent<UIElement>().hovered;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovered.gameObject.SetActive(true);
        hovered.position = transform.position;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered.gameObject.SetActive(false);
    }
}
