using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemButtons> buttons;
    [HideInInspector]
    public static ItemManager instance;
    void Awake() => instance = this;
    int maxSelectionInt;
    int minSelectionInt;
    public int selectionInt;
    public SpriteRenderer soul;
    public TextMeshPro useText;
    public GameObject itemObjects;
    bool isFighting;
    public float time;
    public bool isMenu;
    public bool canAct = true;
    void Start()
    {
        isFighting = BattleManager.battleInstance.isFighting;
        maxSelectionInt = 3;
        minSelectionInt = 0;
    }

    void Update()
    {
        if (!isFighting && isMenu)
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
                if (canAct && DialogueManager.instance.done)
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
        Action dialogue = () =>
        {
            DialogueManager.instance.shouldTalk = false;
            StartCoroutine(BattleManager.battleInstance.ItemSequence());
        };
        if (selectionInt == 0)
        {
            if (PlayerVars.instance.health < 20)
            {
                PlayerVars.instance.health += buttons[0].itemHeal;
            }
            soul.enabled = false;   
            DialogueManager.instance.dialogueTxt = "*You Ate The " + buttons[0].itemName + ", You Healed " + buttons[0].itemHeal + " HP.";
            DialogueManager.instance.text.gameObject.SetActive(true);
            DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
            DialogueManager.instance.shouldTalk = true;
            DialogueManager.instance.Talking(dialogue);
            itemObjects.SetActive(false);
        }
        if (selectionInt == 1)
        {
            if (PlayerVars.instance.health < 20)
            {
                PlayerVars.instance.health += buttons[1].itemHeal;
            }
            if (PlayerVars.instance.health > 20)
            {
                PlayerVars.instance.health = 20;
            }
            soul.enabled = false;
            DialogueManager.instance.dialogueTxt = "*You Ate The " + buttons[1].itemName + ", You Healed " + buttons[1].itemHeal + " HP.";
            DialogueManager.instance.text.gameObject.SetActive(true);
            DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
            DialogueManager.instance.shouldTalk = true;
            DialogueManager.instance.Talking(dialogue);
            itemObjects.SetActive(false);
        }
        if (selectionInt == 2)
        {
            if (PlayerVars.instance.health < 20)
            {
                PlayerVars.instance.health += buttons[2].itemHeal;
            }
            if (PlayerVars.instance.health > 20)
            {
                PlayerVars.instance.health = 20;
            }
            soul.enabled = false;
            DialogueManager.instance.dialogueTxt = "*You Ate The " + buttons[2].itemName + ", You Healed " + buttons[2].itemHeal + " HP.";
            DialogueManager.instance.text.gameObject.SetActive(true);
            DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
            DialogueManager.instance.shouldTalk = true;
            DialogueManager.instance.Talking(dialogue);
            itemObjects.SetActive(false);
        }
        if (selectionInt == 3)
        {
            if (PlayerVars.instance.health < 20)
            {
                PlayerVars.instance.health += buttons[3].itemHeal;
            }
            if (PlayerVars.instance.health > 20)
            {
                PlayerVars.instance.health = 20;
            }
            soul.enabled = false;
            DialogueManager.instance.dialogueTxt = "*You Ate The " + buttons[3].itemName + ", You Healed " + buttons[3].itemHeal + " HP.";
            DialogueManager.instance.text.gameObject.SetActive(true);
            DialogueManager.instance.enemyTxt = BattleManager.battleInstance.enemyDialogue[UnityEngine.Random.Range(0, BattleManager.battleInstance.enemyDialogue.Count)];
            DialogueManager.instance.shouldTalk = true;
            DialogueManager.instance.Talking(dialogue);
            itemObjects.SetActive(false);


        }

    }
}
