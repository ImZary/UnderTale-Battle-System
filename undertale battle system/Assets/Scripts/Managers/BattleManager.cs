using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [HideInInspector]
    public bool isFighting;
    [HideInInspector]
    public static BattleManager battleInstance;
    private AttackManager attackMgr;
    public ActingManager actingMgr;
    public AudioManager audioMgr;
    void Awake() => battleInstance = this;
    public SpriteRenderer soul;
    public SpriteRenderer battleBox;
    public List<Buttons> buttons;
    int maxSelectionInt;
    int minSelectionInt;
    int selectionInt;
    public GameObject mercyMenu;
    public GameObject damageSprite;
    const float SIZE_INCREASE = 14f;

    [HideInInspector]
    public Action isFinished;
    public Attacking attackingSys;
    PlayerVars playerVariables;
    public GameObject healthMeter;
    public TextMeshPro healthTxt;
    public float damage;
    public void Attacking()
    {
        if (!isFighting)
        {
            actingMgr.actingText.gameObject.SetActive(false);
            AudioManager.instance.Selecting();
            StartCoroutine(AttackSequence());
        }
       
    }
    public void Acting()
    {
        actingMgr.actObjects.SetActive(true);
        actingMgr.isActing = true;
        actingMgr.actingText.gameObject.SetActive(false);
        audioMgr.Selecting();
    }

    public void Item()
    {
        Debug.Log("We're using an item :o");
        audioMgr.Selecting();
    }

    public void Mercy()
    {
        if (actingMgr.totalMercy >= actingMgr.totalMercyMax)
        {
            Debug.Log("Battle Ended");
            audioMgr.Selecting();
        }
    }

    void Start()
    {
        selectionInt = 0;
        maxSelectionInt = 3;
        minSelectionInt = 0;
        attackMgr = AttackManager.instance;
        playerVariables = FindObjectOfType<PlayerVars>();
    }

    void Update()
    {
        if (!isFighting && !actingMgr.isActing)
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
            Selection();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Selected();
            }
            if (!attackMgr.attackFinished)
            {
                battleBox.size = new Vector2(11.5f,3);
            }
        }
        if (!isFighting)
        {
            playerVariables.gameObject.SetActive(false);
        }
        if (isFighting)
        {
            playerVariables.gameObject.SetActive(true);
        }
        float xScale = (1f * playerVariables.health)/ 20;
        healthTxt.text = playerVariables.health + "   /   20";
        healthMeter.transform.localScale = new Vector3(xScale,healthMeter.transform.localScale.y, healthMeter.transform.localScale.z);

    }

    void Selecting(int selectedInt)
    {
        if (buttons[selectedInt].selected)
        {
            buttons[selectedInt].currentSprite = buttons[selectedInt].buttonSelected;
            soul.transform.position = buttons[selectedInt].soulPosition.position;
            
        }
        else
        {
            buttons[selectedInt].currentSprite = buttons[selectedInt].buttonDeselected;
        }
    }
    void Deselecting(int deselectionInt)
    {
        buttons[deselectionInt].selected = false;
        buttons[deselectionInt].currentSprite = buttons[deselectionInt].buttonDeselected;
    }
    void Selection()
    {

        if (selectionInt == 0)
        {
            if (!buttons[selectionInt].selected)
            {
                audioMgr.Hovering();
            }
            buttons[selectionInt].selected = true;
            Selecting(0);
           
        }
        else
        {
            Deselecting(0);
        }
        if (selectionInt == 1)
        {
            if (!buttons[selectionInt].selected)
            {
                audioMgr.Hovering();
            }
            buttons[selectionInt].selected = true;
            Selecting(1);
        }
        else
        {
            Deselecting(1);
        }
        if (selectionInt == 2)
        {
            if (!buttons[selectionInt].selected)
            {
                audioMgr.Hovering();
            }
            buttons[selectionInt].selected = true;
            Selecting(2);
        }
        else
        {
            Deselecting(2);
        }
        if (selectionInt == 3)
        {
            if (!buttons[selectionInt].selected)
            {
                audioMgr.Hovering();
            }
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
            Attacking();
        }
        if (selectionInt == 1)
        {
            Acting();
        }
        if (selectionInt == 2)
        {
            Item();
        }
        if (selectionInt == 3)
        {
            Mercy();
        }
    }

    IEnumerator AttackSequence()
    {
        isFinished = () =>
        {
            StartCoroutine(ResizeBattleBox(new Vector2(11.5f, 3), null));
            attackMgr.attackFinished = !attackMgr.attackFinished;
            isFighting = false;
            actingMgr.actingText.gameObject.SetActive(true);
        };
        attackingSys.StartAttacking(playerVariables.atkValue);
        isFighting = true;
        yield return new WaitForSeconds(attackingSys.maxTime);
        StartCoroutine(ResizeBattleBox(new Vector2(3, 3), null));
        actingMgr.actingText.gameObject.SetActive(false);
        attackMgr.StartAttack(attackMgr.attacksScriptable.GetAttack(), isFinished);
    }
  public IEnumerator ActingSequence()
    {
        Debug.Log("ActingSequence start");
        Action boxAction = () =>
        {
            if (actingMgr.totalMercy >= actingMgr.totalMercyMax)
            {
                actingMgr.actingText.text = actingMgr.spareMessage;
            }
            else
            {
                actingMgr.actingText.text = actingMgr.flavorText[UnityEngine.Random.Range(0, actingMgr.flavorText.Capacity)];
            }
            
        };
        soul.enabled = false;
        actingMgr.actObjects.SetActive(false);
        isFinished = () =>
        {
            soul.enabled = true;
            soul.transform.position = buttons[1].soulPosition.position;
            StartCoroutine(ResizeBattleBox(new Vector2(11.5f, 3.1f), boxAction));
            actingMgr.isActing = false;
            isFighting = false;
            actingMgr.actingText.gameObject.SetActive(true);
            actingMgr.actObjects.SetActive(false);
        };
        yield return new WaitForSeconds(1);
        StartCoroutine(ResizeBattleBox(new Vector2(3, 3), boxAction));
        actingMgr.actingText.gameObject.SetActive(false);
        actingMgr.isActing = false;
        isFighting = true;
        actingMgr.time = 0;
        attackMgr.StartAttack(attackMgr.attacksScriptable.GetAttack(), isFinished);
    }
    IEnumerator ResizeBattleBox(Vector2 targetSize, Action onFinish)
    {
        Vector2 startSize = battleBox.size;
        float xSign = Mathf.Sign(targetSize.x - startSize.x);
        float ySign = Mathf.Sign(targetSize.y - startSize.y);

        Vector2 size = startSize;
        Debug.Log("Initiating resizing box");
        while (size.x != targetSize.x || size.y != targetSize.y)
        {
            Debug.Log("making the box larger, i think");
            size.x += xSign * SIZE_INCREASE * Time.deltaTime;
            size.y += ySign * SIZE_INCREASE * Time.deltaTime;

            if ((xSign == 1 && size.x > targetSize.x) || (xSign == -1 && size.x < targetSize.x))
            {
                size.x = targetSize.x;
                Debug.Log("changing battlebox x size");
            }
            if ((ySign == 1 && size.y > targetSize.y) || (ySign == -1 && size.y < targetSize.y))
            {
                size.y = targetSize.y;
                Debug.Log("changing battlebox y size");
            }
               
            battleBox.size = size;
            yield return null;
        }
        onFinish?.Invoke();
        
    }
}
