﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class TilingVertical : MonoBehaviour {

    public bool hasTopBuddy = false;
    public bool hasBottomBuddy = false;

    public bool reverseScale = false;

    private float spriteHeight = 0f;
    private Camera cam;
    private Transform myTransform;
    private float camVerticalExtend;

    void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () 
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteHeight = sRenderer.sprite.bounds.size.y * Mathf.Abs(transform.localScale.y);
        camVerticalExtend = cam.orthographicSize * Screen.height/Screen.width;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(!hasTopBuddy)
        {
            float edgeVisiblePositionTop = (myTransform.position.y + spriteHeight/2) - camVerticalExtend;

            if(cam.transform.position.y >= edgeVisiblePositionTop)
            {
                makeNewBuddy();
                hasTopBuddy = true;
            }
        }
        else
        {
            float topEdge = myTransform.position.y + spriteHeight/1.5f;
            //Leftmost side of camera
            float camBottom = cam.transform.position.y - camVerticalExtend;

            if(camBottom > topEdge){
                Destroy (gameObject);
            }
        }
    }

    void makeNewBuddy ()
    {
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + spriteHeight, myTransform.position.z);
        Transform newBuddy = (Transform) Instantiate (myTransform, newPosition, myTransform.rotation);

        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3 (newBuddy.localScale.x, newBuddy.localScale.y*-1, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
    }
}
