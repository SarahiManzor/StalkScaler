using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    public GameObject cloud;
    public float scale = 1f;
    public int orderNumInSortLayer = 0;
    public int cloudsPerSpawn = 4;
    public float speedScale = 1f;

    private Camera cam;

    private float cloudSpriteHeight;
    private float camHorizontalExtend;
    private float camVerticalExtend;
    private float lastCloudY = 0f;
    private float lastCloudX = 0f;
    private GameManager gm;

    // Use this for initialization
    void Awake ()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cam = Camera.main;
        camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
        //Debug.Log(camHorizontalExtend);
        camVerticalExtend = cam.orthographicSize;// * Screen.height / Screen.width;
        //Debug.Log(camVerticalExtend);
	}

    void Start()
    {
        Bounds cloudBounds = cloud.GetComponent<SpriteRenderer>().sprite.bounds;
        cloudSpriteHeight = cloudBounds.max.y - cloudBounds.min.y;
        //Debug.Log(cloudSpriteHeight);
        SpawnClouds(cloudsPerSpawn, 0f, -4);
        SpawnClouds(cloudsPerSpawn, 0f, -3);
        SpawnClouds(cloudsPerSpawn, 0f, -2);
        SpawnClouds(cloudsPerSpawn, 0f, -1);
        SpawnClouds(cloudsPerSpawn, 0f, 0);
    }

    void SpawnClouds(int amountOfClouds, float startFrom, int xOffSet)
    {
        float tempMaxY = 0f;
        for (int i = 0; i < amountOfClouds; i++)
        {
            float xOffVal = xOffSet * (camHorizontalExtend*2);
            float randomX = Random.Range(transform.position.x - camHorizontalExtend, transform.position.x + camHorizontalExtend/2) + xOffVal;
            float minY = startFrom + (cam.transform.position.y - camVerticalExtend) + (2*camVerticalExtend / amountOfClouds) * i + cloudSpriteHeight/2;
            float maxY = minY + (2 * camVerticalExtend / amountOfClouds) - cloudSpriteHeight;
            float randomY = Random.Range(minY, maxY);
            Vector3 newPos = new Vector3(randomX, randomY, transform.position.z);
            tempMaxY = maxY;

            GameObject newCloud = (GameObject)Instantiate(cloud, newPos, transform.rotation);
            newCloud.transform.parent = gameObject.transform;
            newCloud.transform.localScale = new Vector3(scale, scale, 1f);
            newCloud.GetComponent<SpriteRenderer>().sortingOrder = orderNumInSortLayer;
            newCloud.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, scale * 1.75f);

            float randomVel = Random.Range(0.25f, 0.5f);
            newCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(randomVel * speedScale, 0f);
        }
        if (lastCloudY < tempMaxY) {
            lastCloudY = tempMaxY;
        }
    }

    // Update is called once per frame
    void Update () {
		if(cam.transform.position.y > lastCloudY)
        {
            //Debug.Log("Spawn");
            SpawnClouds(cloudsPerSpawn, 2 * cam.orthographicSize + cloudSpriteHeight, -2);
            SpawnClouds(cloudsPerSpawn, 2 * cam.orthographicSize + cloudSpriteHeight, -1);
            SpawnClouds(cloudsPerSpawn, 2 * cam.orthographicSize + cloudSpriteHeight, 0);
        }
	}
}
