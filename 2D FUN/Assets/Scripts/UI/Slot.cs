using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public bool isPlayerSlot;
    public bool isSkillSlot;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    

    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        //if EQ

        if (DragHandler.item.GetComponent<EqStats>())
        {
            if (DragHandler.item.GetComponent<EqStats>().Consumable && isPlayerSlot)
            {
                return;
            }
            else
            {
                DragHandler.item.transform.SetParent(transform);
            }
            if (DragHandler.item.GetComponent<EqStats>().Wearable && isPlayerSlot)
            {
                DragHandler.item.transform.SetParent(transform);
            }
        }



        // if skill
        if (DragHandler.item.GetComponent<AttackStats>() && isSkillSlot)
        {
            DragHandler.item.transform.SetParent(transform);
        }
        else
        {
            return;
        }


        if (!item)
            {
                DragHandler.item.transform.SetParent(transform);
            }

    }
    #endregion

}