using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAllhitShot : MonoBehaviour {

    // Use this for initialization
    public void HunterHitAll()
    {
        var attackStats = gameObject.GetComponent<AttackStats>();

        var player = GameMasterScript.selectedPlayer;

        if (player == null)
        {
            Debug.Log("No player selected");
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

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        foreach (var e in enemies)
        {
            var enemy = e.GetComponentInChildren<Enemy>();

            enemy.eHealth -= attackStats.skillDamage + playerStats.damageBase;
            StartCoroutine(enemy.GetHit());
            StartCoroutine(enemy.GetAttackedFor(attackStats.skillDamage + playerStats.damageBase));

        }

        
        playerStats.pMANA -= attackStats.skillCost;
        GameMasterScript.actionTaken = true;


    }
}
