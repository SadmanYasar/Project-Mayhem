using UnityEngine.Audio;
using System;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if ( instance == null )
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audio;
            s.source.volume = s.Volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

/*     private void Start() {
        //Checks if MainMenu
        if ( UnitySceneManager.GetActiveScene().buildIndex == 0 )
        {
            AudioManager.instance.StopAllAudio();
            Play("MainMenuTheme");
        }
    } */

    
    public void Play( string name )
    {
       Sound s = Array.Find(sounds, sound => sound.Name == name);
       s.source.PlayOneShot(s.audio);
    }

    public void ChangeAllPitchValues(float pitchDifference) {
    // Loop through our whole sound array.
    foreach (Sound s in sounds) {
        // Adjust pitch value equal to the given difference.
        s.source.pitch = pitchDifference;
    }
    }
    public void StopAllAudio() {
        foreach (Sound s in sounds) {
        //Turn it all off
        s.source.Stop();
    }

}
}
