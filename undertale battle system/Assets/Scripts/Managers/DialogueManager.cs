using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public bool shouldTalk;
    public TextMeshPro text;
    public GameObject enemyTextBackground;
    public TextMeshPro textEnemy;
    public string dialogueTxt;
    public string enemyTxt;
    public AudioClip clip;
    public AudioClip enemyClip;
    private GameObject audioHolder;
    private List<AudioSource> sources;
    public bool done = false;
    private bool canNarrate;
    public float talkingSpeed = 0.1f;
    [HideInInspector]
    public static DialogueManager instance;
    void Awake() => instance = this;

    public void Talking(Action talkAction)
    {
        if (canNarrate)
        {
            StartCoroutine(StartTalking(talkAction));
        }
        
    }
    void Start()
    {
        sources = new List<AudioSource>();
        audioHolder = new GameObject("Audio Holder");
        audioHolder.transform.parent = transform;
        canNarrate = true;
        StartCoroutine(StartTalking(null));
    }
    IEnumerator StartTalking(Action action)
    {
        done = false;
        canNarrate = false;
        char[] chars = dialogueTxt.ToCharArray();
        text.text = "";
        for (int i = 0; i < chars.Length; i++)
        {
            AudioSource s = audioHolder.AddComponent<AudioSource>();
            text.text += chars[i];
            s.clip = clip;
            s.pitch = UnityEngine.Random.Range(0.99f,1);
            s.Play();
            sources.Add(s);
            yield return new WaitForSeconds(talkingSpeed);

        }
        
        int playingSources = 0;
        do
        {
            playingSources = 0;

            for (int i = 0; i < sources.Count; i++)
            {
                if (!sources[i].isPlaying)
                {
                    Destroy(sources[i]);
                    sources.RemoveAt(i);
                    i--;
                }
                else
                    playingSources++;
            }
            yield return null;
        }
        while (playingSources > 0);
        if (shouldTalk)
        {
            StartCoroutine(EnemyTalking(action));
            text.text = "";
        }
        else
        {
            done = true;
            canNarrate = true;
            enemyTextBackground.SetActive(false);
            action?.Invoke();
        }
        
    }

    IEnumerator EnemyTalking(Action action)
    {
        done = false;
        canNarrate = false;
        enemyTextBackground.SetActive(true);
        char[] chars = enemyTxt.ToCharArray();
        textEnemy.text = "";
        for (int i = 0; i < chars.Length; i++)
        {
            AudioSource s = audioHolder.AddComponent<AudioSource>();
            textEnemy.text += chars[i];
            s.clip = clip;
            s.pitch = UnityEngine.Random.Range(0.99f, 1);
            s.Play();
            sources.Add(s);
            yield return new WaitForSeconds(talkingSpeed);

        }
        int playingSources = 0;
        do
        {
            playingSources = 0;

            for (int i = 0; i < sources.Count; i++)
            {
                if (!sources[i].isPlaying)
                {
                    Destroy(sources[i]);
                    sources.RemoveAt(i);
                    i--;
                }
                else
                    playingSources++;
            }
            yield return null;
        }
        while (playingSources > 0);
        done = true;
        canNarrate = true;
        enemyTextBackground.SetActive(false);
        action?.Invoke();
    }
}
