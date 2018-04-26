using UnityEngine;
using System.Collections;

public class PlayerContact : MonoBehaviour {

    public GameObject PostGameMenu;
    PlayerBehaviour playerScript;
    GameManager gm;
    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    void OnTriggerEnter2D (Collider2D col) 
    {
        if (col.tag == "Enemy" && gm.GetPlaying())
        {
            //Time.timeScale = 0;
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            GameObject retry = (GameObject)GameObject.Instantiate (PostGameMenu);
            Vector3 camPos = Camera.main.transform.position;
            retry.transform.position = new Vector3(camPos.x,camPos.y,-1f);
            retry.transform.parent = Camera.main.transform;

            Rigidbody2D playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            playerBody.gravityScale = 1f;

            float randomSpeed = Random.Range(-100f, 100f);
            playerBody.angularVelocity = randomSpeed;
            gm.SetPlaying(false);

            Transform highScore = retry.transform.Find("highScoreText");
            int playerScore = playerScript.GetScore();
            if (gm.GetHard())
            {
                if (playerScore > gm.GetHardHighScore())
                {
                    gm.SetHardHighScore(playerScore);
                    gm.Save();
                }
                highScore.GetComponent<TextMesh>().text = "High Score: " + gm.GetHardHighScore() + "m";
            }
            else
            {
                if (playerScore > gm.GetEasyHighScore())
                {
                    gm.SetEasyHighScore(playerScore);
                    gm.Save();
                }
                highScore.GetComponent<TextMesh>().text = "High Score: " + gm.GetEasyHighScore() + "m";
            }
            Transform scoreText = GameObject.Find("ScoreText").transform;
            scoreText.parent = retry.transform;
            TextMesh scoreMesh = scoreText.GetComponent<TextMesh>();
            TextMesh highScoreMesh = highScore.GetComponent<TextMesh>();
            scoreMesh.fontSize = highScoreMesh.fontSize;
            scoreMesh.characterSize = highScoreMesh.characterSize;
            scoreMesh.anchor = highScoreMesh.anchor;
            scoreMesh.alignment = highScoreMesh.alignment;
            scoreText.position = new Vector3(retry.transform.position.x, retry.transform.position.y + 0.25f, highScore.position.z);
            scoreMesh.text = "Current Score: " + scoreMesh.text.Replace("Score: ", "");
            //highScore.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 1.5f, -2f);
        }
    }

    void OnTouchDown(float[] input)
    {
        playerScript.OnTouchDown(input);
    }

    void OnTouchUp(float[] input)
    {
        playerScript.OnTouchUp(input);
    }

    void OnTouchStay(float[] input)
    {
        playerScript.OnTouchStay(input);
    }
}
