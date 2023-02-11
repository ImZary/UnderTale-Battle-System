using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public static Attacking instance;
    void Awake() => instance = this;
    float time;
    public float maxTime;
    float progress;
    float playerDamage;
    float damageDealt;

    public Transform pointerObject;
    public Vector2 leftPos;
    public Vector2 rightPos;
    EnemyVars enemy;
    public bool isAttacking;
    [SerializeField] private SpriteRenderer attackBg;
    public GameObject normal;
    public GameObject damaged;
    public TextMeshPro damageTxt;
    public Color missColor;
    public Color damageColor;


    void Start()
    {
        enemy = FindObjectOfType<EnemyVars>();     
    }
    float PointerProgressToAttackMultiplier(float progress)
    {
        return Mathf.Min(progress * (playerDamage * 2), (1 - progress) * (playerDamage * 2));
    }

    void Update()
    {
        damageDealt = Mathf.Round(PointerProgressToAttackMultiplier(progress)) - enemy.defendValue;
        
        if (isAttacking)
        {
            progress = time / maxTime;
            pointerObject.position = Vector2.Lerp(leftPos, rightPos, progress);

            time += Time.deltaTime;
            if (time > 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    AudioManager.instance.Slashing();
                    StartCoroutine(AfterAttack());
                    StartCoroutine(Damage());
                }
            }
            
        }
    }
    public void StartAttacking(float playerDmg) 
    {
        isAttacking = true;
        playerDamage = playerDmg;
        attackBg.enabled = true;
        pointerObject.gameObject.SetActive(true);
    }


    IEnumerator Damage()
    {
        normal.SetActive(false);
        damaged.SetActive(true);
        if (damageDealt <= 0)
        {
            damageTxt.text = "MISS";
            damageTxt.color = missColor;
        }

        if (damageDealt > 0)
        {
            damageTxt.text = damageDealt.ToString();
        }
        yield return new WaitForSeconds(0.5f);
        normal.SetActive(true);
        damaged.SetActive(false);
        damageTxt.text = "";
        damageTxt.color = damageColor;
    }

    IEnumerator AfterAttack()
    {
        if (damageDealt > 0)
        {
            enemy.curHP -= damageDealt;
        }
        isAttacking = false;
        yield return new WaitForSeconds(1);
        attackBg.enabled = false;
        pointerObject.gameObject.SetActive(false);
        time = 0;
        leftPos.x = leftPos.x * -1;
        rightPos.x = rightPos.x * -1;
    }
}
