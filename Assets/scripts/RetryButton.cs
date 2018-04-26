using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour 
{
    void OnTouchUp () 
	{
		Time.timeScale = 1;
        SceneManager.LoadScene("game");
	}
}
