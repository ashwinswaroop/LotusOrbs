using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {

        SceneManager.LoadScene("Game");

    }

    public void Options()
    {

        SceneManager.LoadScene("Options");

    }

    public void Leaderboard()
    {

        SceneManager.LoadScene("Leaderboard");

    }

    public void Help()
    {

        SceneManager.LoadScene("Help");

    }
}
