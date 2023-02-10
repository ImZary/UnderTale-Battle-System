using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    PlayerMovement player;
    public Sprite brokenSoul;
    public AudioClip soulBreakSfx;
    public AudioClip gameOverMusic;
    public AudioSource sfxPlayer;
    public AudioSource musicPlayer;
    public ParticleSystem deathParticles;
    public GameObject battleObjects;
    private SpriteRenderer playerSprite;
    public GameObject gameOverScreen;
    bool initiating;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerSprite = player.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player.GetComponent<PlayerVars>().health <= 0 && !initiating)
        {
            StartCoroutine(DeathSequence());
        }    
    }
    IEnumerator DeathSequence()
    {
        initiating = true;
        player.enabled = false;
        battleObjects.SetActive(false);
        playerSprite.color = player.GetComponent<PlayerVars>().soulOriginal;
        playerSprite.sprite = brokenSoul;
        musicPlayer.clip = null;
        sfxPlayer.clip = soulBreakSfx;
        sfxPlayer.Play();
        yield return new WaitForSeconds(1.015f);
        player.GetComponent<SpriteRenderer>().enabled = false;
        deathParticles.Play();
        musicPlayer.clip = gameOverMusic;
        musicPlayer.Play();
        gameOverScreen.SetActive(true);

    }
}
