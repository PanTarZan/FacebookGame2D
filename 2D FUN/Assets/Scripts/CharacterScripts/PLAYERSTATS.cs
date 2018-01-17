using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PLAYERSTATS : MonoBehaviour {

    
    public GameObject[] invSlot;
    public float PrevMaxHP;
    public float PrevMaxMANA;
    public GameObject[] skillSlots;

    [Header("Unity UI Stuff")]
    public Text statusText;
    public GameObject SelectCanvas;
    public GameObject[] Inventories;
    public GameObject[] Skillsies;
    public GameObject HitEffect;
    public Transform spawnEffect;


    [Header("Player Initial Stats Stuff")]
    public bool statsIncreased = false;
    public float Experience;
    public string playerClass;
    public float pHealth = 100;
    public float pMANA = 20;
    public int damageBase;
    public int CritPrecent;
    public int DodgePrecent;

    [Header("Player Upgraded Stats Stuff")]
    public float MaxMANAup;
    public float MaxHPup;
    public int DMGup;
    public int CRITup;
    public int DODGEup;
    




    // Use this for initialization
    void Start () {
        PrevMaxHP = pHealth;
        PrevMaxMANA = pMANA;
        GetInventorySlots();
        GetSkillSlots();
	}
	
	// Update is called once per frame
	void Update () {
        if (pHealth <=0)
        {
            Destroy(gameObject);
        }
        CheckEquippedItemsAndUI();
        CheckEquippedSkillsAndUI();
        InventoryStatsCalculate();
        
    }

    void OnMouseDown()
    {
        GameMasterScript.selectedPlayer = gameObject;
    }
    void GetSkillSlots()
    {
        var slots = FindObjectsOfType<Slot>();

        int i = 0;
        foreach (var s in slots)
        {
            if (s.ToString() == playerClass + " (Slot)" && s.isSkillSlot)
            {
                skillSlots[i] = s.transform.gameObject;
                i++;

            }
        }
    }
    void GetInventorySlots()
    {
        var slots = FindObjectsOfType<Slot>();

        int i = 0;
        foreach (var s in slots)
        {
            if (s.ToString() == playerClass + " (Slot)" && s.isPlayerSlot)
            {
                invSlot[i] = s.transform.gameObject;
                i++;

            }
        }
    }
    void CheckEquippedItemsAndUI()
    {
        int x = 0;
        foreach (var i in invSlot)
        {
            if (gameObject == GameMasterScript.selectedPlayer)
            {
                i.SetActive(true);
            }
            else
            {
                i.SetActive(false);
            }

            if (x == 2)
            {
                x = 0;
            }
            if (i.transform.childCount == 0)
            {
                Inventories[x] = null;
                x++;
            }

            if (i.transform.childCount > 0)
            {
                Inventories[x] = i.transform.GetChild(0).gameObject;
                x++;
            }

        }
    }
    void CheckEquippedSkillsAndUI()
    {
        int x = 0;
        foreach (var i in skillSlots)
        {
            if (gameObject == GameMasterScript.selectedPlayer)
            {
                i.SetActive(true);
            }
            else
            {
                i.SetActive(false);
            }

            if (x == 4)
            {
                x = 0;
            }
            if (i.transform.childCount == 0)
            {
                Skillsies[x] = null;
                x++;
            }

            if (i.transform.childCount > 0)
            {
                Skillsies[x] = i.transform.GetChild(0).gameObject;
                x++;
            }

        }
    }
    void InventoryStatsCalculate()
    {
        if (Inventories[0] == null && Inventories[1] == null)
        {
            statsIncreased = false;
            PlayerUpgradedStats();
            return;
        }
        if (Inventories[0] != null && Inventories[1] != null)
        {
            
            statsIncreased = true;
            PlayerUpgradedStats(Inventories[0], Inventories[1]);
            return;
        }

        if (Inventories[0] != null)
        {
            statsIncreased = true;
            PlayerUpgradedStats(Inventories[0]);
        }
        if (Inventories[1] != null)
        {
            statsIncreased = true;
            PlayerUpgradedStats(Inventories[1]);
        }
    }



    void PlayerUpgradedStats(GameObject item1, GameObject item2)
    {
         var i1 = item1.GetComponent<EqStats>();
         var i2 = item2.GetComponent<EqStats>();
        MaxMANAup = PrevMaxMANA;
                    MaxHPup = PrevMaxHP       + i1.addPlayerHealth    + i2.addPlayerHealth;
                    DMGup   = damageBase    + i1.addDamageBase      + i2.addDamageBase;
                    CRITup  = CritPrecent   + i1.addCritPrecent     + i2.addCritPrecent;
                    DODGEup = DodgePrecent  + i1.addDodgePrecent    + i2.addDodgePrecent;
    }
    void PlayerUpgradedStats(GameObject item1)
    {
        var i1 = item1.GetComponent<EqStats>();

        MaxMANAup = PrevMaxMANA;
        MaxHPup = PrevMaxHP + i1.addPlayerHealth;
        DMGup = damageBase + i1.addDamageBase;
        CRITup = CritPrecent + i1.addCritPrecent;
        DODGEup = DodgePrecent + i1.addDodgePrecent;
    }
    void PlayerUpgradedStats()
    {
        MaxMANAup = PrevMaxMANA;
        MaxHPup = PrevMaxHP;
            DMGup = damageBase;
            CRITup = CritPrecent;
            DODGEup = DodgePrecent;
       
    }

    public void UseConsumable(GameObject item)
    {
        if (pHealth >= MaxHPup)
        {
            return;
        }

        pHealth += item.GetComponent<EqStats>().restoreHpAmount;

        if (pHealth >= MaxHPup)
        {
            pHealth = MaxHPup;
        }
        Destroy(item);
    }
    public IEnumerator GetHit()
    {
        var iks = Instantiate(HitEffect, spawnEffect, false);
        yield return new WaitForSeconds(1);
        Destroy(iks);
        yield return null;
    }

    public IEnumerator GetStatus(Color color ,string statusAmount)
    {

        GameObject statTextGO = statusText.gameObject;
        statTextGO.SetActive(true);
        statusText.text = statusAmount;

        statusText.color = color;

        var playeruiAnimation = statTextGO.GetComponent<Animator>();
        playeruiAnimation.Play(0);

        yield return new WaitForSeconds(0.5f);

        if (playeruiAnimation == null)
        {
            yield return null;
        }
        else
        {

            playeruiAnimation.Stop();
            statTextGO.SetActive(false);
        }
        yield return new WaitForSeconds(0.25f);
    }
   
}
