using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour {

    public float smooth = 0.2F;
    public float targetTiltAngle = 30.0F;
    private float tiltAngle;

    private void Start()
    {
        tiltAngle = transform.rotation.eulerAngles.z;
    }
    // Update is called once per frame
    void Update ()
    {
        //Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        tiltAngle += smooth;
        transform.rotation = Quaternion.Euler(0f, 0f, tiltAngle);//Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        //Debug.Log(transform.rotation.eulerAngles.z + "> <" + targetTiltAngle);
        if(tiltAngle >= targetTiltAngle)
        {
            targetTiltAngle = 360f - targetTiltAngle;
            smooth *= -1;
        }
        else if (tiltAngle <= targetTiltAngle - 360f)
        {
            targetTiltAngle = 360f - targetTiltAngle;
            smooth *= -1;
        }
    }
}
