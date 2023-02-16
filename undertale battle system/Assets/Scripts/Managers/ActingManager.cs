using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ActingManager : MonoBehaviour
{
    int maxSelectionInt;
    int minSelectionInt;
    public int selectionInt;
    bool isFighting;
    public string spareMessage;
    public List<ActingButtons> buttons;
    public SpriteRenderer soul;
    public TextMeshPro actingText;
    public bool isActing;
    public GameObject actObjects;
    public int totalMercy;
    public int totalMercyMax;
    public List<string> flavorText;
    public float time;
    public bool canAct = true;
    void Start()
    {
        isFighting = BattleManager.battleInstance.isFighting;
        maxSelectionInt = 3;
        minSelectionInt = 0;
    }
    void Update()
    {
        if (!isFighting && isActing)
        {
            if (selectionInt > maxSelectionInt)
            {
                selectionInt = 0;
            }
            if (selectionInt < minSelectionInt)
            {
                selectionInt = 3;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectionInt--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectionInt++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectionInt -= 2;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectionInt += 2;
            }
            Selection();
            time += Time.deltaTime;
            if (time > 0.25f)
            {
                if(canAct)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        canAct = false;
                        Selected();
                    }
                }
            
                
            }
           
        }
        
    }

    void Selecting(int selectedInt)
    {
        if (buttons[selectedInt].selected)
        {
            soul.transform.position = buttons[selectedInt].soulPosition.position;
        }
    }
    void Deselecting(int deselectionInt)
    {
        buttons[deselectionInt].selected = false;
    }
    void Selection()
    {

        if (selectionInt == 0)
        {
            buttons[selectionInt].selected = true;
            Selecting(0);
        }
        else
        {
            Deselecting(0);
        }
        if (selectionInt == 1)
        {
            buttons[selectionInt].selected = true;
            Selecting(1);
        }
        else
        {
            Deselecting(1);
        }
        if (selectionInt == 2)
        {
            buttons[selectionInt].selected = true;
            Selecting(2);
        }
        else
        {
            Deselecting(2);
        }
        if (selectionInt == 3)
        {
            buttons[selectionInt].selected = true;
            Selecting(3);
        }
        else
        {
            Deselecting(3);
        }
    }

    void Selected()
    {
        if (selectionInt == 0)
        {
            OnActing(0);
            OnActing(0);
            totalMercy += buttons[0].actVars.curMercy;
        }
        if (selectionInt == 1)
        {
            OnActing(1);
            totalMercy += buttons[1].actVars.curMercy;
            totalMercyMax += buttons[1].actVars.mercyMax;
        }
        if (selectionInt == 2)
        {
            OnActing(2);
            totalMercy += buttons[2].actVars.curMercy;
            totalMercyMax += buttons[2].actVars.mercyMax;
        }
        if (selectionInt == 3)
        {
            OnActing(3);
            totalMercy += buttons[3].actVars.curMercy;
            totalMercyMax += buttons[3].actVars.mercyMax;
        }
    }
    public void OnActing(int selectedInt)
    {
        canAct = false;
        buttons[selectedInt].actVars.curMercy += buttons[selectedInt].actVars.mercyValue[0];
        actingText.gameObject.SetActive(true);
        DialogueManager.instance.dialogueTxt = buttons[selectedInt].actVars.actTxt[0];
        Action doneTalking = () =>
        {
            Debug.Log("action initiated");
            DialogueManager.instance.shouldTalk = false;
            StartCoroutine(BattleManager.battleInstance.ActingSequence());
        };
        DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
        DialogueManager.instance.shouldTalk = true;
        DialogueManager.instance.Talking(doneTalking);
        actObjects.SetActive(false);
        if (buttons[selectedInt].actVars.actTxt.Count <= 2 || buttons[selectedInt].actVars.mercyValue.Count <= 2)
        {
            Debug.Log("We added");
            buttons[selectedInt].actVars.actTxt.Add(buttons[selectedInt].actVars.actTxt[0]);
            buttons[selectedInt].actVars.mercyValue.Add(buttons[selectedInt].actVars.mercyValue[0]);
        }
        else
        {
            buttons[selectedInt].actVars.actTxt.RemoveAt(0);
            buttons[selectedInt].actVars.mercyValue.RemoveAt(0);
        }
    }


}
