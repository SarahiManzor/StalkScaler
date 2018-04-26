using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardButton : MonoBehaviour {

    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //transform.position = new Vector2(transform.position.x, Camera.main.transform.position.y - 0.5f);
    }

    void OnTouchUp()
    {
        gm.SetHard(true);
        gm.SetPlaying(true);
        this.transform.position = new Vector3(-200f, 0f, 0f);
        GameObject.Find("EasyButton").transform.position = new Vector3(-200f, 0f, 0f);
        //Destroy(GameObject.Find("EasyButton"));
        //Destroy(this.gameObject);
    }
}
