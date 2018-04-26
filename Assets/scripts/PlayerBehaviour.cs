using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
    
    public float autoFactor = 0.25f;
    private int autoCounter = 0;
    private int autoAnimCounter = 0;
    public int autoAnimStart = 10;

    private float speed = 0.5f;
    private string lastTouch = "";
    private float lastTouchPosX = -200f;

    private float ropeLeftPos;
    private float ropeMidPos;
    private float ropeRightPos;

    private int playerRope = 2;

    private float swipeRange = 0.015f;

    private int swipeId;
    private float swipeStart;
    private float swipeEnd;

    private Vector2 targetPos;

    private TextMesh scoreText;
    private int score = 0;

    private GameObject leftHand;
    private GameObject rightHand;

    private GameManager gm;

    void Awake() {
        targetPos = new Vector2(transform.position.x, transform.position.y);
    }

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();

        ropeLeftPos = GameObject.Find("rope1").transform.position.x;
        ropeMidPos = GameObject.Find("rope2").transform.position.x;
        ropeRightPos = GameObject.Find("rope3").transform.position.x;
        swipeRange *= Screen.width;

        leftHand = GameObject.Find("left_hand");
        rightHand = GameObject.Find("right_hand");
        //PlayJumpAnim();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gm.GetPlaying() && Time.timeScale == 1f)
        {
            autoAnimCounter++;
            if (!gm.GetHard())
            {
                targetPos = new Vector2(targetPos.x, targetPos.y + speed * autoFactor);
                autoCounter++;
                
                if (autoAnimCounter == 1)
                {
                    Animator animL = rightHand.GetComponent<Animator>();
                    if (animL != null)
                    {
                        animL.SetTrigger("AutoGrab");
                    }
                }else if (autoAnimCounter == autoAnimStart)
                {
                    Animator animL = leftHand.GetComponent<Animator>();
                    if (animL != null)
                    {
                        animL.SetTrigger("AutoGrab");
                    }
                }
                if (autoCounter > 1f / autoFactor)
                {
                    UpdateScore(1);
                    autoCounter = 0;
                }
            }
            //Debug.Log(Screen.width);
            //transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            float newX = Mathf.Lerp(transform.position.x, targetPos.x, 0.25f);
            float newY = Mathf.Lerp(transform.position.y, targetPos.y, 0.1f);
            //newX = Mathf.Clamp(newX, -2.0f,2.0f);
            transform.position = new Vector2(newX, newY);
        }
    }

    void PlayJumpAnim()
    {
        PlayLeftAnim();
        PlayRightAnim();
    }

    void PlayRightAnim()
    {
        Animator animL = rightHand.GetComponent<Animator>();
        if (animL != null)
        {
            animL.SetTrigger("Grab");
        }
    }

    void PlayLeftAnim()
    {
        Animator animR = leftHand.GetComponent<Animator>();
        if (animR != null)
        {
            animR.SetTrigger("Grab");
        }
    }

    public void OnTouchDown(float []touchData)
    {
        if (gm.GetPlaying() && Time.timeScale == 1f)
        {
            //Debug.Log("playerboop");
            swipeId = (int)touchData[2];
            swipeStart = touchData[0];
            if (gm.GetHard())
            {
                if (Mathf.Abs(swipeStart - touchData[0]) < Screen.width * 0.1)
                {
                    if (touchData[0] - Screen.width * 0.05 > lastTouchPosX)
                    {
                        Debug.Log("right");
                        RightTouch();
                        lastTouchPosX = touchData[0];
                    }
                    else if (touchData[0] + Screen.width * 0.05 < lastTouchPosX)
                    {
                        Debug.Log("left");
                        LeftTouch();
                        lastTouchPosX = touchData[0];
                    }
                }
            }
        }
    }

    public void OnTouchStay(float []touchData)
    {
        if (gm.GetPlaying() && Time.timeScale == 1f)
        {
            if (swipeStart != -200f && swipeId == (int)touchData[2])
            {
                swipeEnd = touchData[0];
                float swipeDisplacement = swipeStart - swipeEnd;
                //Debug.Log("start" + swipeStart);
                //Debug.Log("end" + swipeEnd);
                if (swipeDisplacement < -swipeRange)
                {
                    swipeId = -1;
                    PlayJumpAnim();
                    if (playerRope == 2)
                    {
                        targetPos = new Vector2(ropeRightPos, targetPos.y);
                        playerRope = 3;
                    }
                    else if (playerRope == 1)
                    {
                        targetPos = new Vector2(ropeMidPos, targetPos.y);
                        playerRope = 2;
                    }
                    swipeStart = -200f;
                    lastTouch = "";
                }
                else if (swipeDisplacement > swipeRange)
                {
                    swipeId = -1;
                    PlayJumpAnim();
                    if (playerRope == 3)
                    {
                        targetPos = new Vector2(ropeMidPos, targetPos.y);
                        playerRope = 2;
                    }
                    else if (playerRope == 2)
                    {
                        targetPos = new Vector2(ropeLeftPos, targetPos.y);
                        playerRope = 1;
                    }
                    swipeStart = -200f;
                    lastTouch = "";
                }
            }
        }
    }

    public void OnTouchUp(float []touchData)
    {
        if (gm.GetPlaying() && Time.timeScale == 1f)
        {
            //Debug.Log(pos.x);
            //Debug.Log(Screen.width/2);
            if (touchData[2] == swipeId)
            {
                swipeStart = -200f;
                swipeId = -1;
            }
        }
    }

    void LeftTouch() {
        if (lastTouch != "left"){
            targetPos = new Vector2(targetPos.x, targetPos.y + speed);
            PlayLeftAnim();
            lastTouch = "left";
            UpdateScore(1);
        }
    }

    void RightTouch() {
        if (lastTouch != "right"){
            targetPos = new Vector2(targetPos.x, targetPos.y + speed);
            PlayRightAnim();
            lastTouch = "right";
            UpdateScore(1);
        }
    }

    void UpdateScore(int val) {
        score += val;
        scoreText.text = "Score: " + score + "m";
    }

    public int GetScore()
    {
        return score;
    }

}
