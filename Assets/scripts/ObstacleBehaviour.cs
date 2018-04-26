using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour {

    PlayerBehaviour playerScript;
	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-3.5f);
        float randomSpeed = Random.Range(50f,100f);
        int randomDirection = Random.Range(-1, 1);
        if (randomDirection == -1)
        {
            randomSpeed *= -1f;
        }
        transform.GetComponent<Rigidbody2D>().angularVelocity = randomSpeed;;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTouchDown(float[] input)
    {
        playerScript.OnTouchDown(input);
    }

    void OnTouchUp(float[] input)
    {
        playerScript.OnTouchUp(input);
    }

    void OnTouchStay(float[] input)
    {
        playerScript.OnTouchStay(input);
    }
}
