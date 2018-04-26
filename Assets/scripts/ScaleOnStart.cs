using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnStart : MonoBehaviour {
    public float scaleSpeed = 1f;
    private Vector3 targetScale;

    private void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = new Vector3(0f, 0f, transform.localScale.z);
    }

    private void Update()
    {
        float newScaleX = Mathf.Lerp(transform.localScale.x, targetScale.x, scaleSpeed);
        float newScaleY = Mathf.Lerp(transform.localScale.y, targetScale.y, scaleSpeed);
        transform.localScale = new Vector3(newScaleX, newScaleY, transform.localScale.z);
    }
}
