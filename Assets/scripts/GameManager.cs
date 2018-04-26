using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

    private bool playing = false;
    private bool hard = false;
    private bool musicPlaying = true;

    private int hardHighScore = 0;
    private int easyHighScore = 0;

	// Use this for initialization
	void Awake () {
        //transform.parent = Camera.main.transform;
        Load();
        if (!musicPlaying)
        {
            transform.GetComponent<AudioSource>().volume = 0f;
        }
    }

    public void InitPosition()
    {
        Camera cam = Camera.main;
        float camWidth = cam.orthographicSize * Screen.width / Screen.height;
        float camHeight = cam.orthographicSize;
        transform.position = new Vector2(cam.transform.position.x - camWidth + 0.25f, cam.transform.position.y + camHeight - 0.25f);
        GameObject title = GameObject.Find("Title");
        title.transform.position = new Vector2(-200f, -200f);
    }
	
	public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            easyHighScore = data.EasyHighScore;
            hardHighScore = data.HardHighScore;
            musicPlaying = data.MusicPlaying;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
		
		GameData data = new GameData();
		data.EasyHighScore = easyHighScore;
		data.HardHighScore = hardHighScore;
        data.MusicPlaying = musicPlaying;
		
		bf.Serialize(file,data);
		file.Close();
    }

    public void SetPlaying(bool b)
    {
        playing = b;
        if (playing == false)
        {
            InvokeRepeating("StopMusic", 0f, 0.016f);
        }
    }

    void StopMusic()
    {
        AudioSource audio = transform.GetComponent<AudioSource>();
        audio.pitch -= 0.016f;
        if (audio.pitch <= 0f)
        {
            audio.pitch = 0f;
            CancelInvoke();
        }
    }

    public bool GetPlaying()
    {
        return playing;
    }

    public void SetHard(bool b)
    {
        hard = b;
    }

    public bool GetHard()
    {
        return hard;
    }

    public int GetEasyHighScore()
    {
        return easyHighScore;
    }

    public void SetEasyHighScore(int newHighScore)
    {
        easyHighScore = newHighScore;
    }

    public int GetHardHighScore()
    {
        return hardHighScore;
    }

    public void SetHardHighScore(int newHighScore)
    {
        hardHighScore = newHighScore;
    }

    public bool GetMusic()
    {
        return musicPlaying;
    }

    public void SetMusic(bool b)
    {
        musicPlaying = b;
    }
}


[Serializable]
class GameData
{
    int easyHighScore;
    int hardHighScore;
    bool musicPlaying;

    public int EasyHighScore
    {
        get
        {
            return easyHighScore;
        }

        set
        {
            easyHighScore = value;
        }
    }

    public int HardHighScore
    {
        get
        {
            return hardHighScore;
        }

        set
        {
            hardHighScore = value;
        }
    }

    public bool MusicPlaying
    {
        get
        {
            return musicPlaying;
        }

        set
        {
            musicPlaying = value;
        }
    }
}
