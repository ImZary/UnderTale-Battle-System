using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleDisplayer : MonoBehaviour
{
    public TextMeshProUGUI actingText;
    public EnemyVars enemy;
    public PlayerVars player;
    void Start()
    {
        enemy.curHP = enemy.maxHP;
    }

    public void OnActing(ActVars actVars)
    {
        actVars.curMercy += actVars.mercyValue[0];
        actingText.text = actVars.actTxt[0];
        actVars.actTxt.RemoveAt(0);
        actVars.mercyValue.RemoveAt(0);
    }

    public void OnSpare(ActVars actVars) 
    {
        if (actVars.curMercy >= actVars.mercyMax)
        {
            EndBattle();
        }
    }

    void EndBattle()
    {
        Debug.Log("The Battle Ended");
    }

    public void OnAttack()
    {
        enemy.curHP -= player.atkValue - enemy.defendValue;
    }
}
