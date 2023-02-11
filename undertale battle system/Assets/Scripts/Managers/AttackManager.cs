using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance;
    void Awake() => instance = this;
    public Pellet[] pelletPrefab;
    public Attacks attacksScriptable;
    public bool attackFinished;

    List<IFightObject> attackObject = new();

    public void StartAttack(IEnumerator attack, Action onFinish)
    {
        attackFinished = false;
        StartCoroutine(StartAttackEnumerator(attack, onFinish));
    }

    void Update()
    {
        for (int i = 0; i < attackObject.Count; i++)
        {
            IFightObject curObject = attackObject[i];
            if (curObject == null)
                Debug.LogWarning($"THE ATTACK OBJECT AT {i} IS NULL!!!!");
            else
                curObject.Tick();
        }

    }
    public void SpawnPellet(Vector2 position, PelletType type, int pelletType)
    {
        Pellet newPellet = Instantiate(pelletPrefab[pelletType], position, Quaternion.identity).GetComponent<Pellet>();

        newPellet.type = type;
        IFightObject pelletAsObj = (IFightObject)newPellet;
        pelletAsObj.Spawn();
        attackObject.Add(pelletAsObj);
    }
    IEnumerator StartAttackEnumerator(IEnumerator attack, Action onFinish)
    {
        while (attack.MoveNext())
        {
            yield return attack.Current;
        }

        onFinish?.Invoke();
        for (int i = 0; i < attackObject.Count; i++)
            attackObject[i].Remove();
        attackObject.Clear();
    }
}
