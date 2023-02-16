using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [HideInInspector]
    public static DialogueHolder instance;
    public string[] dialogue;
    void Awake () => instance = this;

}
