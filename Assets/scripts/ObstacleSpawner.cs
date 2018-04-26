using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject obstacle1;

    private float[] ropePositions = new float[3];

    private float frequency = 0.8f;
    private float frequencyFactor = 0.925f;
    private int diffCount = 0;
    private int spawnCount = 0;
    private int lastSpawn = -1;
    private float lastSpawnPos = -1f;

    private Camera cam;
    private GameObject parent;
    private GameManager gm;

	// Use this for initialization
	void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = Camera.main;
        ropePositions[0] = GameObject.Find("rope1").transform.position.x;
        ropePositions[1] = GameObject.Find("rope2").transform.position.x;
        ropePositions[2] = GameObject.Find("rope3").transform.position.x;
        parent = this.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (gm.GetPlaying())
        {
            diffCount += 1;
            spawnCount += 1;
            if (diffCount > 250)
            {
                diffCount = 0;
                frequency *= frequencyFactor;
                if(frequency < 0.2f)
                {
                    frequencyFactor = 0.98f;
                }
                //Debug.Log("Difficulty" + frequency);
            }
            if (spawnCount > frequency * 50)
            {
                Spawn();
                spawnCount = 0;
            }
        }
	}

    void Spawn() {
        GameObject newObject = (GameObject) GameObject.Instantiate(obstacle1);
        int randomRope = Random.Range(0, 3);
        if (lastSpawn == 0)
        {
            randomRope = Random.Range(1, 3);
        }
        else if (lastSpawn == 1)
        {
            randomRope = Random.Range(0, 2);
            if(randomRope == 1)
            {
                randomRope++;
            }
        }
        else if (lastSpawn == 2)
        {
            randomRope = Random.Range(0, 2);
        }
        if(randomRope == lastSpawn)
        {
            randomRope = Random.Range(0, 3);
        }
        lastSpawn = randomRope;
        float newX = ropePositions[0] + (ropePositions[1] - ropePositions[0]) * randomRope;
        newObject.transform.position = new Vector2(newX, cam.transform.position.y + cam.orthographicSize + 1f);
        newObject.transform.parent = parent.transform;

        //Debug.Log(newObject.transform.position.y - lastSpawnPos);
        lastSpawnPos = newObject.transform.position.y;
    }
}
