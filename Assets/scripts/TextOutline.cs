using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOutline : MonoBehaviour {

    public float offSet = 0.03f;

	// Use this for initialization
	void Start () {
        GameObject[] textObjects = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            textObjects[i] = new GameObject(gameObject.name + "Outline" + i);
            textObjects[i].transform.position = new Vector2(transform.position.x + i, transform.position.y + i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
