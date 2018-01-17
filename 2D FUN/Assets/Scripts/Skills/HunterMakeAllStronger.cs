using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMakeAllStronger : MonoBehaviour {

    public void HunterMakeStronger()
    {
        var attackStats = gameObject.GetComponent<AttackStats>();


        var player = GameMasterScript.selectedPlayer;
        if (player == null)
        {
            Debug.Log("No player selected");
            return;
        }

        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (allPlayers == null)
        {
            Debug.Log("No players at all");
            return;
        }
        
        var playerStats = player.GetComponent<PLAYERSTATS>();
        if (playerStats.pMANA < attackStats.skillCost)
        {
            Debug.Log("No mana");
            return;
        }

        foreach (var p in allPlayers)
        {
            p.GetComponent<PLAYERSTATS>().damageBase += 1;
        }
        playerStats.pMANA -= attackStats.skillCost;
        GameMasterScript.actionTaken = true;


    }
}
