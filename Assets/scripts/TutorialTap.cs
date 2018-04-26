using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTap : MonoBehaviour
{

    private GameObject player;
    private Camera cam;
    private float spriteWidth;
    public int animDelay = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Bounds spriteBounds = sr.sprite.bounds;
        spriteWidth = spriteBounds.max.x - spriteBounds.min.x;
        float newX = player.transform.position.x;
        if (sr.flipX)
        {
            newX += spriteWidth;
        }
        else
        {
            newX -= spriteWidth;
        }

        //transform.position = new Vector2(newX, player.transform.position.y);
    }
    
    void FixedUpdate()
    {
        animDelay--;
        if(animDelay == -1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("startTap");
        }

    }
}
