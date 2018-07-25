using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

    public Sprite soundOff, soundOn;
    public Text placeholder;
    public Text userID;
    public Text info;
    public InputField i;
    string user;
    float highScore, scoreFraction;

    // Use this for initialization
    void Start () {
        user = PlayerPrefs.GetString("UserID", "");
        if (user != "")
        {
            placeholder.text = user;
            i.interactable = false;
            highScore = PlayerPrefs.GetFloat("HighScore", 0);
            scoreFraction = Mathf.Floor((highScore - Mathf.Floor(highScore)) * 100);
            info.text = "HIGH SCORE: " + Mathf.Floor(highScore).ToString() + ":" + scoreFraction.ToString();
            info.alignment = TextAnchor.MiddleCenter;
        }
        else
        {
            info.text = "Your user ID can have a max length of 12 and cannot be changed";
        }
      
    }
	
	// Update is called once per frame
	void Update () {

 

    }

    public void Done()
    {
        if(userID.text.Length > 0 && userID.text.Length <= 12)
            PlayerPrefs.SetString("UserID", userID.text);
        SceneManager.LoadScene("Menu");
    }
}
