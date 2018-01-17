
using UnityEngine;

public class DisplayHealth : MonoBehaviour {
    public GameObject HealthBar;
    public float playerhealthvariable;
    public GameObject manaBar;
    public float playermanavariable;

    public float enemyhealth;
    
	void Update () {
        if (gameObject.GetComponent<Enemy>() != null)
        {
            var currentEnemy = gameObject.GetComponentInChildren<Enemy>();
            enemyhealth = currentEnemy.eHealth / currentEnemy.MaxHealth;

            HealthBar.transform.localScale = new Vector3(enemyhealth, 1, 1);
        }
        else
        {
            var playerStats = gameObject.GetComponent<PLAYERSTATS>();
            playerhealthvariable = playerStats.pHealth / playerStats.MaxHPup;
            playermanavariable = playerStats.pMANA / playerStats.MaxMANAup;

            if (playerhealthvariable > 1)
            {
                return;
            }
            else
            {
            HealthBar.transform.localScale = new Vector3(playerhealthvariable ,1,1);
            }



            if (playermanavariable > 1)
            {
                return;
            }
            else
            {

            manaBar.transform.localScale = new Vector3(playermanavariable, 1, 1);
            
            }
        }

	}
}
