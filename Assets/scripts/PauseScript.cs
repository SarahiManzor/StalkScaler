using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    GameManager gm;
    private bool gameStarted = false;
	// Use this for initialization
	void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void InitPosition ()
    {
        Camera cam = Camera.main;
        float camWidth = cam.orthographicSize * Screen.width / Screen.height;
        float camHeight = cam.orthographicSize;
        transform.position = new Vector3(cam.transform.position.x + camWidth - 0f, cam.transform.position.y + camHeight - 0.25f, transform.position.z);
        gm.InitPosition();
    }

    // Update is called once per frame
    void Update () {
        if (gm.GetPlaying() && !gameStarted)
        {
            gameStarted = true;
            InitPosition();
        }
        if (!gm.GetPlaying() && gameStarted)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTouchDown()
    {
        //Debug.Log("yep");
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Camera cam = Camera.main;
            transform.GetChild(0).transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
        }
        else
        {
            Time.timeScale = 1;
            Camera cam = Camera.main;
            transform.GetChild(0).transform.position = new Vector3(-200f, 0f, transform.position.z);
        }
    }
}
