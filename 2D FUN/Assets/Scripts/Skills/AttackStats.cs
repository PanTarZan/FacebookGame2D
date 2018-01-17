
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AttackStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public float skillDamage;
    public float skillCost;
    public string Class;
    public string skillName;
    public string skillDescription;
    public bool skillLocked = true;
    public float expNeeded;
    public Sprite DisabledSprite;
    public Sprite StartingSprite;
    public Button thisButton;
    public UIControls uiControls;
    void Start()
    {
        StartingSprite = gameObject.GetComponent<Image>().sprite;
        var GAMEMASTER = GameObject.Find("GAME MASTER");
        uiControls = GAMEMASTER.GetComponent<UIControls>();
        thisButton = gameObject.GetComponent<Button>();
    }
    void Update()
    {
        if (GameMasterScript.selectedPlayer.GetComponent<PLAYERSTATS>().Experience < expNeeded)
        {
            gameObject.GetComponent<Image>().sprite = DisabledSprite;
            skillLocked = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = StartingSprite;
            skillLocked = false;
        }
        ButtonInteractionsHandler();
    }
    void ButtonInteractionsHandler()
    {
        if (skillLocked)
        {
            thisButton.interactable = false;
            return;
        }


        if (GameMasterScript.IsBattleOver)
        {
            thisButton.interactable = false;
        }
        else
        {
            if (GameMasterScript.actionTaken)
            {
                thisButton.interactable = false;
            }
            else
            {
                thisButton.interactable = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiControls.UiDesc.SetActive(true);
        uiControls.UIInv.SetActive(false);

        if (GameMasterScript.selectedPlayer == null)
        {
            return;
        }

        uiControls.UiDescTitle.text = skillName;
        uiControls.UiDescDescription.text = skillDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiControls.UiDesc.SetActive(false);
        uiControls.UIInv.SetActive(true);
    }
}

