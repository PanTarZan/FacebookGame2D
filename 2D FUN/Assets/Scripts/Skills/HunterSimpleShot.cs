using UnityEngine;

public class HunterSimpleShot : MonoBehaviour {


    public void HunterAttackPrimary()
    {
        var attackStats = gameObject.GetComponent<AttackStats>();

        var player = GameMasterScript.selectedPlayer;

        if(player == null)
        {
            Debug.Log("No player selected");
            return;
        }
        
        var enemy = GameMasterScript.selectedEnemy;

        if(enemy == null)
        {
            Debug.Log("No Enemy selected");
            return;
        }

        if (GameMasterScript.selectedEnemy == null)
        {
            StartCoroutine(GetComponent<UIControls>().statusTextDisplay());
            return;
        }

        var playerStats = player.GetComponent<PLAYERSTATS>();

        if (playerStats.pMANA < attackStats.skillCost)
        {
            Debug.Log("No mana");
            return;
        }

        var enemyStats = enemy.GetComponentInChildren<Enemy>();

        StartCoroutine(enemyStats.GetHit());
        StartCoroutine(enemyStats.GetAttackedFor(attackStats.skillDamage + playerStats.damageBase));
        enemyStats.eHealth -= attackStats.skillDamage+playerStats.damageBase;
        playerStats.pMANA -= attackStats.skillCost;
        GameMasterScript.actionTaken = true;
                
            
        }
    }



