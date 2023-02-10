using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clipList = new List<AudioClip>();
    public AudioSource audioSource;
    [HideInInspector]
    public static AudioManager instance;
    void Awake() => instance = this.GetComponent<AudioManager>();
    
        
    
    public void takingDamage()
    {
        audioSource.clip = clipList[0];
        audioSource.Play();
    }

    public void Hovering()
    {
        audioSource.clip = clipList[1];
        audioSource.Play();
    }
    public void Selecting()
    {
        audioSource.clip = clipList[2];
        audioSource.Play();
    }
    public void Slashing()
    {
        audioSource.clip = clipList[3];
        audioSource.Play();
    }
}
