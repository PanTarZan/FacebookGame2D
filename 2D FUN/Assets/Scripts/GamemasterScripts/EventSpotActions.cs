using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSpotActions : MonoBehaviour {

    public bool triggered = false;
    public bool spawnPVECharacter;
    public bool textAndActionEvent;
    public GameObject spawnEffect;
    [Header("UI Grrrr")]

    public Button ExitButton;
    public Button NextButton;
    public Button PrevButton;
    public GameObject PVECanvas;
    public Text TextSpace;

    public int page = 0;
    

    [Header("Text Events strings")]

    public string[] text;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        

    }

    public void SpawnPVECharacter()
    {

        triggered = true;
    }

    public void TextActionEvent()
    {
        triggered = true;
        PlayerMovement.canPlayerMove = false;
        PVECanvas.SetActive(true);
        SetButtonActionsITP();
        TextSpace.text = text[page];
        PrevButton.gameObject.SetActive(false);

    }
    void Exit()
    {
        Debug.Log("Exit");
        PlayerMovement.canPlayerMove = true;
        PVECanvas.SetActive(false);
        RemoveButtonActionsITP();
    }

    void Next()
    {
        page++;
        PrevButton.gameObject.SetActive(true);
        if (page == text.Length - 1)
        {
            NextButton.gameObject.SetActive(false);
        }
        Debug.Log("Next");
        TextSpace.text = text[page];
    }
    void Prev()
    {
        page--;
        NextButton.gameObject.SetActive(true);
        if (page == 0)
        {
            PrevButton.gameObject.SetActive(false);

        }
        Debug.Log("Prev");
        TextSpace.text = text[page];
    }

    public void SetButtonActionsITP()
    {

        Button btnExit = ExitButton.GetComponent<Button>();
        btnExit.onClick.AddListener(Exit);
        Button btnNext = NextButton.GetComponent<Button>();
        btnNext.onClick.AddListener(Next);
        Button btnPrev = PrevButton.GetComponent<Button>();
        btnPrev.onClick.AddListener(Prev);
    }

    public void RemoveButtonActionsITP()
    {

        Button btnExit = ExitButton.GetComponent<Button>();
        btnExit.onClick.RemoveListener(Exit);
        Button btnNext = NextButton.GetComponent<Button>();
        btnNext.onClick.RemoveListener(Next);
        Button btnPrev = PrevButton.GetComponent<Button>();
        btnPrev.onClick.RemoveListener(Prev);
    }

}
