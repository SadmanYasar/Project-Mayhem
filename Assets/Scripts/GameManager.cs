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
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;

        //use the refresh rate
        //set it to targetframerate
        //set it to DoSlowMo method in TimeManager
        Application.targetFrameRate = 80;

        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        ammoText.text = "0";
        
        switch (UnitySceneManager.GetActiveScene().buildIndex)
        {
            
            case 1:
                //Tutorial
                enemyCount = 4; 
                break;
            
            case 2:
                //Tutorial
                enemyCount = 11; 
                break;

        }
    }

    public static void checkForWin() {
        enemyCount--;
        Debug.Log(enemyCount);
        switch (enemyCount)
        {
            case 0:
                Debug.Log("You Win!");
                break;
            default:
                break;
        }
    }
}
