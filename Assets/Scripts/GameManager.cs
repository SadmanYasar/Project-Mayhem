using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    public static int shotByPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;

        //use the refresh rate
        //set it to targetframerate
        //set it to DoSlowMo method in TimeManager
        Application.targetFrameRate = 80;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
