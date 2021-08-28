using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager =  UnityEngine.SceneManagement.SceneManager;

public class MainMenuManager : MonoBehaviour
{
    public Text startText;
    public GameObject levels;

    private void Start() {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 1/75 ;

        Application.targetFrameRate = 75;
        AudioManager.instance.ChangeAllPitchValues(0.5f);
        AudioManager.instance.StopAllAudio();
        AudioManager.instance.Play("MainMenuTheme");
    }

    public void enableLevelList() {
        startText.gameObject.SetActive(false);
        levels.SetActive(true);
    }

    public void exit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void loadTutorial() => UnitySceneManager.LoadScene(1);

    public void loadOne() => UnitySceneManager.LoadScene(2);
    public void loadTwo() => UnitySceneManager.LoadScene(3);
    public void loadThree() => UnitySceneManager.LoadScene(4);

}
