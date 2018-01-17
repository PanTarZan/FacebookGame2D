using System.Collections;
using UnityEngine;

// using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour {
    
    public void JustUseAttack(int x)
    {
        switch (x)
        {
            case 0:
                Attack1();
                break;
            case 1:
                Attack2();
                break;
            case 2:
                Attack3();
                break;
            case 3:
                Attack4();
                break;
        }
    }


    public void Attack1()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        if (enemies.Length == 0)
        {
            return;
        }

        if (GameMasterScript.selectedEnemy == null)
        {
            
            StartCoroutine(GetComponent<UIControls>().statusTextDisplay());
            return;
        }

        var currentPlayer = GameMasterScript.selectedPlayer;
        var classInfo = currentPlayer.GetComponent<PLAYERSTATS>();

        switch (classInfo.playerClass)
        {
            case "Knight":
                        //PlayerAttackAnimation
                           var KnightAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                           KnightAttackAnimation.Play("Attack2H"); 
                           KnightAttackPrimary(classInfo.DMGup);
                break;
            case "Priest":
                        //PlayerAttackAnimation
                        var PriestAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                        PriestAttackAnimation.Play("Attack1H");
                        PriestAttackPrimary(classInfo.DMGup);
                break;
        }
        
        GameMasterScript.actionTaken = true;
    }
    public void Attack2()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        if (enemies.Length == 0)
        {
            return;
        }
      //  var skillButton = gameObject.GetComponent<UIControls>();

        var currentPlayer = GameMasterScript.selectedPlayer;
        var classInfo = currentPlayer.GetComponent<PLAYERSTATS>();

        switch (classInfo.playerClass)
        {
            case "Knight":
    
                if (classInfo.pHealth / classInfo.MaxHPup >= 0.33f)
                {
                    return;
                }

                    if (GameMasterScript.selectedEnemy == null)
                    {
                    StartCoroutine(GetComponent<UIControls>().statusTextDisplay());
                    return;
                    }
                var KnightAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                KnightAttackAnimation.Play("Attack2H");
                KnightAttackSecondary(classInfo.DMGup);
                break;
            case "Priest":
                //PlayerAttackAnimation
                var PriestAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                PriestAttackAnimation.Play("Attack1H");
          //      PriestAttackSecondary(classInfo.DMGup);
                break;
        }

        GameMasterScript.actionTaken = true;
    }
    public void Attack3()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        if (enemies.Length == 0)
        {
            return;
        }

      //  var skillButton = gameObject.GetComponent<UIControls>();
        var currentPlayer = GameMasterScript.selectedPlayer;
        var classInfo = currentPlayer.GetComponent<PLAYERSTATS>();

        switch (classInfo.playerClass)
        {
            case "Knight":
                //PlayerAttackAnimation
                    if (GameMasterScript.selectedEnemy == null)
                    {
                    StartCoroutine(GetComponent<UIControls>().statusTextDisplay());
                    return;
                    }
                  var enemy = GameMasterScript.selectedEnemy.GetComponentInChildren<Enemy>();
                   if (enemy.eHealth / enemy.MaxHealth >= 0.25f)
                   {
                    return;
                   }
                    var KnightAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                KnightAttackAnimation.Play("Attack2H");
                KnightAttackTertiary(classInfo.DMGup);
                break;
            case "Priest":
                //PlayerAttackAnimation
                var PriestAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                PriestAttackAnimation.Play("Attack1H");
             //   PriestAttackTertiary(classInfo.DMGup);
                break;
          
        }

        GameMasterScript.actionTaken = true;
    }
    public void Attack4()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");

        if (enemies.Length == 0)
        {
            return;
        }


        var currentPlayer = GameMasterScript.selectedPlayer;
        var classInfo = currentPlayer.GetComponent<PLAYERSTATS>();

        switch (classInfo.playerClass)
        {
            case "Knight":
                   
                //PlayerAttackAnimation
                var KnightAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
                KnightAttackAnimation.Play("Attack2H");
                KnightAttackQuaternary(classInfo.DMGup);
                GameMasterScript.actionTaken = true;
                break;
            case "Priest":
                //PlayerAttackAnimation
                
          //      StartCoroutine(PriestAttackQuaternary(classInfo.DMGup));
                
                break;
      

        }
        
    }



    public IEnumerator AttackAnimation(float ilosc, GameObject e)
    {
                var enemy = e.GetComponentInChildren<Enemy>();
                var enemyUI = enemy.damageText;
                var enemyUiAnimation = enemyUI.gameObject.GetComponent<Animator>();
        
            // enemy damage text
                    enemyUI.gameObject.SetActive(true);
                    enemyUI.text = ilosc.ToString();
                    enemyUiAnimation.Play(0);
            
            
        
        yield return new WaitForSeconds(1f);
                    if (enemyUiAnimation == null)
                        {
                            yield break ;
                        }

                    enemyUiAnimation.Stop();
                    enemyUI.gameObject.SetActive(false);
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
    } */

    // Knight
    public void KnightAttackPrimary(int attackBase)
    {
         GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
         foreach (var e in enemies)
         {
             if (e.transform == GameMasterScript.selectedEnemy)
             {
                 var enemy = e.GetComponentInChildren<Enemy>();


                 if (enemy.eHealth <= 0)
                 {
                     Destroy(enemy);
                 }
                 else
                 {

                     enemy.eHealth -= attackBase *2;
                     StartCoroutine(AttackAnimation(attackBase*2, e));
                 }
             }
         }

        //"Knight Attacked for: "+attackBase*2);
    }
    public void KnightAttackSecondary(int attackBase)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        var currentPlayer = GameMasterScript.selectedPlayer;
        var classInfo = currentPlayer.GetComponent<PLAYERSTATS>();


        foreach (var e in enemies)
        {
            if (e.transform == GameMasterScript.selectedEnemy)
            {
                var enemy = e.GetComponentInChildren<Enemy>();

                
                        if (enemy.eHealth <= 0)
                        {
                            Destroy(enemy);
                        }
                        else
                        {
                            classInfo.pHealth -= attackBase;
                            enemy.eHealth -= attackBase * 3;
                          //  StartCoroutine(enemy.AttackAnimation(currentPlayer, attackBase));
                            StartCoroutine(AttackAnimation(attackBase * 4,e));
                        }

            }
        }
        

    }
    public void KnightAttackTertiary(int attackBase)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach (var e in enemies)
        {
            if (e.transform == GameMasterScript.selectedEnemy)
            {
                var enemy = e.GetComponentInChildren<Enemy>();


                if (enemy.eHealth <= 0)
                {
                    Destroy(enemy);
                }
                if (enemy.eHealth/enemy.MaxHealth <= 0.25f)
                {
                    enemy.eHealth -= attackBase * 10;
                    StartCoroutine(AttackAnimation(attackBase * 10,e));
                }
            }
        }
        
    }
    public void KnightAttackQuaternary(int attackBase)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach (var e in enemies)
        {
                var enemy = e.GetComponentInChildren<Enemy>();

               
                        enemy.eHealth -= attackBase;
                 
                        StartCoroutine(AttackAnimation(attackBase,e));
            
        }

    }
    // Priest
    public void PriestAttackPrimary(int attackBase)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");


        foreach (var e in enemies)
        {
            if (e.transform == GameMasterScript.selectedEnemy)
            {
                var enemy = e.GetComponentInChildren<Enemy>();

                    enemy.eHealth -= attackBase*1f;
                    StartCoroutine(AttackAnimation(attackBase*1f,e));
              
            }
        }
    }
  /*  public void PriestAttackSecondary(int attackBase)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            var stats = p.GetComponent<PLAYERSTATS>();
            if (stats.pHealth <= stats.MaxHPup)
            stats.pHealth += attackBase;
            StartCoroutine(HealAnimation(attackBase, p));
        }
    }
    public void PriestAttackTertiary(int attackBase)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float mostDamagedPlayer = 1000 ;

        foreach (var p in players)
        {
            var stats = p.GetComponent<PLAYERSTATS>();

            if (stats.pHealth < mostDamagedPlayer)
            {
                mostDamagedPlayer = stats.pHealth;
            }
            
        }
        foreach (var p in players)
        {
            var stats = p.GetComponent<PLAYERSTATS>();
            if (stats.pHealth == mostDamagedPlayer)
            {
                stats.pHealth += attackBase * 4;
                StartCoroutine(HealAnimation(attackBase*4, p ));
            }
        }
    }
    public IEnumerator PriestAttackQuaternary(int attackBase)
    {
        var currentPlayer = GameMasterScript.selectedPlayer;
        var PriestAttackAnimation = currentPlayer.GetComponentInChildren<Animator>();
        var beforeReset = GameMasterScript.selectedPlayer;
        GameMasterScript.selectedPlayer = null;


        while (GameMasterScript.selectedPlayer == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameMasterScript.selectedPlayer = beforeReset;
                yield break;
            }
            yield return null;
        }

        var selectedStats = GameMasterScript.selectedPlayer.GetComponent<PLAYERSTATS>();
        selectedStats.pHealth += attackBase;
        PriestAttackAnimation.Play("Attack1H");
        StartCoroutine(HealAnimation(attackBase * 3, GameMasterScript.selectedPlayer));

        GameMasterScript.actionTaken = true;
        yield return null;
    } */
}
