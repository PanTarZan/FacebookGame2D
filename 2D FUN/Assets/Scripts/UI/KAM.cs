using UnityEngine;

public class KAM : MonoBehaviour {

    public bool iksde = false;


    public static Vector3 cameraposition;
    public GameObject[] availablePlayers;
    public GameObject followingCam;
    public float camDistance = -10;


    public float OffsetX;
    public float OffsetY;

    Vector3 playerPos;
    Vector3 camPos;
    Vector3 camvelocity = Vector3.zero;

    public  Vector3 camDistanceToPlayer;

    
    // Use this for initialization
    void Start () {
        cameraposition = camPos;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        availablePlayers = players;
        if (availablePlayers.Length > 0)
        {
            cameraFollow(availablePlayers[0]);
        }
        else
        {
            return;
        }
        
        
           
    }

    void cameraFollow(GameObject playerToFollow)
    {
        //odleglosc do gracza
        if (playerToFollow == null)
        {
            return;
        }

        camDistanceToPlayer = gameObject.transform.position - playerPos;
        playerPos = playerToFollow.transform.position;
        camPos.Set(playerPos.x + OffsetX, playerPos.y + OffsetY, camDistance);
        
        followingCam.transform.position = Vector3.SmoothDamp(followingCam.transform.position, camPos, ref camvelocity,0.1f);
        var iksdexx = followingCam.GetComponent<Camera>();
        
  
        iksdexx.orthographicSize = Mathf.MoveTowards(5, 3.5f, 0.1f);
           
       

    }
    
}
