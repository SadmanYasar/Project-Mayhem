using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
