using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Transform prefab;
    public Transform sPrefab;
    public Transform cPrefab;
    public Transform lPrefab;
    public Transform rPrefab;
    public Transform hPrefab;
    public GameObject lController;
    public GameObject sound;
    public Button retryButton;
    public Button homeButton;
    public Button leaderboardButton;
    public Button settingsButton;
    public ParticleSystem pSys;
    public Text time;
    public Text newHighScore;
    public Text createuser;
    public int score = 0;
    public int deaths = 0;
    public int streak = 0;
    public Sprite[] spriteList;
    public Sprite[] sphereSpriteList;
    public Sprite gHealth;
    public Sprite wHealth;
    public Sprite retry;
    List<GameObject> healthList;
    List<GameObject> targetList;
    GameObject[] tempList;
    GameObject obj, obj1, obj2, obj3, obj4;
    float sphereTimer = 0.5f;
    float levelTimer = 10f;
    float levelTime = 10f;
    float changeColorTimer = 5f;
    float changeColorTime = 5f;
    float scoreTimer = 0f;
    float scoreFraction = 0f;
    int level = 1;
    List<int> randomList;
    float highScore;
    bool highScoreDisplayed = false;
    bool switchColor = true;
    bool done;
    List<LeaderboardController.Score> leaderboardScores;
    int len;

    void Start () {
        //deaths = 4;
        lController.GetComponent<LeaderboardController>().LoadScores();
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetFloat("HighScore", 0);
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        targetList = new List<GameObject>();
        healthList = new List<GameObject>();
        done = false;
        obj = Instantiate(prefab, new Vector3(-6, 0, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        obj.gameObject.GetComponent<TargetController>().targetColor = 0;
        targetList.Add(obj);
        obj = Instantiate(prefab, new Vector3(-3, 3, 0), Quaternion.identity).gameObject;
        obj.transform.Rotate(new Vector3(0, 0, -90));
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[1];
        obj.gameObject.GetComponent<TargetController>().targetColor = 1;
        targetList.Add(obj);
        obj = Instantiate(prefab, new Vector3(-3, -3, 0), Quaternion.identity).gameObject;
        obj.transform.Rotate(new Vector3(0, 0, 90));
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[2];
        obj.gameObject.GetComponent<TargetController>().targetColor = 2;
        targetList.Add(obj);
        obj = Instantiate(prefab, new Vector3(6, 0, 0), Quaternion.identity).gameObject;
        obj.transform.Rotate(new Vector3(0, 0, 180));
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[3];
        obj.gameObject.GetComponent<TargetController>().targetColor = 3;
        targetList.Add(obj);
        obj = Instantiate(prefab, new Vector3(3, 3, 0), Quaternion.identity).gameObject;
        obj.transform.Rotate(new Vector3(0, 0, -90));
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[4];
        obj.gameObject.GetComponent<TargetController>().targetColor = 4;
        targetList.Add(obj);
        obj = Instantiate(prefab, new Vector3(3, -3, 0), Quaternion.identity).gameObject;
        obj.transform.Rotate(new Vector3(0, 0, 90));
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[5];
        obj.gameObject.GetComponent<TargetController>().targetColor = 5;
        targetList.Add(obj);
      
        obj1 = Instantiate(lPrefab, new Vector3(-3, 0, 0), Quaternion.identity).gameObject;
        obj2 = Instantiate(cPrefab, new Vector3(0, 0, 0), Quaternion.identity).gameObject;
        obj3 = Instantiate(rPrefab, new Vector3(3, 0, 0), Quaternion.identity).gameObject;

        obj = Instantiate(hPrefab, new Vector3(-0.4f, 3f, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = gHealth;
        healthList.Add(obj);

        obj = Instantiate(hPrefab, new Vector3(-0.2f, 3f, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = gHealth;
        healthList.Add(obj);

        obj = Instantiate(hPrefab, new Vector3(0, 3f, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = gHealth;
        healthList.Add(obj);

        obj = Instantiate(hPrefab, new Vector3(0.2f, 3f, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = gHealth;
        healthList.Add(obj);

        obj = Instantiate(hPrefab, new Vector3(0.4f, 3f, 0), Quaternion.identity).gameObject;
        obj.gameObject.GetComponent<SpriteRenderer>().sprite = gHealth;
        healthList.Add(obj);

    }

    void Update () {
        if (deaths < 5)
        {
            if (scoreTimer > highScore && highScore!=0 && highScoreDisplayed == false)
            {
                newHighScore.text = "NEW HIGH SCORE!";
                Invoke("HideText", 5);
            }
            else
            {
                newHighScore.text = "";
            }
            if(streak == 3)
            {
                if (deaths > 0)
                {
                    deaths--;
                }
                sound.GetComponent<AudioSource>().Play();
                streak = 0;
            }
            for (int i = 0; i < deaths; i++)
            {
                healthList[4 - i].GetComponent<SpriteRenderer>().sprite = wHealth;
            }
            for (int i = 0; i < 5-deaths; i++)
            {
                healthList[i].GetComponent<SpriteRenderer>().sprite = gHealth;
            }
            scoreTimer += Time.deltaTime;
            scoreFraction = Mathf.Floor((scoreTimer - Mathf.Floor(scoreTimer))*100);
            time.text = Mathf.Floor(scoreTimer).ToString() + ":" + scoreFraction.ToString();
            sphereTimer -= Time.deltaTime;
            levelTimer -= Time.deltaTime;
            changeColorTimer -= Time.deltaTime;

            if (levelTimer <= 0f)
            {
                level++;
                levelTimer = levelTime;
            }

            if (changeColorTimer <= 0f)
            {
                switchColor = true;
                randomList = GenerateRandomList1();
                for (int i = 0; i < 3; i++)
                {
                    tempList = GameObject.FindGameObjectsWithTag("Sphere");
                    foreach(GameObject g in tempList)
                    {
                        if (g.transform.position.x <= -1.5)
                            switchColor = false;
                    }
                    if (switchColor)
                    {
                        targetList[i].gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[randomList[i]];
                        targetList[i].gameObject.GetComponent<TargetController>().targetColor = randomList[i];
                    }
                }
                switchColor = true;
                randomList = GenerateRandomList2();
                for (int i = 3; i < 6; i++)
                {
                    tempList = GameObject.FindGameObjectsWithTag("Sphere");
                    foreach (GameObject g in tempList)
                    {
                        if (g.transform.position.x >= 1.5)
                            switchColor = false;
                    }
                    if (switchColor)
                    {
                        targetList[i].gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[randomList[i - 3]];
                        targetList[i].gameObject.GetComponent<TargetController>().targetColor = randomList[i - 3];
                    }
                }
                changeColorTimer = changeColorTime;
            }

            if (sphereTimer <= 0f)
            {
                obj = Instantiate(sPrefab, new Vector3(0, -3, 0), transform.rotation).gameObject;
                int index = Random.Range(0, 6);
                obj.gameObject.GetComponent<SphereController>().sphereColor = index;
                obj.gameObject.GetComponent<SpriteRenderer>().sprite = sphereSpriteList[index];
                obj.gameObject.GetComponent<SphereController>().lpp = obj1.transform;
                obj.gameObject.GetComponent<SphereController>().cpp = obj2.transform;
                obj.gameObject.GetComponent<SphereController>().rpp = obj3.transform;
                if (level == 1)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 2.0f;
                    sphereTimer = 3f;
                    changeColorTime = 5f;
                    levelTime = 10f;
                }
                else if (level == 2)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 2.5f;
                    sphereTimer = 2.5f;
                    changeColorTime = 4f;
                    levelTime = 10f;
                }
                else if (level == 3)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 2.5f;
                    sphereTimer = 2.0f;
                    changeColorTime = 3f;
                    levelTime = 10f;
                }
                else if (level == 4)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1.5f;
                    changeColorTime = 2f;
                    levelTime = 10f;
                }
                else if (level == 5)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1.4f;
                    changeColorTime = 1;
                    levelTime = 10f;
                }
                else if (level == 6)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1.3f;
                    changeColorTime = 0.9f;
                    levelTime = 10f;
                }
                else if (level == 7)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1.2f;
                    changeColorTime = 0.9f;
                    levelTime = 10f;
                }
                else if (level == 8)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1.1f;
                    changeColorTime = 0.8f;
                    levelTime = 10f;
                }
                else if (level == 9)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1f;
                    changeColorTime = 0.8f;
                    levelTime = 10f;
                }
                else if (level >= 10)
                {
                    obj.gameObject.GetComponent<SphereController>().speed = 3.0f;
                    sphereTimer = 1f;
                    changeColorTime = 0.7f;
                    levelTime = 10f;
                }
            }
        }
        else
        {
            if (!done)
            {
                pSys.gameObject.SetActive(true);
                healthList[0].GetComponent<SpriteRenderer>().sprite = wHealth;
                foreach (GameObject tObj in targetList)
                {
                    tObj.GetComponent<Rigidbody2D>().isKinematic = false;
                    Destroy(tObj, 2f);
                }
                foreach (GameObject hObj in healthList)
                {
                    hObj.GetComponent<Rigidbody2D>().isKinematic = false;
                    Destroy(hObj, 2f);
                }
                obj1.GetComponent<Rigidbody2D>().isKinematic = false;
                Destroy(obj1, 1f);
                obj2.GetComponent<Rigidbody2D>().isKinematic = false;
                Destroy(obj2, 1f);
                obj3.GetComponent<Rigidbody2D>().isKinematic = false;
                Destroy(obj3, 1f);
                if (scoreTimer > highScore)
                {
                    PlayerPrefs.SetFloat("HighScore", scoreTimer);
                    newHighScore.text = "NEW HIGH SCORE!";
                }
                else
                {
                    scoreFraction = Mathf.Floor((highScore - Mathf.Floor(highScore)) * 100);
                    newHighScore.text = "HIGH SCORE: " + Mathf.Floor(highScore).ToString() + ":" + scoreFraction.ToString();
                }
                if (PlayerPrefs.GetString("UserID", "unknown") == "unknown")
                {
                    createuser.text = "Create a user ID in the settings menu to be displayed on the leaderboards. This user ID cannot be changed.";
                    settingsButton.gameObject.SetActive(true);
                }
                newHighScore.fontSize = 10;
                newHighScore.rectTransform.localPosition = new Vector3(0, 100, 0);
                time.fontSize = 18;
                time.rectTransform.localPosition = new Vector3(0, 36, 0);
                retryButton.gameObject.SetActive(true);
                homeButton.gameObject.SetActive(true);
                leaderboardButton.gameObject.SetActive(true);
                leaderboardScores = lController.GetComponent<LeaderboardController>().ToListHighToLow();
                len = leaderboardScores.Count;
                if (len < 10 || PlayerExists())
                {
                    lController.GetComponent<LeaderboardController>().AddScore(PlayerPrefs.GetString("UserID", "unknown"), (int)(scoreTimer*100));
                }
                else if(len >= 10)
                {
                    lController.GetComponent<LeaderboardController>().DeleteScore(leaderboardScores[len - 1].playerName, PlayerPrefs.GetString("UserID", "unknown"), (int)(scoreTimer * 100));   
                }
                done = true;
            }
        }
    }

    public List<int> GenerateRandomList1()
    {

        List<int> uniqueNumbers = new List<int>();
        List<int> finishedNumbers = new List<int>();
        int maxNumbers = 3;

        for (int i = 0; i < maxNumbers; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 0; i < maxNumbers; i++)
        {
            int ranNum = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            finishedNumbers.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }

        return finishedNumbers;
    }

    public List<int> GenerateRandomList2()
    {

        List<int> uniqueNumbers = new List<int>();
        List<int> finishedNumbers = new List<int>();
        int maxNumbers = 6;

        for (int i = 3; i < maxNumbers; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 3; i < maxNumbers; i++)
        {
            int ranNum = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            finishedNumbers.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }

        return finishedNumbers;
    }

    void HideText()
    {
        if (deaths < 5)
        {
            newHighScore.text = "";
            highScoreDisplayed = true;
        }
    }

    public void Retry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Home()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Leaderboard()
    {

        SceneManager.LoadScene("Leaderboard");

    }

    public void Options()
    {

        SceneManager.LoadScene("Options");

    }

    public bool PlayerExists()
    {
        foreach (LeaderboardController.Score scr in leaderboardScores)
        {
            if (scr.playerName.Trim().Equals(PlayerPrefs.GetString("UserID", "unknown")))
            {
                return true;
            }
        }
        return false;
    }
}
