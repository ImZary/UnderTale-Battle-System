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
    const float SIZE_INCREASE = 18f;
    public List<string> enemyDialogue;

    [HideInInspector]
    public Action isFinished;
    public Attacking attackingSys;
    PlayerVars playerVariables;
    public GameObject healthMeter;
    public TextMeshPro healthTxt;
    public float damage;
    /// <summary>
    /// The attacking method, gets called when the player selects the fight button, responsible for initiating attacks
    /// </summary>
    public void Attacking()
    {
        if (!isFighting)
        {
            actingMgr.actingText.gameObject.SetActive(false);
            AudioManager.instance.Selecting();
            StartCoroutine(AttackSequence());
        }
       
    }
    /// <summary>
    /// The acting method, gets called when the player selects the act button, responsible for initiating acts
    /// </summary>
    public void Acting()
    {
        actingMgr.actObjects.SetActive(true);
        actingMgr.isActing = true;
        actingMgr.actingText.gameObject.SetActive(false);
        audioMgr.Selecting();
    }
    /// <summary>
    /// The item method, does nothing so far
    /// </summary>
    public void Item()
    {
        ItemManager.instance.isMenu = true;
        ItemManager.instance.canAct = true;
        isFighting = false;
        ItemManager.instance.itemObjects.SetActive(true);
        ItemManager.instance.useText.gameObject.SetActive(false);
        audioMgr.Selecting();
    }
    /// <summary>
    /// The mercy method, as of right now it ends the battle but really it doesn't do anything special
    /// </summary>
    public void Mercy()
    {
        if (actingMgr.totalMercy >= actingMgr.totalMercyMax)
        {
            DialogueManager.instance.enemyTxt = "*Blushes Deeply*";
            DialogueManager.instance.Talking(null);
            audioMgr.Selecting();
        }
    }

    void Start()
    {
        //We set the selectionInt (the int responsible for knowing which button's which) to the fight button int, aka 0, & set it's min & max values
        selectionInt = 0;
        maxSelectionInt = 3;
        minSelectionInt = 0;
        attackMgr = AttackManager.instance;
        playerVariables = FindObjectOfType<PlayerVars>();
    }

    void Update()
    {
        //when the player is not fighting nor acting, these if statements get called
        if (!isFighting && !actingMgr.isActing && !ItemManager.instance.isMenu)
        {
            if (selectionInt > maxSelectionInt)
            {
                //if we overflow, automatically select the fight button
                selectionInt = 0;
            }
            if (selectionInt < minSelectionInt)
            {
                //if we underflow, automatically select the mercy button
                selectionInt = 3;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //if we press the left arrow key, reduce the selection int
                selectionInt--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //the opposite of what I just said above
                selectionInt++;
            }
            Selection();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //when we press the Enter key, the selected method will be called
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
        //setting how will the health value be showed in-game
        healthTxt.text = playerVariables.health + "   /   20";
        //the health meter, grabs the health value & divides it by 20, while also multiplying it with the transform's scale
        healthMeter.transform.localScale = new Vector3(xScale,healthMeter.transform.localScale.y, healthMeter.transform.localScale.z);

    }

    /// <summary>
    /// The method responsible for selecting, when a method is selected the soul will be positioned on the selected button.
    /// </summary>
    /// <param name="selectedInt"></param>
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
    /// <summary>
    /// The method responsible for deSelecting
    /// </summary>
    /// <param name="deselectionInt"></param>
    void Deselecting(int deselectionInt)
    {
        buttons[deselectionInt].selected = false;
        buttons[deselectionInt].currentSprite = buttons[deselectionInt].buttonDeselected;
    }
    /// <summary>
    /// The method that sets up the selection, a selectionInt of 0 means selecting the fight button, 1 is for acting, 2 for item, & 3 for mercy
    /// </summary>
    void Selection()
    {

        if (selectionInt == 0)
        {
            if (!buttons[selectionInt].selected)
            {
                //the "hover" sfx gets played
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
    /// <summary>
    /// The last method, this method calls the methods in the start to initiate each respective action of said button.
    /// </summary>
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
    /// <summary>
    /// The attack coroutine, responsible for initiating the attacks
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSequence()
    {
        isFighting = true;
        //The action that will get called once we finish resizing the battle box
        Action onBoxFinish = () =>
        {
            actingMgr.actingText.gameObject.SetActive(true);
        };
        //The action that will get called once we finish the round
        isFinished = () =>
        {
            StartCoroutine(ResizeBattleBox(new Vector2(11.5f, 3), onBoxFinish));
            attackMgr.attackFinished = !attackMgr.attackFinished;
            isFighting = false;
        };
        //Starting the attack (go look in the "Attacking" script if curious).
        attackingSys.StartAttacking(playerVariables.atkValue);
        //Setting the fight bool to true;
        playerVariables.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(attackingSys.maxTime);
        //Once we finish waiting for the player to attack, we move the player's soul to the middle of the battle box.
        playerVariables.transform.position = new Vector2(0, -1.7f);
        StartCoroutine(ResizeBattleBox(new Vector2(3, 3), null));
        actingMgr.actingText.gameObject.SetActive(false);
        playerVariables.GetComponent<SpriteRenderer>().enabled = true;
        attackMgr.StartAttack(attackMgr.attacksScriptable.GetAttack(), isFinished);
    }
  public IEnumerator ActingSequence()
    {
        //Action that gets called once we finish resizing
        Action boxAction = () =>
        {
            //A pretty simple system, it checks for our mercy value, if we meet the requirement for sparing, a spare message appears, otherwise you'll get a new flavour text.
            if (actingMgr.totalMercy >= actingMgr.totalMercyMax)
            {
                DialogueManager.instance.dialogueTxt = actingMgr.spareMessage;
            }
            else
            {
                DialogueManager.instance.dialogueTxt = actingMgr.flavorText[UnityEngine.Random.Range(0, actingMgr.flavorText.Capacity)];
            }
            
        };
        //The selection soul, gets disabled in preperation for the attack this round.
        soul.enabled = false;
        //The acting menu gets disabled, as we are about to start the round.
        //Action that gets called once we finish the round.
        isFinished = () =>
        {
            soul.enabled = true;
            soul.transform.position = buttons[1].soulPosition.position;
            StartCoroutine(ResizeBattleBox(new Vector2(11.5f, 3f), boxAction));
            actingMgr.isActing = false;
            isFighting = false;
            actingMgr.actingText.gameObject.SetActive(true);
            DialogueManager.instance.Talking(null);
            actingMgr.actObjects.SetActive(false);
            actingMgr.canAct = true;

        };
        yield return new WaitForSeconds(1);
        playerVariables.transform.position = new Vector2(0, -1.7f);
        StartCoroutine(ResizeBattleBox(new Vector2(3, 3), boxAction));
        actingMgr.actingText.gameObject.SetActive(false);
        actingMgr.isActing = false;
        isFighting = true;
        actingMgr.time = 0;
        attackMgr.StartAttack(attackMgr.attacksScriptable.GetAttack(), isFinished);
    }

    public IEnumerator ItemSequence()
    {
        //The selection soul, gets disabled in preperation for the attack this round.
        soul.enabled = false;
        //The acting menu gets disabled, as we are about to start the round.
        //Action that gets called once we finish the round.
        isFinished = () =>
        {
            if (actingMgr.totalMercy >= actingMgr.totalMercyMax)
            {
                DialogueManager.instance.dialogueTxt = actingMgr.spareMessage;
            }
            else
            {
                DialogueManager.instance.dialogueTxt = actingMgr.flavorText[UnityEngine.Random.Range(0, actingMgr.flavorText.Capacity)];
            }
            ItemManager.instance.time = 0;
            soul.enabled = true;
            soul.transform.position = buttons[2].soulPosition.position;
            StartCoroutine(ResizeBattleBox(new Vector2(11.5f, 3f), null));
            actingMgr.isActing = false;
            isFighting = false;
            actingMgr.actingText.gameObject.SetActive(true);
            DialogueManager.instance.Talking(null);
            actingMgr.actObjects.SetActive(false);
            ItemManager.instance.itemObjects.SetActive(false);
            actingMgr.canAct = true;

        };
        yield return new WaitForSeconds(1);
        playerVariables.transform.position = new Vector2(0, -1.7f);
        StartCoroutine(ResizeBattleBox(new Vector2(3, 3), null));
        ItemManager.instance.itemObjects.SetActive(false);
        isFighting = true;
        ItemManager.instance.isMenu = false;
        ItemManager.instance.useText.text = "";
        actingMgr.time = 0;
        attackMgr.StartAttack(attackMgr.attacksScriptable.GetAttack(), isFinished);
    }
    /// <summary>
    /// The coroutine behind the resizing system, I think you can figure this one out for yourself.
    /// </summary>
    /// <param name="targetSize"></param>
    /// <param name="onFinish"></param>
    /// <returns></returns>
    IEnumerator ResizeBattleBox(Vector2 targetSize, Action onFinish)
    {
        Vector2 startSize = battleBox.size;
        float xSign = Mathf.Sign(targetSize.x - startSize.x);
        float ySign = Mathf.Sign(targetSize.y - startSize.y);

        Vector2 size = startSize;
        while (size.x != targetSize.x || size.y != targetSize.y)
        {
            size.x += xSign * SIZE_INCREASE * Time.deltaTime;
            size.y += ySign * SIZE_INCREASE * Time.deltaTime;

            if ((xSign == 1 && size.x > targetSize.x) || (xSign == -1 && size.x < targetSize.x))
            {
                size.x = targetSize.x;
            }
            if ((ySign == 1 && size.y > targetSize.y) || (ySign == -1 && size.y < targetSize.y))
            {
                size.y = targetSize.y;
            }
               
            battleBox.size = size;
            yield return null;
        }
        onFinish?.Invoke();
        
    }
}
