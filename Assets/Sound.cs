using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
   public string Name;
   public AudioClip audio;

   [Range(0f,1f)]
   public float Volume;

   [Range(0.1f,3f)]
   public float pitch;

   public bool loop;

   [HideInInspector]
   public AudioSource source;
}
