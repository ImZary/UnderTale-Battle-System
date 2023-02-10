using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    float time;
    public float maxTime;
    public Transform pointerObject;
    public Vector2 leftPos;
    public Vector2 rightPos;
    float progress;
    EnemyVars enemy;
    public bool isAttacking;
    float playerDamage;
    [SerializeField] private SpriteRenderer attackBg;

    void Start()
    {
        enemy = FindObjectOfType<EnemyVars>();     
    }
    float PointerProgressToAttackMultiplier(float progress)
    {
        return Mathf.Clamp01(Mathf.Min(progress * playerDamage, -progress * playerDamage + playerDamage));
       
    }

    void Update()
    {
       
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
                }
            }
            
        }
    }
    public void StartAttacking(float playerDmg) 
    {
        Debug.Log("Initiating attack");
        isAttacking = true;
        playerDamage = playerDmg;
        attackBg.enabled = true;
        pointerObject.gameObject.SetActive(true);
    }

    IEnumerator AfterAttack()
    {
        enemy.curHP -= PointerProgressToAttackMultiplier(progress);
        pointerObject.position = pointerObject.position;
        isAttacking = false;
        yield return new WaitForSeconds(1);
        attackBg.enabled = false;
        pointerObject.gameObject.SetActive(false);
        time = 0;
        leftPos.x = leftPos.x * -1;
        rightPos.x = rightPos.x * -1;
    }
}
