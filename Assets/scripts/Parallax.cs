using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{

    public Transform[] backgrounds;             // Array of all the backgrounds to be parallaxed.
    public int[] backgroundScales;
    
    private Camera cam;

    private float[] startY;
    private float camStart;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        startY = new float[backgrounds.Length];
        camStart = cam.transform.position.y;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            startY[i] = backgrounds[i].position.y + camStart / 10f;
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float newY = startY[i] + ((cam.transform.position.y - 0.5f) - camStart) / 10f * backgroundScales[i];
            backgrounds[i].position = new Vector3(backgrounds[i].position.x, newY, backgrounds[i].position.z);
        }
    }
}
