using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVEAction : MonoBehaviour {

    [Header("Question Panel Data")]
    public GameObject QuestionPanel;
    public Text QuestionTextSpace;
    public string QuestionText;

    public Button AnswerGoodButton;
    public Button AnswerBadButton;

    [Header("Bad Answer Data")]
    public Button BAExitButton;
    public GameObject BadAnswer;
    public Text BadAnswerTextSpace;
    public string BadAnswerText;

    [Header("Good Answer Data")]
    public Button GAExitButton;
    public GameObject GoodAnswer;
    public Text GoodAnswerTextSpace;
    public string GoodAnswerText;

    [Header(" After Decision Data")]
    public Button ADExitButton;
    public GameObject AfterDecision;
    public Text AfterDecisionTextSpace;
    public string AfterDecisionTextGood;
    public string AfterDecisionTextBad;

    [Header("Misc PVE Parameters")]

    public bool decisionMade = false;
    public bool answeredGood = false;
    public bool answeredBad = false;
    public bool itemWasCreated = false;

    public GameObject rewardItem;
    public GameObject rewardSlot;

    public int counter = 0;


	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        SetButtonActionsITP();


        PlayerMovement.canPlayerMove = false;
        if (decisionMade)
        {
            AfterDecision.SetActive(true);
            if (answeredBad)
            {
                AfterDecisionTextSpace.text = AfterDecisionTextBad;
            }
            if (answeredGood)
            {
                AfterDecisionTextSpace.text = AfterDecisionTextGood;
            }
        }
        QuestionPanel.SetActive(true);
        QuestionTextSpace.text = QuestionText;
        
    }
    public void AnswerGood()
    {
        answeredGood = true;
        QuestionPanel.SetActive(false);
        GoodAnswer.SetActive(true);
        GoodAnswerTextSpace.text = GoodAnswerText;
            if (itemWasCreated == false)
            {
                CreateItemToBeTaken();
     
                itemWasCreated = true;  
            }
    }
    public void AnswerBAD()
    {
        answeredBad = true;
        QuestionPanel.SetActive(false);
        BadAnswer.SetActive(true);
        BadAnswerTextSpace.text = BadAnswerText;
    }

    public void CloseCanvas()
    {
 
        RemoveButtonActionsITP();
        decisionMade = true;
        GoodAnswer.SetActive(false);
        BadAnswer.SetActive(false);
        QuestionPanel.SetActive(false);
        AfterDecision.SetActive(false);
        PlayerMovement.canPlayerMove = true;
    }

    public void CreateItemToBeTaken()
    {
        Instantiate(rewardItem, rewardSlot.transform).transform.SetParent(rewardSlot.transform);
    }

    public void SetButtonActionsITP()
    {
 
        Button btnGood = AnswerGoodButton.GetComponent<Button>();
        btnGood.onClick.AddListener(AnswerGood);
        Button btnBad = AnswerBadButton.GetComponent<Button>();
        btnBad.onClick.AddListener(AnswerBAD);
        Button btnExitGA = GAExitButton.GetComponent<Button>();
        btnExitGA.onClick.AddListener(CloseCanvas);
        Button btnExitBA = BAExitButton.GetComponent<Button>();
        btnExitBA.onClick.AddListener(CloseCanvas);
        Button btnExitAD = ADExitButton.GetComponent<Button>();
        btnExitAD.onClick.AddListener(CloseCanvas);
    }

    public void RemoveButtonActionsITP()
    {
       
        Button btnGood = AnswerGoodButton.GetComponent<Button>();
        btnGood.onClick.RemoveListener(AnswerGood);
        Button btnBad = AnswerBadButton.GetComponent<Button>();
        btnBad.onClick.RemoveListener(AnswerBAD);
        Button btnExitGA = GAExitButton.GetComponent<Button>();
        btnExitGA.onClick.RemoveListener(CloseCanvas);
        Button btnExitBA = BAExitButton.GetComponent<Button>();
        btnExitBA.onClick.RemoveListener(CloseCanvas);
        Button btnExitAD = ADExitButton.GetComponent<Button>();
        btnExitAD.onClick.RemoveListener(CloseCanvas);
    }
}
