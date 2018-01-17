
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeeed;
    public GameObject player;
    public static bool canPlayerMove = true;
    public bool canMove = true;

    // Use this for initialization
    void Start()
    {
        canPlayerMove = canMove;
    }
	
	// Update is called once per frame
	void Update () {

        if(canPlayerMove)
        movePlayer();
        else
        {
            var playerMovementAnimation = gameObject.GetComponentInChildren<Animator>();
            playerMovementAnimation.Play("Stand");
        }

       

    }

    void movePlayer()
    {
        var playerMovementAnimation = gameObject.GetComponentInChildren<Animator>();
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(moveSpeeed * Time.deltaTime, 0, 0);
            playerMovementAnimation.Play("Walk");

        }
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(-moveSpeeed * Time.deltaTime, 0, 0);
            playerMovementAnimation.Play("Walk");
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerMovementAnimation.Play("Stand");
        }
    }
    


}
