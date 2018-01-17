using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour {

    [Header("Main stuff")] 
    public GameObject UIMain;
    public GameObject GameOverUI;
    public GameObject LevelWonUI;
    public GameObject skillPanel;
    public GameObject statsPanel;
    public GameObject inventoryPanel;
    public GameObject systemText;
    public GameObject SkillChoosePanel;

    [Header("UI on Hover Info")]
    public GameObject UIInv;
    public GameObject UiDesc;
    public Text UiDescTitle;
    public Text UiDescDescription;

    [Header("Quest Panel stuff")]
    public GameObject questIcon;
    public GameObject questTitle;
    public GameObject questDescription;
    [Header("Turn Manager stuff")]
    public bool EnableClickingSkills;

    public Button[] skillButtons;
    private Text[] statsFields;
    GameObject[] allPlayers;


    // Use this for initialization
    void Start () {
        statsFields = statsPanel.GetComponentsInChildren<Text>();
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        
    }
	
	// Update is called once per frame
	void Update () {
        
     //   SkillButtonsChange();
        UIStatsChange();
        SelectCurrentPlayer();
        
        questDescription.GetComponentInChildren<Text>().text = "Kill all enemies: " + GameMasterScript.enemiesCount;
    }

    public void GameOver()
    {
        UIMain.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void LevelWon()
    {
        UIMain.SetActive(false);
        LevelWonUI.SetActive(true);
    }

    public void SelectCurrentPlayer()
    {

        if (GameMasterScript.selectedPlayer == null)
        {
            foreach (var p in allPlayers)
            {
                var player = p.GetComponent<PLAYERSTATS>();
                player.SelectCanvas.SetActive(false);
            }
        }
        else
        {
            foreach (var p in allPlayers)
            {
                if (p == null)
                {
                    return;
                }
                if (GameMasterScript.selectedPlayer == p)
                {
                    var player = p.GetComponent<PLAYERSTATS>();
                    player.SelectCanvas.SetActive(true);
                }
                else
                {
                    var player = p.GetComponent<PLAYERSTATS>();
                    player.SelectCanvas.SetActive(false);
                }
            }
        }

    }

 

    void UIStatsChange()
    {
        foreach (var p in allPlayers)
        {
            if (p == GameMasterScript.selectedPlayer)
            {
                var stats = p.GetComponent<PLAYERSTATS>();
           
                    statsFields[6].text = stats.playerClass;
                    statsFields[7].text = stats.pHealth + " / " +stats.MaxHPup;
                    statsFields[8].text = stats.pMANA + " / " + stats.MaxMANAup;
                    statsFields[9].text = stats.DMGup.ToString();
                    statsFields[10].text = stats.CRITup.ToString();
                    statsFields[11].text = stats.DODGEup.ToString();
            }
        }
    }

    public IEnumerator statusTextDisplay()
    {
        
        systemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        systemText.SetActive(false);



        yield return null;
    }

    
}
