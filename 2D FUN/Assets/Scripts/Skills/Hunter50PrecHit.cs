using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter50PrecHit : MonoBehaviour {

    public void Hunter50PrecShot()
    {
        var attackStats = gameObject.GetComponent<AttackStats>();

        var player = GameMasterScript.selectedPlayer;

        if (player == null)
        {
            Debug.Log("No player selected");
            return;
        }

        var enemy = GameMasterScript.selectedEnemy;

        if (enemy == null)
        {
            Debug.Log("No Enemy selected");
            return;
        }


        var playerStats = player.GetComponent<PLAYERSTATS>();

        if (playerStats.pMANA < attackStats.skillCost)
        {
            Debug.Log("No mana");
            return;
        }

        if (GameMasterScript.selectedEnemy == null)
        {
            StartCoroutine(GetComponent<UIControls>().statusTextDisplay());
            return;
        }

        var enemyStats = enemy.GetComponentInChildren<Enemy>();

        int mayCrit = Random.Range(0, 100);

        if (mayCrit >= 50)
        {
        StartCoroutine(enemyStats.GetHit());
        StartCoroutine(enemyStats.GetAttackedFor(attackStats.skillDamage + playerStats.damageBase));
        enemyStats.eHealth -= attackStats.skillDamage + playerStats.damageBase;
        playerStats.pMANA -= attackStats.skillCost;
        GameMasterScript.actionTaken = true;
        }

        if (mayCrit < 50)
        {
            StartCoroutine(enemyStats.GetHit());
            StartCoroutine(enemyStats.GetAttackedFor(playerStats.damageBase));
            enemyStats.eHealth -= playerStats.damageBase;
            playerStats.pMANA -= attackStats.skillCost;
            GameMasterScript.actionTaken = true;
        }


    }
}
