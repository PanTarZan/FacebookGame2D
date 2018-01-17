using System.Collections;
using UnityEngine;


public class GameMasterScript : MonoBehaviour {

    public static Transform selectedEnemy = null;
    public static GameObject selectedPlayer = null;
    public static GameObject selectedItem = null;
    public  GameObject[] playersToSpawn;
    public Vector3[] playerSpawnLocations;

    public static bool playersTURN = false;
    public static bool actionTaken = false;
    public static bool IsBattleOver = true;
    public static GameObject currentBattleposition;
    public bool battleover;

    int EnemyTurnNumber;
    int PlayerTurnNumber;
    public static int enemiesCount;
    

    // Use this for initialization
    void Awake()
    {
      //  SpawnPlayers();

    }

    void Start () {


        var player1 = GameObject.FindGameObjectWithTag("Player");
        selectedPlayer = player1;
        Vector3 dir =  playerSpawnLocations[0] - selectedPlayer.transform.position;
        selectedPlayer.transform.Translate(dir);

        var battlePositions = GameObject.FindGameObjectsWithTag("TRYGERY");
        foreach (var b in battlePositions)
        {
            var battlepositionsevents = b.GetComponent<BattlePositionsEvents>();
            enemiesCount += battlepositionsevents.enemy.Length;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        var battlePositions = GameObject.FindGameObjectsWithTag("TRYGERY");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        battleover = IsBattleOver;
        var eventPositions = GameObject.FindGameObjectsWithTag("EventPosition");


        if (players.Length > 0)
            {
                    foreach (var battleposition in battlePositions)
                    {
                        BattlePositionsEvents battlepositionsevents = battleposition.GetComponent<BattlePositionsEvents>();
                        if (Mathf.Abs(players[0].transform.position.x - battleposition.transform.position.x) <= 12)
                        {

                            if (battlepositionsevents.triggered == false)
                            {
                                if (IsBattleOver == true)
                                {

                                    StartBattle(battleposition.transform.position, battleposition);

                                }
                             return;
                            }
                        }
                   }

                    foreach (var eventPosition in eventPositions)
                    {
                        var pveEvent = eventPosition.GetComponent<EventSpotActions>();
                        if (Mathf.Abs(players[0].transform.position.x - eventPosition.transform.position.x) <= 1)
                        {

                            if (pveEvent.triggered == false)
                            {
                                if (pveEvent.spawnPVECharacter)
                                {
                            pveEvent.SpawnPVECharacter();
                                }

                                if (pveEvent.textAndActionEvent)
                                {
                            pveEvent.TextActionEvent();
                                }
                                return;
                            }
                        }
                    }

            if (IsBattleOver == false && enemies.Length == 0)
            {
                EndBattle();
            }
        } else
        {
            UIControls UI = FindObjectOfType<UIControls>();
            UI.GameOver();
        }

        if (enemiesCount <= 0)
        {
            StartCoroutine("LevelWon");
        }
    }
  

    void SpawnEnemy(Vector3 punktx, GameObject battleposition)
    {

        var battlepositionsevents = battleposition.GetComponent<BattlePositionsEvents>();
        punktx.x -= 3;
        punktx.z = -1;
        punktx.y = -0.2f;
        foreach (var e in battlepositionsevents.enemy)
        {
            if (e.name != "GurenYello")
            {

             Instantiate(e,punktx, Quaternion.identity);
             punktx.x += 3;
            }
            else
            {
                punktx.x += 3; 
                punktx.y = -0.25f;
                Instantiate(e, punktx, Quaternion.identity);
            }
        }
        
    }

    void StartBattle(Vector3 enemyposition,GameObject battleposition)
    {
        currentBattleposition = battleposition;

        PlayerTurnNumber = 1;
        EnemyTurnNumber = 1;
        
        IsBattleOver = false;
        PlayerMovement.canPlayerMove = false;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            var playerAttackAnimation = p.GetComponentInChildren<Animator>();
            playerAttackAnimation.Play("Stand");
        }


        var battlepositionsevents = battleposition.GetComponent<BattlePositionsEvents>();

        battlepositionsevents.triggered = true;

        SpawnEnemy(enemyposition, battleposition);
      
            fight();
    }

    void fight()
    {
        
        int i=1;
        i = Random.Range(0, 2);
        if (i>=1)
        {
            StartCoroutine("EnemyTurn");
        }
        else
        {
           StartCoroutine("PlayerTurn");
        }
        
    }

    public IEnumerator PlayerTurn()
    {
        playersTURN = true;
        selectedEnemy = GameObject.FindGameObjectWithTag("ENEMY").transform;
        actionTaken = false;
        //poczatek tury
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                
        //WARUNKI
        
        foreach (var p in players)
            {
            if (p != null)
            {
            
                if (IsBattleOver == false)
                {
                    selectedPlayer = p;
                    while (actionTaken == false)
                    {
                        if (p == null)
                        {
                            break;
                        }

                        if (IsBattleOver)
                        {
                            break;
                        }

                        yield return null;
                    }
                }
                    selectedEnemy = null;
            }
        }
        
        //koniec tury

        if (IsBattleOver == false)
        {
        PlayerTurnNumber++;
        StartCoroutine("EnemyTurn");
        }
        yield return null;
        
    }

    public IEnumerator EnemyTurn()
    {
        
        playersTURN = false;

        
        yield return new WaitForSeconds(0.5f);

        //Srodek Tury
        GameObject anyEnemy = GameObject.FindGameObjectWithTag("ENEMY");
        if (anyEnemy == null)
        {
            yield break;
        }
        selectedEnemy = anyEnemy.transform;
        //all enemies attack in enemy turn

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach(var e in enemies)
            {
            if (e)
            {
            Enemy currentEnemy =  e.GetComponentInChildren<Enemy>();

                if (currentEnemy.isActiveAndEnabled)
                {
                    currentEnemy.Attack();
                    yield return new WaitForSeconds(0.7f);
                }
            }
        
        }
        yield return new WaitForSeconds(0.5f);


        //Zakonczenie tury
        if (IsBattleOver == false)
        {

        EnemyTurnNumber++;
        StartCoroutine("PlayerTurn");
        }

        yield return null;
        
    }
    
    public void EndBattle()
    {

        IsBattleOver = true;
        TurnOffUnusedUI();
        playersTURN = false;
        
        Getreward();
        GetExp();
        
    }
    public void TurnOffUnusedUI()
    {
       
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in allPlayers)
        {
            var playerstats = p.GetComponent<PLAYERSTATS>();
            playerstats.statusText.gameObject.SetActive(false);
        }

    }

    public void SpawnPlayers()
    {
      int i = 0;
        foreach (GameObject p in playersToSpawn)
        {
            Instantiate(p, playerSpawnLocations[i], new Quaternion());
            i++;
        }
    }
    public IEnumerator LevelWon()
    {
        yield return new WaitForSeconds(1f);
        UIControls UI = FindObjectOfType<UIControls>();
        UI.LevelWon();
        yield return null;
    }
    public void Getreward()
    {
        currentBattleposition.GetComponent<BattlePositionsEvents>().AfterFight();
    }
    public void GetExp()
    {
        var player = selectedPlayer.GetComponent<PLAYERSTATS>();
        var exp = currentBattleposition.GetComponent<BattlePositionsEvents>().expGained;
        player.Experience += exp;
        StartCoroutine(player.GetStatus(Color.grey, exp.ToString()+ " EXP"));
        Debug.Log("exp Animation");

    }
}
