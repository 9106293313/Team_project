using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnPointerButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{

    public GameObject Arrow;
    public Transform ArrowPlace;
    public Transform ArrowPlaceLeave;
    public AudioSource SelectSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            //Debug.Log("Left Mouse Clicked.");
        }
        else if (eventData.pointerId == -2)
        {
            //Debug.Log("Right Mouse Clicked.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Enter..");
        Arrow.transform.position = ArrowPlace.transform.position;
        Arrow.transform.rotation = ArrowPlace.transform.rotation;
        SelectSound.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exit..");
        Arrow.transform.position = ArrowPlaceLeave.transform.position;
        Arrow.transform.rotation = ArrowPlaceLeave.transform.rotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down..");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragged..");
    }
}
