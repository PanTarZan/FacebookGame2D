using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {


    
    public float MaxHealth;
    public float eHealth = 100;


    public Color color;
    public int damageAmount;
    public GameObject enemy;
    public int AttackMin;
    public int AttackMax;

    public Text damageText;

    public string EnemyTitle;
    public string EnemyDescription;
    
    bool dyingStarted = false;
    public GameObject HitEffect;

    public UIControls uiControls;

    // Use this for initialization
    void Start () {
        color = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        var GAMEMASTER = GameObject.Find("GAME MASTER");
        uiControls = GAMEMASTER.GetComponent<UIControls>();
        MaxHealth = eHealth;

    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform == GameMasterScript.selectedEnemy)
        {
            var selectedEnemy = gameObject.GetComponentInChildren<SpriteRenderer>();
            selectedEnemy.color = Color.red;
        }
        else
        {
            var selectedEnemy = gameObject.GetComponentInChildren<SpriteRenderer>();
            selectedEnemy.color = color;
        }

        if (eHealth <= 0 && dyingStarted == false)
        {
            dyingStarted = true;
            StartCoroutine("Die");
        }


    }

    public void Attack()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        var onePlayer = UnityEngine.Random.Range(0, players.Length);

        
        

            var player = players[onePlayer].GetComponent<PLAYERSTATS>();

                if (player.pHealth<=0)
                {
                    return;
                }
                else
                {
                        

                damageAmount = UnityEngine.Random.Range(AttackMin, AttackMax);
                player.pHealth -= damageAmount;
                gameObject.GetComponentInChildren<Animator>().Play("Attack");
                StartCoroutine(player.GetHit());
                StartCoroutine(player.GetStatus(Color.red ,damageAmount.ToString()));
                }
        
    }

   

    void OnMouseDown()
    {
        if (GameMasterScript.playersTURN)
        {
        GameMasterScript.selectedEnemy = gameObject.transform;
        }
    }


    public void OnMouseEnter()
    {

        uiControls.UiDesc.SetActive(true);
        uiControls.UIInv.SetActive(false);


        uiControls.UiDescTitle.text = EnemyTitle;
        uiControls.UiDescDescription.text = EnemyDescription;

    }
    public void OnMouseExit()
    {
        uiControls.UiDesc.SetActive(false);
        uiControls.UIInv.SetActive(true);
    }
    public IEnumerator Die()
    {
        eHealth = 0;
        yield return new WaitForSeconds(0.5f);

        GameMasterScript.enemiesCount -= 1;
        Destroy(gameObject);


        yield return null;
    }

    public IEnumerator GetHit()
    {
        var iks = Instantiate(HitEffect, transform, false);
        yield return new WaitForSeconds(1);
        Destroy(iks);
        yield return null;
    }

    public IEnumerator GetAttackedFor(float ilosc)
    {
       
        var enemyUiAnimation = damageText.GetComponent<Animator>();

        // enemy damage text
        damageText.gameObject.SetActive(true);
        damageText.text = ilosc.ToString();
        enemyUiAnimation.Play(0);



        yield return new WaitForSeconds(1f);
        if (enemyUiAnimation == null)
        {
            yield break;
        }

        enemyUiAnimation.Stop();
        damageText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);

        
    }
  /*  public IEnumerator HealAnimation(float ilosc, GameObject p)
    {
        var currentPlayer = p.GetComponentInChildren<PLAYERSTATS>();

        var playerUI = currentPlayer.healText;
        playerUI.gameObject.SetActive(true);
        playerUI.text = ilosc.ToString();

        var playerUiAnimation = playerUI.gameObject.GetComponent<Animator>();
        playerUiAnimation.Play(0);
        yield return new WaitForSeconds(1f);

        if (playerUiAnimation == null)
        {
            yield break;
        }
        playerUiAnimation.Stop();
        playerUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
    }
    */

}
