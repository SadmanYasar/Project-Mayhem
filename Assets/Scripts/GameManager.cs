using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    public static int shotByPlayer;
    public static int enemyCount;
    public static Text ammoText;
    public static Text scoreText;

    public static Text winText;


    public static Button quitText;

    public static Button continueButton;

    public static Text restartText;

    public static int prevScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 1/75 ;
        AudioManager.instance.StopAllAudio();
        AudioManager.instance.ChangeAllPitchValues(1f);
        GameOver = false;
        Application.targetFrameRate = -1;
        //use the refresh rate
        //set it to targetframerate
        //set it to DoSlowMo method in TimeManager

        AudioManager.instance.StopAllAudio();
        prevScore = 0;

        //ammo
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        ammoText.text = "";

        //score
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "";

        //quit
        quitText = GameObject.Find("Quit").GetComponent<Button>();
        quitText.gameObject.SetActive(false);

        //continue
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        continueButton.gameObject.SetActive(false);

        //restart
        restartText = GameObject.Find("RestartText").GetComponent<Text>();
        restartText.text = "";

        //wintext
        winText = GameObject.Find("WinText").GetComponent<Text>();
        winText.text = "";

        
        
        switch (UnitySceneManager.GetActiveScene().buildIndex)
        {
            
            case 1:
                //Tutorial
                AudioManager.instance.Play("TutorialTheme");
                
                enemyCount = 4; 
                break;
            
            case 2:
                //Level 1
                AudioManager.instance.Play("Level 1 Theme");

                enemyCount = 11; 
                break;

            case 3:
                //Level 2
                AudioManager.instance.Play("Level 2 Theme");

                enemyCount = 14; 
                break;    

            case 4:
                //Level 3
                AudioManager.instance.Play("Level 3 Theme");

                enemyCount = 12; 
                break;    
        }
    }

    private void Update() {
        if ( Input.GetKey(KeyCode.Escape) )
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 1/75;
            AudioManager.instance.ChangeAllPitchValues(0.3f);
            quitText.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape) )
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 1/75;
            AudioManager.instance.ChangeAllPitchValues(1f);
            quitText.gameObject.SetActive(false);
            
        }

        if ( Input.GetKeyDown(KeyCode.R) && restartText.text != ""  )
        {
            UnitySceneManager.LoadScene(UnitySceneManager.GetActiveScene().buildIndex);
        }


    }

    public static void Score(int scoreValue) {
        prevScore += scoreValue;
        scoreText.text = "SCORE: " + prevScore;
    }

    public static void checkForWin() {
        enemyCount--;
        switch (enemyCount)
        {
            
            case 0:
                //win
                winText.text = "YOU WIN";
                continueButton.gameObject.SetActive(true);
                quitText.gameObject.SetActive(true);
                break;
        }

    }

    public void Quit() {
        UnitySceneManager.LoadScene(0);
    }

    public void LevelUp() {
            int levelNo = UnitySceneManager.GetActiveScene().buildIndex + 1;
            switch ( levelNo)
            {
                case 5:
                    winText.text = "Thank you for playing";
                    break;
                default:
                    UnitySceneManager.LoadScene(levelNo);
                    break;
            }

    }

}        
