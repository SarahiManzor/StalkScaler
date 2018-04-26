using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwipe : MonoBehaviour {

    public float swipeRange = 2.5f;
    private GameObject player;
    private Camera cam;
    private float spriteWidth;
    private float speed = 0.1f;
    private Animator anim;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Bounds spriteBounds = sr.sprite.bounds;
        spriteWidth = spriteBounds.max.x - spriteBounds.min.x;

        //transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 5f);

        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("touchDown");
    }
    
	void FixedUpdate () {
        if(transform.position.x + spriteWidth > swipeRange || transform.position.x - spriteWidth < -swipeRange)
        {
            speed *= -1;
            anim.SetTrigger("touchUp");
            //anim.SetTrigger("touchDown");
        }
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
	}
}
