using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneManager : MonoBehaviour
{
    public static SceneManager SceneInstance;
    // Start is called before the first frame update
    void Awake()
    {
        SceneInstance = this;
        DontDestroyOnLoad(SceneInstance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
