using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
    public float moveSpeeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

    void Update()
    {
        if (PlayerMovement.canPlayerMove)
            ParallaxEffectMovement();

    }

    public void ParallaxEffectMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(-moveSpeeed * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(+moveSpeeed * Time.deltaTime, 0, 0);
        }
     
    }
}
