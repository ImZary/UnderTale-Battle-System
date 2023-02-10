using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public Sprite currentSprite;
    [HideInInspector]
    public Sprite instanceSprite;
    public Sprite buttonDeselected;
    public Sprite buttonSelected;
    public bool selected;
    public Transform soulPosition;

    void Awake()
    {
        instance = this.gameObject;
        instanceSprite = instance.GetComponent<SpriteRenderer>().sprite;
        currentSprite = instanceSprite;
    }

    void Update()
    {
        instance.GetComponent<SpriteRenderer>().sprite = currentSprite;   
    }
}
