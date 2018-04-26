using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScript : MonoBehaviour {
    
    void OnTouchUp()
    {
        transform.parent.transform.position = new Vector2(-200f, -200f);
        Time.timeScale = 1;
    }
}
