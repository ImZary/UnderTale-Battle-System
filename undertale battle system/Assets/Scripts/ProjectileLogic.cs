using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public EnemyVars enemy;
    PlayerVars player;
    public List<Sprite> projectileSprite;
    int spriteShown;
    float time;
    public float shownTime;
    void Start()
    {
        enemy = FindObjectOfType<EnemyVars>().GetComponent<EnemyVars>();
    }
    void Awake()
    {
        GameObject pelleteParent = GameObject.FindGameObjectWithTag("PelleteHolder");
        this.transform.parent = pelleteParent.transform;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject.GetComponent<PlayerVars>();
            player.TakeDamage(enemy.attackValue - player.defValue);
            Debug.Log("The player got damaged!");
        }    
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerVars>();
            player.TakeDamage(enemy.attackValue - player.defValue);
        }
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = projectileSprite[0];
    }
}
