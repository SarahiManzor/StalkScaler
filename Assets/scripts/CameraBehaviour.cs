using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	Camera cam;
	private Transform player;

	private float camVerticalExtend;
    GameManager gm;
	// Use this for initialization
	void Awake () {
        Application.targetFrameRate = 60;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		cam = Camera.main;
		camVerticalExtend = cam.orthographicSize * Screen.height/Screen.width;
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(transform.position.x, player.position.y + camVerticalExtend / 3, transform.position.z);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (gm.GetPlaying())
        {
            transform.position = new Vector3(transform.position.x, player.position.y + camVerticalExtend / 3, transform.position.z);
        }
	}
}
