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
        AttackManager.instance.SpawnPellet(new Vector2(Random.Range(1.4f, -1.4f), 0), PelletType.FallFollowDirect, 0);
        yield return new WaitForSeconds(1);
        AttackManager.instance.SpawnPellet(new Vector2(Random.Range(1.4f, -1.4f), 0), PelletType.FallFollowDirect, 0);
        yield return new WaitForSeconds(1);
        AttackManager.instance.SpawnPellet(new Vector2(Random.Range(1.4f, -1.4f), 0), PelletType.FallFollowDirect, 0);
        yield return new WaitForSeconds(5);
        
    }
    IEnumerator FroggitTwo()
    {
        Debug.Log("this is froggit's 2nd attack");
        Vector2 pos = new Vector2(1.013f, - 2.725f);
        AttackManager.instance.SpawnPellet(pos, PelletType.JumpDirect, 1);
        yield return new WaitForSeconds(2);
        
    }
}
