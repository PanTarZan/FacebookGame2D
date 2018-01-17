
using UnityEngine;
using UnityEngine.UI;

public class BattlePositionsEvents : MonoBehaviour {

    public bool triggered = false;
    public GameObject[] enemy;

    public GameObject reward;

    [Header("Good Answer Data")]
    public Button ExitButton;
    public GameObject AfterFightPanel;
    public Text AFTextSpace;
    public string AFText;
    public GameObject rewardSlot;
    public float expGained;
    
    public bool itemWasCreated = false;

    public void AfterFight()
    {
        AfterFightPanel.SetActive(true);
        waitforexit();
        AFTextSpace.text = AFText;
        if (itemWasCreated == false)
        {

            CreateItemToBeTaken();

            itemWasCreated = true;
        }
    }

    public void CreateItemToBeTaken()
    {
        Instantiate(reward, rewardSlot.transform).transform.SetParent(rewardSlot.transform);
    }
    public void waitforexit()
    {
    Button btnGood = ExitButton.GetComponent<Button>();
    btnGood.onClick.AddListener(Exit);
    }
    public void endwaiting()
    {

    Button btnGood = ExitButton.GetComponent<Button>();
    btnGood.onClick.RemoveListener(Exit);
    }
    public void Exit()
    {
        if (rewardSlot.transform.childCount >= 1)
        {
            Destroy(rewardSlot.transform.GetChild(0).gameObject);
        }
            AfterFightPanel.SetActive(false);
        PlayerMovement.canPlayerMove = true;
    }
}
