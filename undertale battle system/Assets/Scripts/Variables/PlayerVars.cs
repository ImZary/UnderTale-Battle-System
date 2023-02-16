using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVars : MonoBehaviour
{
    public float atkValue;
    public float defValue;
    public float health;
   [SerializeField] Color soulFlashing;
   public Color soulOriginal;
    float soulAlpha;
    float soulAlphaFlash;
    float time;
    public float maxTime;
    [SerializeField] private SpriteRenderer soulSprite;
    bool invincible;
    [HideInInspector]
    public static PlayerVars instance;
    void Awake() => instance = this;



    void Start()
    {
        soulAlpha = soulSprite.color.a;
        soulAlphaFlash = soulAlpha / 2;
        soulOriginal = soulSprite.color;
        time = maxTime;
    }
    public void TakeDamage(float damageTaken)
    {
        if (!invincible)
        {
            health -= damageTaken;
            AudioManager.instance.takingDamage();
            invincible = true;
        }
    }

    void FlashSoul()
    {
        
        if (time > 0)
        {
            soulAlphaFlash = soulAlphaFlash * -1;
            soulSprite.color = soulFlashing;
        }
        else
        {
            soulSprite.color = soulOriginal;
            time = maxTime;
            invincible = false;
        }
        
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (invincible)
        {
            FlashSoul();
        }
    }
}
