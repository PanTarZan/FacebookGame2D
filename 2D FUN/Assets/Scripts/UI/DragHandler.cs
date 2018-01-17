using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 debugPositionItem;
    public Vector3 debugPositionMouse;


    public static GameObject item;
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {

        item = gameObject;
        
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        item.GetComponent<LayoutElement>().ignoreLayout = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("UIMANAGER").transform);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (transform.parent.GetComponent<Slot>() == null)
        {
            transform.SetParent(startParent);
            transform.position = startPosition;
        } 
     
         transform.position = startPosition;
         
         
         


        GetComponent<CanvasGroup>().blocksRaycasts = true;
        item.GetComponent<LayoutElement>().ignoreLayout = false;
        item = null;
    }

}
