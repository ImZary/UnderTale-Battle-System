using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActingButtons : MonoBehaviour
{
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public bool selected;
    public Transform soulPosition;
    [HideInInspector]
    public ActVars actVars;

    void Awake()
    {
        instance = this.gameObject;
        actVars = this.GetComponent<ActVars>();
    }
}
