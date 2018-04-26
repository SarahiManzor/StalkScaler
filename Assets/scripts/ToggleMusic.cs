using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour {

    private GameManager gm;
    private AudioSource music;
    public Sprite []sprites;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!gm.GetMusic())
        {
            transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
	}

    void OnTouchUp()
    {
        if (gm.GetMusic())
        {
            gm.transform.GetComponent<AudioSource>().volume = 0f;
            gm.SetMusic(false);
            transform.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else
        {
            gm.transform.GetComponent<AudioSource>().volume = 1f;
            gm.SetMusic(true);
            transform.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        gm.Save();
    }
}
