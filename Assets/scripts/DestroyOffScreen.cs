using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camPos = Camera.main.transform.position;
        if (transform.position.x > camPos.x + 5f || transform.position.y < camPos.y - 10f)
        {
            Destroy(this.gameObject);
        }
	}
}
