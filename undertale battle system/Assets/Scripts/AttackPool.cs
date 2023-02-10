using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPool : MonoBehaviour
{
    public List<IEnumerator> list = new ();
    public AttackManager attackManager;
    void Start()
    {
        attackManager = FindObjectOfType<AttackManager>();
        list.Add(Froggit1());
    }
    IEnumerator Froggit1()
    {
        Vector2 pos = new Vector2(0,0);
        attackManager.SpawnPellet(pos, PelletType.FallFollowDirect);   
        yield return new WaitForSeconds(1);
        Debug.Log("The Froggit1 Attack (attack pool side) is done!");
    }

   
}
