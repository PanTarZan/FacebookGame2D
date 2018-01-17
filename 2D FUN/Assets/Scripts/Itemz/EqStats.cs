using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class EqStats : MonoBehaviour, IPointerClickHandler {

    [Header("Item Class")]
    public bool equipped = false;
    public bool Wearable = false;
    public bool Consumable = false;

    

    [Header("Wearable")]

    public float addPlayerHealth;
    public int addDamageBase;
    public int addCritPrecent;
    public int addDodgePrecent;

    [Header("Consumable")]

    public float restoreHpAmount;
    
   
    public void OnPointerClick(PointerEventData eventData)
    {
       
        if (GameMasterScript.selectedItem == gameObject)
        {
            GameMasterScript.selectedPlayer.GetComponent<PLAYERSTATS>().UseConsumable(GameMasterScript.selectedItem);
            GameMasterScript.selectedItem = null;
            return;
        }
        
        GameMasterScript.selectedItem = gameObject;
    }
}
