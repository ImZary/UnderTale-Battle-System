using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attacks")]
public class Attacks : ScriptableObject
{
    public IEnumerator GetAttack()
    {
        return Random.Range(0, 2) switch
        {
            1 => FroggitTwo(),
            _ => FroggitOne(), 
        };
}
    IEnumerator FroggitOne()
    {
        Vector2 pos = new Vector2(0, 0);
       AttackManager.instance.SpawnPellet(pos, PelletType.FallFollowDirect);
        yield return new WaitForSeconds(10);
        Debug.Log("The Froggit1 Attack (attack pool side) is done!");
    }
    IEnumerator FroggitTwo()
    {
        Vector2 pos = new Vector2(0, 0);
        AttackManager.instance.SpawnPellet(pos, PelletType.FollowDirect);
        yield return new WaitForSeconds(10);
        Debug.Log("this is froggit's 2nd attack");
    }
}
